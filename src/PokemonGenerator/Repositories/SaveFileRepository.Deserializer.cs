using PokemonGenerator.Models.Serialization;
using System.Collections;
using System.IO;
using System.Linq;

namespace PokemonGenerator.Repositories
{
    public partial class SaveFileRepository
    {
        /// <summary>
        /// Contains all logic needed to parse data from a pokemon Gold/Silver sav file. 
        /// 
        /// See information available here for a detailed explaination: 
        /// http://bulbapedia.bulbagarden.net/wiki/Save_data_structure_in_Generation_II 
        /// http://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_structure_in_Generation_II
        /// </summary>
        private SaveFileModel Deserialize()
        {
            var sav = new SaveFileModel();

            _breader.Seek(0x2000, SeekOrigin.Begin);
            sav.Options = _breader.ReadUInt64();

            _breader.Seek(0x2009, SeekOrigin.Begin);
            sav.PlayerTrainerID = _breader.ReadUInt16();

            _breader.Seek(0x200B, SeekOrigin.Begin);
            sav.PlayerName = _breader.ReadString(11, _charset);

            _breader.Seek(0x2021, SeekOrigin.Begin);
            sav.RivalName = _breader.ReadString(11, _charset);

            _breader.Seek(0x2037, SeekOrigin.Begin);
            sav.Daylightsavings = (_breader.ReadByte() & 0x80) == 1;

            _breader.Seek(0x2053, SeekOrigin.Begin);
            sav.TimePlayed = _breader.ReadUInt32();

            _breader.Seek(0x206B, SeekOrigin.Begin);
            sav.Playerpalette = _breader.ReadByte();

            _breader.Seek(0x23DB, SeekOrigin.Begin);
            sav.Money = _breader.ReadUInt24();

            _breader.Seek(0x23E4, SeekOrigin.Begin);
            sav.JohtoBadges = _breader.ReadByte();

            var arr = new BitArray(new[] { sav.JohtoBadges });
            sav.JohtoBadges = (byte)arr.Cast<bool>().Count(b => b);

            _breader.Seek(0x23E6, SeekOrigin.Begin);
            sav.TMpocket = ParseTMPocket();

            _breader.Seek(0x241F, SeekOrigin.Begin);
            sav.PocketItemList = ParseItemList(20);

            _breader.Seek(0x2449, SeekOrigin.Begin);
            sav.PocketKeyItemList = ParseItemList(26, true);

            _breader.Seek(0x2464, SeekOrigin.Begin);
            sav.PocketBallItemList = ParseItemList(12);

            _breader.Seek(0x247E, SeekOrigin.Begin);
            sav.PCItemList = ParseItemList(50);

            _breader.Seek(0x2724, SeekOrigin.Begin);
            sav.CurrentPCBoxNumber = _breader.ReadByte();

            // Boxes
            _breader.Seek(0x2727, SeekOrigin.Begin);
            sav.PCBoxNames = new string[14];
            for (var i = 0; i < 14; i++)
            {
                sav.PCBoxNames[i] = _breader.ReadString(9, _charset);
            }

            // Team
            _breader.Seek(0x288A, SeekOrigin.Begin);
            sav.TeamPokemonList = ParsePokeList(true, 6);

            // Pokedex
            _breader.Seek(0x2A4C, SeekOrigin.Begin);
            sav.PokedexOwned = new bool[32 * 8];
            var pokedex = _breader.ReadBytes(32);
            arr = new BitArray(pokedex);
            arr.CopyTo(sav.PokedexOwned, 0);

            _breader.Seek(0x2A6C, SeekOrigin.Begin);
            sav.PokedexSeen = new bool[32 * 8];
            pokedex = _breader.ReadBytes(32);
            arr = new BitArray(pokedex);
            arr.CopyTo(sav.PokedexSeen, 0);

            // Current Box List
            _breader.Seek(0x2D6C, SeekOrigin.Begin);
            sav.CurrentBoxPokemonlist = ParsePokeList(false, 20);

            // GET 1-7 boxes
            sav.Boxes = new PokeList[14];

            for (var i = 0; i < 7; i++)
            {
                _breader.Seek(0x4000 + 0x450 * i, SeekOrigin.Begin);
                sav.Boxes[i] = ParsePokeList(false, 20);
            }

            // GET 8-14 boxes
            for (var i = 7; i < 14; i++)
            {
                _breader.Seek(0x6000 + 0x450 * (i - 7), SeekOrigin.Begin);
                sav.Boxes[i] = ParsePokeList(false, 20);
            }

            // Checksum 0x2009 - 0x2D68
            _breader.Seek(0x2D69, SeekOrigin.Begin);
            sav.Checksum1 = _breader.ReadUInt16LittleEndian();

            // Calculate checksum
            _breader.Seek(0x2009, SeekOrigin.Begin);
            ushort checksum = 0;
            while (_breader.Position <= 0x2D68)
            {
                checksum += _breader.ReadByte();
            }

            if (checksum != sav.Checksum1)
            {
                throw new InvalidDataException("Checksum doesn't match. Data possibly corrupt.");
            }

            return sav;
        }

