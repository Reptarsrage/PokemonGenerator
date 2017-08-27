using PokemonGenerator.Modals;
using System.Collections;
using System.IO;

namespace PokemonGenerator.IO
{
    /// <summary>
    /// Contains all tools needed to parse data from a pokemon Gold/Silver sav file. <para/> 
    /// 
    /// See information available here for a detailed explaination: <para/> 
    /// http://bulbapedia.bulbagarden.net/wiki/Save_data_structure_in_Generation_II <para/> 
    /// http://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_structure_in_Generation_II
    /// </summary>
    class PokeDeserializer
    {

        /// <summary>
        /// Completely parses a given file stream into a <see cref="Pokemon"/> .
        /// </summary>
        /// <param name="breader">Steam</param>
        /// <param name="inBox">Is the pokemon in a box or on a team?</param>
        /// <param name="charset">The charset to use for string literals</param>
        public Pokemon ParsePokemon(BinaryReader2 breader, bool inBox, Charset charset)
        {
            byte[] buffer = new byte[1];

            Pokemon poke = new Pokemon();

            poke.species = breader.ReadByte();
            poke.heldItem = breader.ReadByte();
            poke.moveIndex1 = breader.ReadByte();
            poke.moveIndex2 = breader.ReadByte();
            poke.moveIndex3 = breader.ReadByte();
            poke.moveIndex4 = breader.ReadByte();
            poke.trainerID = breader.ReadInt16();
            poke.experience = breader.ReadInt24();
            poke.hpEV = breader.ReadInt16();
            poke.attackEV = breader.ReadInt16();
            poke.defenseEV = breader.ReadInt16();
            poke.speedEV = breader.ReadInt16();
            poke.specialEV = breader.ReadInt16();


            breader.ReadBits(4).CopyTo(buffer, 0);
            poke.attackIV = buffer[0];

            breader.ReadBits(4).CopyTo(buffer, 0);
            poke.defenseIV = buffer[0];

            breader.ReadBits(4).CopyTo(buffer, 0);
            poke.speedIV = buffer[0];

            breader.ReadBits(4).CopyTo(buffer, 0);
            poke.specialIV = buffer[0];

            breader.ReadBits(2).CopyTo(buffer, 0);
            poke.ppUps1 = buffer[0];

            breader.ReadBits(6).CopyTo(buffer, 0);
            poke.currentPP1 = buffer[0];

            breader.ReadBits(2).CopyTo(buffer, 0);
            poke.ppUps2 = buffer[0];

            breader.ReadBits(6).CopyTo(buffer, 0);
            poke.currentPP2 = buffer[0];

            breader.ReadBits(2).CopyTo(buffer, 0);
            poke.ppUps3 = buffer[0];

            breader.ReadBits(6).CopyTo(buffer, 0);
            poke.currentPP3 = buffer[0];

            breader.ReadBits(2).CopyTo(buffer, 0);
            poke.ppUps4 = buffer[0];

            breader.ReadBits(6).CopyTo(buffer, 0);
            poke.currentPP4 = buffer[0];

            poke.friendship = breader.ReadByte();

            breader.ReadBits(4).CopyTo(buffer, 0);
            poke.pokerusStrain = buffer[0];

            breader.ReadBits(4).CopyTo(buffer, 0);
            poke.pokerusDuration = buffer[0];

            breader.ReadBits(2).CopyTo(buffer, 0);
            poke.caughtTime = buffer[0];

            breader.ReadBits(6).CopyTo(buffer, 0);
            poke.caughtLevel = buffer[0];

            breader.ReadBit().CopyTo(buffer, 0);
            poke.OTGender = buffer[0];

            breader.ReadBits(7).CopyTo(buffer, 0);
            poke.caughtLocation = buffer[0];

            poke.level = breader.ReadByte();

            if (!inBox)
            {
                poke.status = breader.ReadByte();
                poke.unused = breader.ReadByte();
                poke.currentHp = breader.ReadInt16();
                poke.maxHp = breader.ReadInt16();
                poke.attack = breader.ReadInt16();
                poke.defense = breader.ReadInt16();
                poke.speed = breader.ReadInt16();
                poke.spAttack = breader.ReadInt16();
                poke.spDefense = breader.ReadInt16();
            }

            return poke;
        }

