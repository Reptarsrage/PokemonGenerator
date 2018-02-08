using PokemonGenerator.Models.Serialization;
using System.Collections;
using System.IO;
using System.Linq;

namespace PokemonGenerator.Repositories
{
    /// <summary>
    /// Contains all logic needed to serialize data into a pokemon Gold/Silver sav file. <para/> 
    /// 
    /// See information available here for a detailed explaination: <para/> 
    /// http://bulbapedia.bulbagarden.net/wiki/Save_data_structure_in_Generation_II <para/> 
    /// http://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_structure_in_Generation_II
    /// </summary>
    public partial class SaveFileRepository
    {
        private void Serialize(SAVFileModel sav)
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
            var buffer = new byte[1];
            arr.CopyTo(buffer, 0);
            _bwriter.Write(buffer[0]);

            _bwriter.Seek(0x23E6, SeekOrigin.Begin);
            SerializeTmPocket(sav.TMpocket);

            _bwriter.Seek(0x241F, SeekOrigin.Begin);
            SerializeItemList(20, sav.PocketItemList);

            _bwriter.Seek(0x2449, SeekOrigin.Begin);
            SerializeItemList(26, sav.PocketKeyItemList, true);

            _bwriter.Seek(0x2464, SeekOrigin.Begin);
            SerializeItemList(12, sav.PocketBallItemList);

            _bwriter.Seek(0x247E, SeekOrigin.Begin);
            SerializeItemList(50, sav.PCItemList);

            _bwriter.Seek(0x2724, SeekOrigin.Begin);
            _bwriter.Write(sav.CurrentPCBoxNumber);

            // Boxes
            _bwriter.Seek(0x2727, SeekOrigin.Begin);
            for (var i = 0; i < 14; i++)
            {
                _bwriter.WriteString(sav.PCBoxNames[i], 9, _charset);
            }

            // Team
            _bwriter.Seek(0x288A, SeekOrigin.Begin);
            SerializePokeList(true, 6, sav.TeamPokemonList);

            // Pokedex
            _bwriter.Seek(0x2A4C, SeekOrigin.Begin);
            arr = new BitArray(sav.PokedexOwned);
            buffer = new byte[32];
            arr.CopyTo(buffer, 0);
            foreach (var b in buffer)
            {
                _bwriter.Write(b);
            }

            _bwriter.Seek(0x2A6C, SeekOrigin.Begin);
            arr = new BitArray(sav.PokedexSeen);
            buffer = new byte[32];
            arr.CopyTo(buffer, 0);
            foreach (var b in buffer)
            {
                _bwriter.Write(b);
            }

            // Current Box List
            _bwriter.Seek(0x2D6C, SeekOrigin.Begin);
            SerializePokeList(false, 20, sav.CurrentBoxPokemonlist);

            // GET 1-7 boxes
            for (var i = 0; i < 7; i++)
            {
                _bwriter.Seek(0x4000 + 0x450 * i, SeekOrigin.Begin);
                SerializePokeList(false, 20, sav.Boxes[i]);
            }

