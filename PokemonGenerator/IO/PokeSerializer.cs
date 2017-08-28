using PokemonGenerator.Modals;
using System.Collections;
using System.IO;

namespace PokemonGenerator.IO
{
    /// <summary>
    /// Contains all tools needed to serialize data into a pokemon Gold/Silver sav file. <para/> 
    /// 
    /// See information available here for a detailed explaination: <para/> 
    /// http://bulbapedia.bulbagarden.net/wiki/Save_data_structure_in_Generation_II <para/> 
    /// http://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_structure_in_Generation_II
    /// </summary>
    internal class PokeSerializer : IPokeSerializer
    {
        /// <summary>
        /// Completely serialized a given <see cref="SAVFileModel"/> into the file stream.
        /// </summary>
        public void SerializeSAVFileModal(string @out, Charset charset, SAVFileModel sav)
        {
            using (FileStream writer = File.OpenWrite(@out))
            using (BinaryWriter2 bwriter = new BinaryWriter2(writer))
            {

                bwriter.BaseStream.Seek(0x2000, SeekOrigin.Begin);
                bwriter.WriteInt64(sav.Options);

                bwriter.BaseStream.Seek(0x2009, SeekOrigin.Begin);
                bwriter.WriteInt16((ushort)sav.PlayerTrainerID);

                bwriter.BaseStream.Seek(0x200B, SeekOrigin.Begin);
                bwriter.writeString(sav.Playername, 11, charset);

                bwriter.BaseStream.Seek(0x2021, SeekOrigin.Begin);
                bwriter.writeString(sav.Rivalname, 11, charset);

                bwriter.BaseStream.Seek(0x2037, SeekOrigin.Begin);
                bwriter.Write((byte)(sav.Daylightsavings ? 0x80 : 0));

                bwriter.BaseStream.Seek(0x2053, SeekOrigin.Begin);
                bwriter.WriteInt32(sav.Timeplayed);

                bwriter.BaseStream.Seek(0x206B, SeekOrigin.Begin);
                bwriter.Write(sav.Playerpalette);

                bwriter.BaseStream.Seek(0x23DB, SeekOrigin.Begin);
                bwriter.WriteInt24(sav.Money);

                bwriter.BaseStream.Seek(0x23E4, SeekOrigin.Begin);

                bwriter.Write((byte)0xff); // TODO  ?

                bwriter.BaseStream.Seek(0x23E6, SeekOrigin.Begin);
                this.SerializeTMPocket(bwriter, charset, sav.TMpocket);

                bwriter.BaseStream.Seek(0x241F, SeekOrigin.Begin);
                this.SerializeItemList(bwriter, charset, 20, sav.Itempocketitemlist);

                bwriter.BaseStream.Seek(0x2449, SeekOrigin.Begin);
                this.SerializeItemList(bwriter, charset, 26, sav.Keyitempocketitemlist, true);

                bwriter.BaseStream.Seek(0x2464, SeekOrigin.Begin);
                this.SerializeItemList(bwriter, charset, 12, sav.Ballpocketitemlist);

                bwriter.BaseStream.Seek(0x247E, SeekOrigin.Begin);
                this.SerializeItemList(bwriter, charset, 50, sav.PCitemlist);

                bwriter.BaseStream.Seek(0x2724, SeekOrigin.Begin);
                bwriter.Write(sav.CurrentPCBoxnumber);

                // Boxes
                bwriter.BaseStream.Seek(0x2727, SeekOrigin.Begin);
                for (int i = 0; i < 14; i++)
                {
                    bwriter.writeString(sav.PCBoxnames[i], 9, charset);
                }

                // Team
                bwriter.BaseStream.Seek(0x288A, SeekOrigin.Begin);
                SerializePokeList(bwriter, charset, true, 6, sav.TeamPokémonlist);

                // Pokedex
                bwriter.BaseStream.Seek(0x2A4C, SeekOrigin.Begin);
                BitArray arr = new BitArray(sav.Pokédexowned);
                byte[] bytes = new byte[32];
                arr.CopyTo(bytes, 0);
                foreach (byte b in bytes)
                {
                    bwriter.Write(b);
                }

                bwriter.BaseStream.Seek(0x2A6C, SeekOrigin.Begin);
                arr = new BitArray(sav.Pokédexseen);
                bytes = new byte[32];
                arr.CopyTo(bytes, 0);
                foreach (byte b in bytes)
                {
                    bwriter.Write(b);
                }

                // Current Box List
                bwriter.BaseStream.Seek(0x2D6C, SeekOrigin.Begin);
                SerializePokeList(bwriter, charset, false, 20, sav.CurrentBoxPokémonlist);

                // GET 1-7 boxes
                for (int i = 0; i < 7; i++)
                {
                    bwriter.BaseStream.Seek(0x4000 + 0x450 * i, SeekOrigin.Begin);
                    SerializePokeList(bwriter, charset, false, 20, sav.Boxes[i]);
                }

                // GET 8-14 boxes
                for (int i = 7; i < 14; i++)
                {
                    bwriter.BaseStream.Seek(0x6000 + 0x450 * (i - 7), SeekOrigin.Begin);
                    SerializePokeList(bwriter, charset, false, 20, sav.Boxes[i]);
                }
            }

            ushort checksum = 0;
            using (FileStream reader = File.OpenRead(@out))
            using (BinaryReader2 breader = new BinaryReader2(reader))
            {
                // Calculate checksum
                breader.BaseStream.Seek(0x2009, SeekOrigin.Begin);
                while (breader.BaseStream.Position <= 0x2D68)
                {
                    checksum += breader.ReadByte();
                }
            }

            using (FileStream writer = File.OpenWrite(@out))
            using (BinaryWriter2 bwriter = new BinaryWriter2(writer))
            {
                // Checksum 0x2009 - 0x2D68
                bwriter.BaseStream.Seek(0x2D69, SeekOrigin.Begin);
                bwriter.WriteInt16LittleEndian(checksum);
            }
        }

