using PokemonGenerator.Models.Serialization;
using System.Collections;
using System.IO;
using System.Linq;

namespace PokemonGenerator.IO
{
    public interface IPokeSerializer
    {
        void SerializeSAVFileModal(string outFilePath, SAVFileModel sav);
        void SerializeSAVFileModal(Stream stream, SAVFileModel sav);
    }

    /// <summary>
    /// Contains all tools needed to serialize data into a pokemon Gold/Silver sav file. <para/> 
    /// 
    /// See information available here for a detailed explaination: <para/> 
    /// http://bulbapedia.bulbagarden.net/wiki/Save_data_structure_in_Generation_II <para/> 
    /// http://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_structure_in_Generation_II
    /// </summary>
    public class PokeSerializer : IPokeSerializer
    {
        private readonly ICharset _charset;
        private readonly IBinaryWriter2 _bwriter;
        private readonly IBinaryReader2 _breader;

        public PokeSerializer(ICharset charset, IBinaryWriter2 writer, IBinaryReader2 reader)
        {
            _charset = charset;
            _bwriter = writer;
            _breader = reader;
        }

        /// <summary>
        /// Completely serialized a given <see cref="SAVFileModel"/> into the file stream.
        /// </summary>
        public void SerializeSAVFileModal(string fileName, SAVFileModel sav)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                _bwriter.Open(stream);
                _breader.Open(stream);
                SerializeSAVFileModal(sav);
                _breader.Close();
                _bwriter.Close();
            }
        }

        /// <summary>
        /// Completely serialized a given <see cref="SAVFileModel"/> into the file stream.
        /// </summary>
        public void SerializeSAVFileModal(Stream stream, SAVFileModel sav)
        {
            _bwriter.Open(stream);
            _breader.Open(stream);
            SerializeSAVFileModal(sav);
            _breader.Close();
            _bwriter.Close();
        }

        private void SerializeSAVFileModal(SAVFileModel sav)
        {
            _bwriter.Seek(0x2000, SeekOrigin.Begin);
            _bwriter.WriteUInt64(sav.Options);

            _bwriter.Seek(0x2009, SeekOrigin.Begin);
            _bwriter.WriteUInt16((ushort)sav.PlayerTrainerID);

            _bwriter.Seek(0x200B, SeekOrigin.Begin);
            _bwriter.WriteString(sav.PlayerName, 11, _charset);

            _bwriter.Seek(0x2021, SeekOrigin.Begin);
            _bwriter.WriteString(sav.RivalName, 11, _charset);

            _bwriter.Seek(0x2037, SeekOrigin.Begin);
            _bwriter.Write((byte)(sav.Daylightsavings ? 0x80 : 0));

            _bwriter.Seek(0x2053, SeekOrigin.Begin);
            _bwriter.WriteUInt32(sav.TimePlayed);

            _bwriter.Seek(0x206B, SeekOrigin.Begin);
            _bwriter.Write(sav.Playerpalette);

            _bwriter.Seek(0x23DB, SeekOrigin.Begin);
            _bwriter.WriteUInt24(sav.Money);

            _bwriter.Seek(0x23E4, SeekOrigin.Begin);

            // Johnto Badges
            var arr = new BitArray(Enumerable.Repeat(true, sav.JohtoBadges).ToArray());
            byte[] buffer = new byte[1];
            arr.CopyTo(buffer, 0);
            _bwriter.Write(buffer[0]);

            _bwriter.Seek(0x23E6, SeekOrigin.Begin);
            SerializeTMPocket(_bwriter, _charset, sav.TMpocket);

            _bwriter.Seek(0x241F, SeekOrigin.Begin);
            SerializeItemList(_bwriter, _charset, 20, sav.PocketItemList);

            _bwriter.Seek(0x2449, SeekOrigin.Begin);
            SerializeItemList(_bwriter, _charset, 26, sav.PocketKeyItemList, true);

            _bwriter.Seek(0x2464, SeekOrigin.Begin);
            SerializeItemList(_bwriter, _charset, 12, sav.PocketBallItemList);

            _bwriter.Seek(0x247E, SeekOrigin.Begin);
            SerializeItemList(_bwriter, _charset, 50, sav.PCItemList);

            _bwriter.Seek(0x2724, SeekOrigin.Begin);
            _bwriter.Write(sav.CurrentPCBoxNumber);

            // Boxes
            _bwriter.Seek(0x2727, SeekOrigin.Begin);
            for (int i = 0; i < 14; i++)
            {
                _bwriter.WriteString(sav.PCBoxNames[i], 9, _charset);
            }

            // Team
            _bwriter.Seek(0x288A, SeekOrigin.Begin);
            SerializePokeList(_bwriter, _charset, true, 6, sav.TeamPokemonList);

            // Pokedex
            _bwriter.Seek(0x2A4C, SeekOrigin.Begin);
            arr = new BitArray(sav.PokedexOwned);
            buffer = new byte[32];
            arr.CopyTo(buffer, 0);
            foreach (byte b in buffer)
            {
                _bwriter.Write(b);
            }

            _bwriter.Seek(0x2A6C, SeekOrigin.Begin);
            arr = new BitArray(sav.PokedexSeen);
            buffer = new byte[32];
            arr.CopyTo(buffer, 0);
            foreach (byte b in buffer)
            {
                _bwriter.Write(b);
            }

            // Current Box List
            _bwriter.Seek(0x2D6C, SeekOrigin.Begin);
            SerializePokeList(_bwriter, _charset, false, 20, sav.CurrentBoxPokemonlist);

            // GET 1-7 boxes
            for (int i = 0; i < 7; i++)
            {
                _bwriter.Seek(0x4000 + 0x450 * i, SeekOrigin.Begin);
                SerializePokeList(_bwriter, _charset, false, 20, sav.Boxes[i]);
            }

            // GET 8-14 boxes
            for (int i = 7; i < 14; i++)
            {
                _bwriter.Seek(0x6000 + 0x450 * (i - 7), SeekOrigin.Begin);
                SerializePokeList(_bwriter, _charset, false, 20, sav.Boxes[i]);
            }

            // Calculate checksum
            ushort checksum = 0;
            _breader.Seek(0x2009, SeekOrigin.Begin);
            while (_breader.Position <= 0x2D68)
            {
                checksum += _breader.ReadByte();
            }

            // Write Checksum 1 (0x2009 - 0x2D68)
            _bwriter.Seek(0x2D69, SeekOrigin.Begin);
            _bwriter.WriteInt16LittleEndian(checksum);
        }

        /// <summary>
        /// Completely serialized a given <see cref="Pokemon"/> into the file stream.
        /// </summary>
        private void SerializePokemon(IBinaryWriter2 bwriter, bool inBox, ICharset charset, Pokemon poke)
        {
            byte buffer;
            bwriter.Write(poke.SpeciesId);
            bwriter.Write(poke.HeldItem);
            bwriter.Write(poke.MoveIndex1);
            bwriter.Write(poke.MoveIndex2);
            bwriter.Write(poke.MoveIndex3);
            bwriter.Write(poke.MoveIndex4);
            bwriter.WriteUInt16(poke.TrainerId);
            bwriter.WriteUInt24(poke.Experience);
            bwriter.WriteUInt16(poke.HitPointsEV);
            bwriter.WriteUInt16(poke.AttackEV);
            bwriter.WriteUInt16(poke.DefenseEV);
            bwriter.WriteUInt16(poke.SpeedEV);
            bwriter.WriteUInt16(poke.SpecialEV);

            buffer = (byte)(poke.AttackIV << 0x4 | (0xf & poke.DefenseIV));
            bwriter.Write(buffer);

            buffer = (byte)(poke.SpeedIV << 0x4 | (0xf & poke.SpecialIV));
            bwriter.Write(buffer);

            buffer = (byte)(poke.Move1PowerPointsUps << 0x6 | (0x3f & poke.Move1PowerPointsCurrent));
            bwriter.Write(buffer);

            buffer = (byte)(poke.Move2PowerPointsUps << 0x6 | (0x3f & poke.Move2PowerPointsCurrent));
            bwriter.Write(buffer);

            buffer = (byte)(poke.Move3PowerPointsUps << 0x6 | (0x3f & poke.Move3PowerPointsCurrent));
            bwriter.Write(buffer);

            buffer = (byte)(poke.Move4PowerPointsUps << 0x6 | (0x3f & poke.Move4PowerPointsCurrent));
            bwriter.Write(buffer);

            bwriter.Write(poke.Friendship);

            buffer = (byte)(poke.PokerusStrain << 0x4 | (0xf & poke.PokerusDuration));
            bwriter.Write(buffer);

            buffer = (byte)(poke.CaughtTime << 0x6 | (0x3f & poke.CaughtLevel));
            bwriter.Write(buffer);

            buffer = (byte)(poke.OTGender << 0x7 | (0x7f & poke.CaughtLocation));
            bwriter.Write(buffer);

            bwriter.Write(poke.Level);

            if (!inBox)
            {
                bwriter.Write(poke.Status);
                bwriter.Write(poke.Unused);
                bwriter.WriteUInt16(poke.CurrentHp);
                bwriter.WriteUInt16(poke.MaxHp);
                bwriter.WriteUInt16(poke.Attack);
                bwriter.WriteUInt16(poke.Defense);
                bwriter.WriteUInt16(poke.Speed);
                bwriter.WriteUInt16(poke.SpAttack);
                bwriter.WriteUInt16(poke.SpDefense);
            }
        }

        /// <summary>
        /// Completely serialized a given <see cref="ItemList"/> into the file stream.
        /// </summary>
        private void SerializeItemList(IBinaryWriter2 bwriter, ICharset charset, int capacity, ItemList list, bool key = false)
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
        private void SerializePokeList(IBinaryWriter2 bwriter, ICharset charset, bool full, int capacity, PokeList list)
        {
            bwriter.Write(list.Count);

            // Species
            for (int i = 0; i < capacity + 1; i++)
            {
                if (i < list.Count)
                {
                    bwriter.Write(list.Pokemon[i].SpeciesId);
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
                    bwriter.WriteString(list.Pokemon[i].OTName, 11, charset);
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
                    bwriter.WriteString(list.Pokemon[i].Name, 11, charset);
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
        private void SerializeTMPocket(IBinaryWriter2 bwriter, ICharset charset, TMPocket pocket)
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