        /// <summary>
        /// Completely parses a given file stream into a <see cref="ItemList"/> .
        /// </summary>
        public ItemList ParseItemList(BinaryReader2 breader, Charset charset, int capacity, bool key = false)
        {
            ItemList list = new ItemList(capacity);
            list.Count = breader.ReadByte();
            list.ItemEntries = new ItemEntry[list.Count];

            for (int i = 0; i < capacity; i++)
            {
                ItemEntry entry = new ItemEntry();
                entry.Index = breader.ReadByte();

                if (key)
                {
                    entry.Count = 1;
                }
                else
                {
                    entry.Count = breader.ReadByte();
                }

                if (i < list.Count)
                {
                    list.ItemEntries[i] = entry;
                }
            }

            list.Terminator = breader.ReadByte();

            return list;

        }

        /// <summary>
        /// Completely parses a given file stream into a <see cref="PokeList"/> .
        /// </summary>
        public PokeList ParsePokeList(BinaryReader2 breader, Charset charset, bool full, int capacity)
        {
            PokeList list = new PokeList(capacity);
            list.Count = breader.ReadByte();
            list.Pokemon = new Pokemon[list.Count];

            // Species
            for (int i = 0; i < capacity + 1; i++)
            {
                if (i < list.Count)
                {
                    list.Pokemon[i] = new Pokemon();
                    list.Pokemon[i].species = breader.ReadByte();
                }
                else
                {
                    breader.ReadByte();
                }
            }

            // Pokemon
            for (int i = 0; i < capacity; i++)
            {
                if (i < list.Count)
                {
                    Pokemon temp = ParsePokemon(breader, !full, charset);
                    temp.species = list.Pokemon[i].species;
                    list.Pokemon[i] = temp;
                }
                else
                {
                    breader.BaseStream.Seek(full ? 48 : 32, SeekOrigin.Current); // sizeof pokemon
                }
            }

            // OT Names
            for (int i = 0; i < capacity; i++)
            {
                if (i < list.Count)
                {
                    list.Pokemon[i].OTName = breader.readString(11, charset);
                }
                else
                {
                    breader.readString(11, charset);
                }
            }

            // Names
            for (int i = 0; i < capacity; i++)
            {
                if (i < list.Count)
                {
                    list.Pokemon[i].Name = breader.readString(11, charset);
                }
                else
                {
                    breader.readString(11, charset);
                }
            }

            return list;
        }

        /// <summary>
        /// Completely parses a given file stream into a <see cref="TMPocket"/> .
        /// </summary>
        public TMPocket ParseTMPocket(BinaryReader2 breader, Charset charset)
        {
            TMPocket pocket = new TMPocket();
            for (int i = 0; i < 50; i++)
            {
                pocket.TMs[i] = breader.ReadByte();
            }

            for (int i = 0; i < 7; i++)
            {
                pocket.HMs[i] = breader.ReadByte();
            }

            return pocket;
        }