        /// <summary>
        /// Completely serialized a given <see cref="Pokemon"/> into the file stream.
        /// </summary>
        private void SerializePokemon(BinaryWriter2 bwriter, bool inBox, Charset charset, Pokemon poke)
        {
            byte[] buffer = new byte[1];

            bwriter.Write(poke.species);
            bwriter.Write(poke.heldItem);
            bwriter.Write(poke.moveIndex1);
            bwriter.Write(poke.moveIndex2);
            bwriter.Write(poke.moveIndex3);
            bwriter.Write(poke.moveIndex4);
            bwriter.WriteInt16(poke.trainerID);
            bwriter.WriteInt24(poke.experience);
            bwriter.WriteInt16(poke.hpEV);
            bwriter.WriteInt16(poke.attackEV);
            bwriter.WriteInt16(poke.defenseEV);
            bwriter.WriteInt16(poke.speedEV);
            bwriter.WriteInt16(poke.specialEV);

            buffer[0] = poke.attackIV;
            bwriter.WriteBits(new BitArray(buffer), 4);

            buffer[0] = poke.defenseIV;
            bwriter.WriteBits(new BitArray(buffer), 4);

            buffer[0] = poke.speedIV;
            bwriter.WriteBits(new BitArray(buffer), 4);

            buffer[0] = poke.specialIV;
            bwriter.WriteBits(new BitArray(buffer), 4);


            buffer[0] = poke.ppUps1;
            bwriter.WriteBits(new BitArray(buffer), 2);

            buffer[0] = poke.currentPP1;
            bwriter.WriteBits(new BitArray(buffer), 6);

            buffer[0] = poke.ppUps2;
            bwriter.WriteBits(new BitArray(buffer), 2);

            buffer[0] = poke.currentPP2;
            bwriter.WriteBits(new BitArray(buffer), 6);

            buffer[0] = poke.ppUps3;
            bwriter.WriteBits(new BitArray(buffer), 2);

            buffer[0] = poke.currentPP3;
            bwriter.WriteBits(new BitArray(buffer), 6);

            buffer[0] = poke.ppUps4;
            bwriter.WriteBits(new BitArray(buffer), 2);

            buffer[0] = poke.currentPP4;
            bwriter.WriteBits(new BitArray(buffer), 6);

            bwriter.Write(poke.friendship);

            buffer[0] = poke.pokerusStrain;
            bwriter.WriteBits(new BitArray(buffer), 4);

            buffer[0] = poke.pokerusDuration;
            bwriter.WriteBits(new BitArray(buffer), 4);

            buffer[0] = poke.caughtTime;
            bwriter.WriteBits(new BitArray(buffer), 2);

            buffer[0] = poke.caughtLevel;
            bwriter.WriteBits(new BitArray(buffer), 6);

            buffer[0] = poke.OTGender;
            bwriter.WriteBit(new BitArray(buffer));

            buffer[0] = poke.caughtLocation;
            bwriter.WriteBits(new BitArray(buffer), 7);

            bwriter.Write(poke.level);

            if (!inBox)
            {
                bwriter.Write(poke.status);
                bwriter.Write(poke.unused);
                bwriter.WriteInt16(poke.currentHp);
                bwriter.WriteInt16(poke.maxHp);
                bwriter.WriteInt16(poke.attack);
                bwriter.WriteInt16(poke.defense);
                bwriter.WriteInt16(poke.speed);
                bwriter.WriteInt16(poke.spAttack);
                bwriter.WriteInt16(poke.spDefense);
            }
        }