            // GET 8-14 boxes
            for (var i = 7; i < 14; i++)
            {
                _bwriter.Seek(0x6000 + 0x450 * (i - 7), SeekOrigin.Begin);
                SerializePokeList(false, 20, sav.Boxes[i]);
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
        private void SerializePokemon(bool inBox, Pokemon poke)
        {
            _bwriter.Write(poke.SpeciesId);
            _bwriter.Write(poke.HeldItem);
            _bwriter.Write(poke.MoveIndex1);
            _bwriter.Write(poke.MoveIndex2);
            _bwriter.Write(poke.MoveIndex3);
            _bwriter.Write(poke.MoveIndex4);
            _bwriter.WriteUInt16(poke.TrainerId);
            _bwriter.WriteUInt24(poke.Experience);
            _bwriter.WriteUInt16(poke.HitPointsEV);
            _bwriter.WriteUInt16(poke.AttackEV);
            _bwriter.WriteUInt16(poke.DefenseEV);
            _bwriter.WriteUInt16(poke.SpeedEV);
            _bwriter.WriteUInt16(poke.SpecialEV);

            var buffer = (byte)(poke.AttackIV << 0x4 | (0xf & poke.DefenseIV));
            _bwriter.Write(buffer);

            buffer = (byte)(poke.SpeedIV << 0x4 | (0xf & poke.SpecialIV));
            _bwriter.Write(buffer);

            buffer = (byte)(poke.Move1PowerPointsUps << 0x6 | (0x3f & poke.Move1PowerPointsCurrent));
            _bwriter.Write(buffer);

            buffer = (byte)(poke.Move2PowerPointsUps << 0x6 | (0x3f & poke.Move2PowerPointsCurrent));
            _bwriter.Write(buffer);

            buffer = (byte)(poke.Move3PowerPointsUps << 0x6 | (0x3f & poke.Move3PowerPointsCurrent));
            _bwriter.Write(buffer);

            buffer = (byte)(poke.Move4PowerPointsUps << 0x6 | (0x3f & poke.Move4PowerPointsCurrent));
            _bwriter.Write(buffer);

            _bwriter.Write(poke.Friendship);

            buffer = (byte)(poke.PokerusStrain << 0x4 | (0xf & poke.PokerusDuration));
            _bwriter.Write(buffer);

            buffer = (byte)(poke.CaughtTime << 0x6 | (0x3f & poke.CaughtLevel));
            _bwriter.Write(buffer);

            buffer = (byte)(poke.OTGender << 0x7 | (0x7f & poke.CaughtLocation));
            _bwriter.Write(buffer);

            _bwriter.Write(poke.Level);

            if (!inBox)
            {
                _bwriter.Write(poke.Status);
                _bwriter.Write(poke.Unused);
                _bwriter.WriteUInt16(poke.CurrentHp);
                _bwriter.WriteUInt16(poke.MaxHp);
                _bwriter.WriteUInt16(poke.Attack);
                _bwriter.WriteUInt16(poke.Defense);
                _bwriter.WriteUInt16(poke.Speed);
                _bwriter.WriteUInt16(poke.SpAttack);
                _bwriter.WriteUInt16(poke.SpDefense);
            }
        }

        /// <summary>
        /// Completely serialized a given <see cref="ItemList"/> into the file stream.
        /// </summary>
        private void SerializeItemList(int capacity, ItemList list, bool key = false)
        {
            _bwriter.Write(list.Count);

            for (var i = 0; i < capacity; i++)
            {
                if (i < list.ItemEntries.Length)
                {
                    var entry = list.ItemEntries[i];
                    _bwriter.Write(entry.Index);

                    if (!key)
                    {
                        _bwriter.Write(entry.Count);
                    }
                }
                else
                {
                    _bwriter.Write(0xff);

                    if (!key)
                    {
                        _bwriter.Write(0xff);
                    }
                }
            }
            _bwriter.Write(list.Terminator);
        }

        /// <summary>
        /// Completely serialized a given <see cref="PokeList"/> into the file stream.
        /// </summary>
        private void SerializePokeList(bool full, int capacity, PokeList list)
        {
            _bwriter.Write(list.Count);

            // Species
            for (var i = 0; i < capacity + 1; i++)
            {
                _bwriter.Write(i < list.Count ? list.Pokemon[i].SpeciesId : (byte) 0xff);
            }

            // Pokemon
            for (var i = 0; i < capacity; i++)
            {
                if (i < list.Count)
                {
                    SerializePokemon(!full, list.Pokemon[i]);
                }
                else
                {
                    _bwriter.Fill(0xff, (full ? 48 : 32)); // sizeof pokemon
                }
            }

            // OT Names
            for (var i = 0; i < capacity; i++)
            {
                if (i < list.Count)
                {
                    _bwriter.WriteString(list.Pokemon[i].OTName, 11, _charset);
                }
                else
                {
                    _bwriter.Fill(0xff, 11);
                }
            }

            // Names
            for (var i = 0; i < capacity; i++)
            {
                if (i < list.Count)
                {
                    _bwriter.WriteString(list.Pokemon[i].Name, 11, _charset);
                }
                else
                {
                    _bwriter.Fill(0xff, 11);
                }
            }
        }

        /// <summary>
        /// Completely serialized a given <see cref="TMPocket"/> into the file stream.
        /// </summary>
        private void SerializeTmPocket(TMPocket pocket)
        {
            for (var i = 0; i < 50; i++)
            {
                _bwriter.Write(pocket.TMs[i]);
            }

            for (var i = 0; i < 7; i++)
            {
                _bwriter.Write(pocket.HMs[i]);
            }
        }
    }
}