        /// <summary>
        /// Completely parses a given file stream into a <see cref="SAVFileModel"/> .
        /// </summary>
        public SAVFileModel ParseSAVFileModel(BinaryReader2 breader, Charset charset)
        {
            SAVFileModel sav = new SAVFileModel();

            breader.BaseStream.Seek(0x2000, SeekOrigin.Begin);
            sav.Options = breader.ReadInt64();

            breader.BaseStream.Seek(0x2009, SeekOrigin.Begin);
            sav.PlayerTrainerID = breader.ReadInt16();

            breader.BaseStream.Seek(0x200B, SeekOrigin.Begin);
            sav.Playername = breader.readString(11, charset);

            breader.BaseStream.Seek(0x2021, SeekOrigin.Begin);
            sav.Rivalname = breader.readString(11, charset);

            breader.BaseStream.Seek(0x2037, SeekOrigin.Begin);
            sav.Daylightsavings = (breader.ReadByte() & 0x80) == 1;

            breader.BaseStream.Seek(0x2053, SeekOrigin.Begin);
            sav.Timeplayed = breader.ReadInt32();

            breader.BaseStream.Seek(0x206B, SeekOrigin.Begin);
            sav.Playerpalette = breader.ReadByte();

            breader.BaseStream.Seek(0x23DB, SeekOrigin.Begin);
            sav.Money = breader.ReadInt24();

            breader.BaseStream.Seek(0x23E4, SeekOrigin.Begin);
            sav.JohtoBadges = breader.ReadByte();

            BitArray arr = new BitArray(new byte[] { sav.JohtoBadges });
            sav.JohtoBadges = 0;
            foreach (bool bit in arr)
            {
                if (bit)
                {
                    sav.JohtoBadges++;
                }
            }

            breader.BaseStream.Seek(0x23E6, SeekOrigin.Begin);
            sav.TMpocket = this.ParseTMPocket(breader, charset);

            breader.BaseStream.Seek(0x241F, SeekOrigin.Begin);
            sav.Itempocketitemlist = this.ParseItemList(breader, charset, 20);

            breader.BaseStream.Seek(0x2449, SeekOrigin.Begin);
            sav.Keyitempocketitemlist = this.ParseItemList(breader, charset, 26, true);

            breader.BaseStream.Seek(0x2464, SeekOrigin.Begin);
            sav.Ballpocketitemlist = this.ParseItemList(breader, charset, 12);

            breader.BaseStream.Seek(0x247E, SeekOrigin.Begin);
            sav.PCitemlist = this.ParseItemList(breader, charset, 50);

            breader.BaseStream.Seek(0x2724, SeekOrigin.Begin);
            sav.CurrentPCBoxnumber = breader.ReadByte();

            // Boxes
            breader.BaseStream.Seek(0x2727, SeekOrigin.Begin);
            sav.PCBoxnames = new string[14];
            for (int i = 0; i < 14; i++)
            {
                sav.PCBoxnames[i] = breader.readString(9, charset);
            }

            // Team
            breader.BaseStream.Seek(0x288A, SeekOrigin.Begin);
            sav.TeamPokémonlist = ParsePokeList(breader, charset, true, 6);

            // Pokedex
            breader.BaseStream.Seek(0x2A4C, SeekOrigin.Begin);
            sav.Pokédexowned = new bool[32 * 8];
            byte[] pokedex = breader.ReadBytes(32);
            arr = new BitArray(pokedex);
            arr.CopyTo(sav.Pokédexowned, 0);

            breader.BaseStream.Seek(0x2A6C, SeekOrigin.Begin);
            sav.Pokédexseen = new bool[32 * 8];
            pokedex = breader.ReadBytes(32);
            arr = new BitArray(pokedex);
            arr.CopyTo(sav.Pokédexseen, 0);

            // Current Box List
            breader.BaseStream.Seek(0x2D6C, SeekOrigin.Begin);
            sav.CurrentBoxPokémonlist = ParsePokeList(breader, charset, false, 20);

            // GET 1-7 boxes
            sav.Boxes = new PokeList[14];

            for (int i = 0; i < 7; i++)
            {
                breader.BaseStream.Seek(0x4000 + 0x450 * i, SeekOrigin.Begin);
                sav.Boxes[i] = ParsePokeList(breader, charset, false, 20);
            }

            // GET 8-14 boxes
            for (int i = 7; i < 14; i++)
            {
                breader.BaseStream.Seek(0x6000 + 0x450 * (i - 7), SeekOrigin.Begin);
                sav.Boxes[i] = ParsePokeList(breader, charset, false, 20);
            }

            // Checksum 0x2009 - 0x2D68
            breader.BaseStream.Seek(0x2D69, SeekOrigin.Begin);
            sav.Checksum1 = breader.ReadInt16LittleEndian();

            // Calculate checksum
            breader.BaseStream.Seek(0x2009, SeekOrigin.Begin);
            ushort checksum = 0;
            while (breader.BaseStream.Position <= 0x2D68)
            {
                checksum += breader.ReadByte();
            }

            if (checksum != sav.Checksum1)
            {
                throw new InvalidDataException("Checksum doesn't match. Data possibly corrupt.");
            }

            return sav;
        }

    }
}