        /// <summary>
        /// Completely serialized a given <see cref="ItemList"/> into the file stream.
        /// </summary>
        private void SerializeItemList(BinaryWriter2 bwriter, Charset charset, int capacity, ItemList list, bool key = false)
        {
            bwriter.Write(list.Count);

            for (int i = 0; i < capacity; i++)
            {
                if (i < list.ItemEntries.Length)
                {
                    ItemEntry entry = list.ItemEntries[i];
                    bwriter.Write(entry.Index);

                    if (!key)
                    {
                        bwriter.Write(entry.Count);
                    }
                }
                else
                {
                    bwriter.Write((byte)0xff);

                    if (!key)
                    {
                        bwriter.Write((byte)0xff);
                    }
                }
            }
            bwriter.Write(list.Terminator);
        }

        /// <summary>
        /// Completely serialized a given <see cref="PokeList"/> into the file stream.
        /// </summary>
        private void SerializePokeList(BinaryWriter2 bwriter, Charset charset, bool full, int capacity, PokeList list)
        {
            bwriter.Write(list.Count);

            // Species
            for (int i = 0; i < capacity + 1; i++)
            {
                if (i < list.Count)
                {
                    bwriter.Write(list.Pokemon[i].species);
                }
                else
                {
                    bwriter.Write((byte)0xff);
                }
            }

            // Pokemon
            for (int i = 0; i < capacity; i++)
            {
                if (i < list.Count)
                {
                    SerializePokemon(bwriter, !full, charset, list.Pokemon[i]);
                }
                else
                {
                    bwriter.Fill(0xff, (full ? 48 : 32)); // sizeof pokemon
                }
            }

            // OT Names
            for (int i = 0; i < capacity; i++)
            {
                if (i < list.Count)
                {
                    bwriter.writeString(list.Pokemon[i].OTName, 11, charset);
                }
                else
                {
                    bwriter.Fill(0xff, 11);
                }
            }

            // Names
            for (int i = 0; i < capacity; i++)
            {
                if (i < list.Count)
                {
                    bwriter.writeString(list.Pokemon[i].Name, 11, charset);
                }
                else
                {
                    bwriter.Fill(0xff, 11);
                }
            }
        }

        /// <summary>
        /// Completely serialized a given <see cref="TMPocket"/> into the file stream.
        /// </summary>
        private void SerializeTMPocket(BinaryWriter2 bwriter, Charset charset, TMPocket pocket)
        {
            for (int i = 0; i < 50; i++)
            {
                bwriter.Write(pocket.TMs[i]);
            }

            for (int i = 0; i < 7; i++)
            {
                bwriter.Write(pocket.HMs[i]);
            }
        }
    }
}