        /// <summary>
        /// Completely parses a given file stream into a <see cref="Pokemon"/> .
        /// </summary>
        /// <param name="_breader">Steam</param>
        /// <param name="inBox">Is the pokemon in a box or on a team?</param>
        /// <param name="_charset">The _charset to use for string literals</param>
        private Pokemon ParsePokemon(bool inBox)
        {
            var pokemon = new Pokemon
            {
                SpeciesId = _breader.ReadByte(),
                HeldItem = _breader.ReadByte(),
                MoveIndex1 = _breader.ReadByte(),
                MoveIndex2 = _breader.ReadByte(),
                MoveIndex3 = _breader.ReadByte(),
                MoveIndex4 = _breader.ReadByte(),
                TrainerId = _breader.ReadUInt16(),
                Experience = _breader.ReadUInt24(),
                HitPointsEV = _breader.ReadUInt16(),
                AttackEV = _breader.ReadUInt16(),
                DefenseEV = _breader.ReadUInt16(),
                SpeedEV = _breader.ReadUInt16(),
                SpecialEV = _breader.ReadUInt16()
            };

            var buffer = _breader.ReadByte();
            pokemon.AttackIV = (byte)(buffer >> 4);
            pokemon.DefenseIV = (byte)(0xf & buffer);

            buffer = _breader.ReadByte();
            pokemon.SpeedIV = (byte)(buffer >> 4);
            pokemon.SpecialIV = (byte)(0xf & buffer);

            buffer = _breader.ReadByte();
            pokemon.Move1PowerPointsUps = (byte)(buffer >> 6);
            pokemon.Move1PowerPointsCurrent = (byte)(0x3f & buffer);

            buffer = _breader.ReadByte();
            pokemon.Move2PowerPointsUps = (byte)(buffer >> 6);
            pokemon.Move2PowerPointsCurrent = (byte)(0x3f & buffer);

            buffer = _breader.ReadByte();
            pokemon.Move3PowerPointsUps = (byte)(buffer >> 6);
            pokemon.Move3PowerPointsCurrent = (byte)(0x3f & buffer);

            buffer = _breader.ReadByte();
            pokemon.Move4PowerPointsUps = (byte)(buffer >> 6);
            pokemon.Move4PowerPointsCurrent = (byte)(0x3f & buffer);

            pokemon.Friendship = _breader.ReadByte();

            buffer = _breader.ReadByte();
            pokemon.PokerusStrain = (byte)(buffer >> 4);
            pokemon.PokerusDuration = (byte)(0xf & buffer);

            buffer = _breader.ReadByte();
            pokemon.CaughtTime = (byte)(buffer >> 6);
            pokemon.CaughtLevel = (byte)(0x3f & buffer);

            buffer = _breader.ReadByte();
            pokemon.OTGender = (byte)(buffer >> 7);
            pokemon.CaughtLocation = (byte)(0x7f & buffer);

            pokemon.Level = _breader.ReadByte();

            if (!inBox)
            {
                pokemon.Status = _breader.ReadByte();
                pokemon.Unused = _breader.ReadByte();
                pokemon.CurrentHp = _breader.ReadUInt16();
                pokemon.MaxHp = _breader.ReadUInt16();
                pokemon.Attack = _breader.ReadUInt16();
                pokemon.Defense = _breader.ReadUInt16();
                pokemon.Speed = _breader.ReadUInt16();
                pokemon.SpAttack = _breader.ReadUInt16();
                pokemon.SpDefense = _breader.ReadUInt16();
            }

            return pokemon;
        }

        /// <summary>
        /// Completely parses a given file stream into a <see cref="ItemList"/> .
        /// </summary>
        private ItemList ParseItemList(int capacity, bool key = false)
        {
            var list = new ItemList(capacity)
            {
                Count = _breader.ReadByte()
            };
            list.ItemEntries = new ItemEntry[list.Count];

            for (var i = 0; i < capacity; i++)
            {
                var entry = new ItemEntry
                {
                    Index = _breader.ReadByte()
                };

                if (key)
                {
                    entry.Count = 1;
                }
                else
                {
                    entry.Count = _breader.ReadByte();
                }

                if (i < list.Count)
                {
                    list.ItemEntries[i] = entry;
                }
            }
            list.Terminator = _breader.ReadByte();
            return list;
        }

        /// <summary>
        /// Completely parses a given file stream into a <see cref="PokeList"/> .
        /// </summary>
        private PokeList ParsePokeList(bool full, int capacity)
        {
            var list = new PokeList(capacity)
            {
                Count = _breader.ReadByte()
            };
            list.Pokemon = new Pokemon[list.Count];

            // Species
            for (var i = 0; i < capacity + 1; i++)
            {
                if (i < list.Count)
                {
                    list.Pokemon[i] = new Pokemon();
                    list.Pokemon[i].SpeciesId = _breader.ReadByte();
                }
                else
                {
                    _breader.ReadByte();
                }
            }

            // Pokemon
            for (var i = 0; i < capacity; i++)
            {
                if (i < list.Count)
                {
                    Pokemon temp = ParsePokemon(!full);
                    temp.SpeciesId = list.Pokemon[i].SpeciesId;
                    list.Pokemon[i] = temp;
                }
                else
                {
                    _breader.Seek(full ? 48 : 32, SeekOrigin.Current); // sizeof pokemon
                }
            }

            // OT Names
            for (var i = 0; i < capacity; i++)
            {
                if (i < list.Count)
                {
                    list.Pokemon[i].OTName = _breader.ReadString(11, _charset);
                }
                else
                {
                    _breader.ReadString(11, _charset);
                }
            }

            // Names
            for (var i = 0; i < capacity; i++)
            {
                if (i < list.Count)
                {
                    list.Pokemon[i].Name = _breader.ReadString(11, _charset);
                }
                else
                {
                    _breader.ReadString(11, _charset);
                }
            }
            return list;
        }

        /// <summary>
        /// Completely parses a given file stream into a <see cref="TMPocket"/> .
        /// </summary>
        private TMPocket ParseTMPocket()
        {
            var pocket = new TMPocket();
            for (var i = 0; i < 50; i++)
            {
                pocket.TMs[i] = _breader.ReadByte();
            }

            for (var i = 0; i < 7; i++)
            {
                pocket.HMs[i] = _breader.ReadByte();
            }
            return pocket;
        }
    }
}