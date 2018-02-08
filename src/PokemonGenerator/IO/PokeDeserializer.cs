using PokemonGenerator.Models.Serialization;
using System.Collections;
using System.IO;
using System.Linq;

namespace PokemonGenerator.IO
{
    public interface IPokeDeserializer
    {
        SAVFileModel ParseSAVFileModel(string filename);
        SAVFileModel ParseSAVFileModel(Stream stream);
    }

    /// <summary>
    /// Contains all tools needed to parse data from a pokemon Gold/Silver sav file. <para/> 
    /// 
    /// See information available here for a detailed explaination: <para/> 
    /// http://bulbapedia.bulbagarden.net/wiki/Save_data_structure_in_Generation_II <para/> 
    /// http://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_structure_in_Generation_II
    /// </summary>
    public class PokeDeserializer : IPokeDeserializer
    {
        private readonly IBinaryReader2 breader;
        private readonly ICharset charset;

        public PokeDeserializer(IBinaryReader2 breader, ICharset charset)
        {
            this.breader = breader;
            this.charset = charset;
        }

        /// <summary>
        /// Completely parses a given file stream into a <see cref="SAVFileModel"/> .
        /// </summary>
        public SAVFileModel ParseSAVFileModel(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                breader.Open(stream);
                var ret = ParseSAVFileModel();
                breader.Close();
                return ret;
            }
        }

        /// <summary>
        /// Completely parses a given file stream into a <see cref="SAVFileModel"/> .
        /// </summary>
        public SAVFileModel ParseSAVFileModel(Stream stream)
        {
            breader.Open(stream);
            var ret = ParseSAVFileModel();
            breader.Close();
            return ret;
        }

        /// <summary>
        /// Completely parses a given file stream into a <see cref="SAVFileModel"/> .
        /// </summary>
        private SAVFileModel ParseSAVFileModel()
        {
            SAVFileModel sav = new SAVFileModel();

            breader.Seek(0x2000, SeekOrigin.Begin);
            sav.Options = breader.ReadUInt64();

            breader.Seek(0x2009, SeekOrigin.Begin);
            sav.PlayerTrainerID = breader.ReadUInt16();

            breader.Seek(0x200B, SeekOrigin.Begin);
            sav.PlayerName = breader.ReadString(11, charset);

            breader.Seek(0x2021, SeekOrigin.Begin);
            sav.RivalName = breader.ReadString(11, charset);

            breader.Seek(0x2037, SeekOrigin.Begin);
            sav.Daylightsavings = (breader.ReadByte() & 0x80) == 1;

            breader.Seek(0x2053, SeekOrigin.Begin);
            sav.TimePlayed = breader.ReadUInt32();

            breader.Seek(0x206B, SeekOrigin.Begin);
            sav.Playerpalette = breader.ReadByte();

            breader.Seek(0x23DB, SeekOrigin.Begin);
            sav.Money = breader.ReadUInt24();

            breader.Seek(0x23E4, SeekOrigin.Begin);
            sav.JohtoBadges = breader.ReadByte();

            BitArray arr = new BitArray(new byte[] { sav.JohtoBadges });
            sav.JohtoBadges = (byte)arr.Cast<bool>().Count(b => b);

            breader.Seek(0x23E6, SeekOrigin.Begin);
            sav.TMpocket = this.ParseTMPocket(breader, charset);

            breader.Seek(0x241F, SeekOrigin.Begin);
            sav.PocketItemList = this.ParseItemList(breader, charset, 20);

            breader.Seek(0x2449, SeekOrigin.Begin);
            sav.PocketKeyItemList = this.ParseItemList(breader, charset, 26, true);

            breader.Seek(0x2464, SeekOrigin.Begin);
            sav.PocketBallItemList = this.ParseItemList(breader, charset, 12);

            breader.Seek(0x247E, SeekOrigin.Begin);
            sav.PCItemList = this.ParseItemList(breader, charset, 50);

            breader.Seek(0x2724, SeekOrigin.Begin);
            sav.CurrentPCBoxNumber = breader.ReadByte();

            // Boxes
            breader.Seek(0x2727, SeekOrigin.Begin);
            sav.PCBoxNames = new string[14];
            for (int i = 0; i < 14; i++)
            {
                sav.PCBoxNames[i] = breader.ReadString(9, charset);
            }

            // Team
            breader.Seek(0x288A, SeekOrigin.Begin);
            sav.TeamPokemonList = ParsePokeList(breader, charset, true, 6);

            // Pokedex
            breader.Seek(0x2A4C, SeekOrigin.Begin);
            sav.PokedexOwned = new bool[32 * 8];
            byte[] pokedex = breader.ReadBytes(32);
            arr = new BitArray(pokedex);
            arr.CopyTo(sav.PokedexOwned, 0);

            breader.Seek(0x2A6C, SeekOrigin.Begin);
            sav.PokedexSeen = new bool[32 * 8];
            pokedex = breader.ReadBytes(32);
            arr = new BitArray(pokedex);
            arr.CopyTo(sav.PokedexSeen, 0);

            // Current Box List
            breader.Seek(0x2D6C, SeekOrigin.Begin);
            sav.CurrentBoxPokemonlist = ParsePokeList(breader, charset, false, 20);

            // GET 1-7 boxes
            sav.Boxes = new PokeList[14];

            for (int i = 0; i < 7; i++)
            {
                breader.Seek(0x4000 + 0x450 * i, SeekOrigin.Begin);
                sav.Boxes[i] = ParsePokeList(breader, charset, false, 20);
            }

            // GET 8-14 boxes
            for (int i = 7; i < 14; i++)
            {
                breader.Seek(0x6000 + 0x450 * (i - 7), SeekOrigin.Begin);
                sav.Boxes[i] = ParsePokeList(breader, charset, false, 20);
            }

            // Checksum 0x2009 - 0x2D68
            breader.Seek(0x2D69, SeekOrigin.Begin);
            sav.Checksum1 = breader.ReadUInt16LittleEndian();

            // Calculate checksum
            breader.Seek(0x2009, SeekOrigin.Begin);
            ushort checksum = 0;
            while (breader.Position <= 0x2D68)
            {
                checksum += breader.ReadByte();
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
        /// <param name="breader">Steam</param>
        /// <param name="inBox">Is the pokemon in a box or on a team?</param>
        /// <param name="charset">The charset to use for string literals</param>
        private Pokemon ParsePokemon(IBinaryReader2 breader, bool inBox, ICharset charset)
        {
            byte buffer;
            var pokemon = new Pokemon
            {
                SpeciesId = breader.ReadByte(),
                HeldItem = breader.ReadByte(),
                MoveIndex1 = breader.ReadByte(),
                MoveIndex2 = breader.ReadByte(),
                MoveIndex3 = breader.ReadByte(),
                MoveIndex4 = breader.ReadByte(),
                TrainerId = breader.ReadUInt16(),
                Experience = breader.ReadUInt24(),
                HitPointsEV = breader.ReadUInt16(),
                AttackEV = breader.ReadUInt16(),
                DefenseEV = breader.ReadUInt16(),
                SpeedEV = breader.ReadUInt16(),
                SpecialEV = breader.ReadUInt16()
            };

            buffer = breader.ReadByte();
            pokemon.AttackIV = (byte)(buffer >> 4);
            pokemon.DefenseIV = (byte)(0xf & buffer);

            buffer = breader.ReadByte();
            pokemon.SpeedIV = (byte)(buffer >> 4);
            pokemon.SpecialIV = (byte)(0xf & buffer);

            buffer = breader.ReadByte();
            pokemon.Move1PowerPointsUps = (byte)(buffer >> 6);
            pokemon.Move1PowerPointsCurrent = (byte)(0x3f & buffer);

            buffer = breader.ReadByte();
            pokemon.Move2PowerPointsUps = (byte)(buffer >> 6);
            pokemon.Move2PowerPointsCurrent = (byte)(0x3f & buffer);

            buffer = breader.ReadByte();
            pokemon.Move3PowerPointsUps = (byte)(buffer >> 6);
            pokemon.Move3PowerPointsCurrent = (byte)(0x3f & buffer);

            buffer = breader.ReadByte();
            pokemon.Move4PowerPointsUps = (byte)(buffer >> 6);
            pokemon.Move4PowerPointsCurrent = (byte)(0x3f & buffer);

            pokemon.Friendship = breader.ReadByte();

            buffer = breader.ReadByte();
            pokemon.PokerusStrain = (byte)(buffer >> 4);
            pokemon.PokerusDuration = (byte)(0xf & buffer);

            buffer = breader.ReadByte();
            pokemon.CaughtTime = (byte)(buffer >> 6);
            pokemon.CaughtLevel = (byte)(0x3f & buffer);

            buffer = breader.ReadByte();
            pokemon.OTGender = (byte)(buffer >> 7);
            pokemon.CaughtLocation = (byte)(0x7f & buffer);

            pokemon.Level = breader.ReadByte();

            if (!inBox)
            {
                pokemon.Status = breader.ReadByte();
                pokemon.Unused = breader.ReadByte();
                pokemon.CurrentHp = breader.ReadUInt16();
                pokemon.MaxHp = breader.ReadUInt16();
                pokemon.Attack = breader.ReadUInt16();
                pokemon.Defense = breader.ReadUInt16();
                pokemon.Speed = breader.ReadUInt16();
                pokemon.SpAttack = breader.ReadUInt16();
                pokemon.SpDefense = breader.ReadUInt16();
            }

            return pokemon;
        }

        /// <summary>
        /// Completely parses a given file stream into a <see cref="ItemList"/> .
        /// </summary>
        private ItemList ParseItemList(IBinaryReader2 breader, ICharset charset, int capacity, bool key = false)
        {
            var list = new ItemList(capacity)
            {
                Count = breader.ReadByte()
            };
            list.ItemEntries = new ItemEntry[list.Count];

            for (int i = 0; i < capacity; i++)
            {
                var entry = new ItemEntry
                {
                    Index = breader.ReadByte()
                };

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
        private PokeList ParsePokeList(IBinaryReader2 breader, ICharset charset, bool full, int capacity)
        {
            var list = new PokeList(capacity)
            {
                Count = breader.ReadByte()
            };
            list.Pokemon = new Pokemon[list.Count];

            // Species
            for (int i = 0; i < capacity + 1; i++)
            {
                if (i < list.Count)
                {
                    list.Pokemon[i] = new Pokemon();
                    list.Pokemon[i].SpeciesId = breader.ReadByte();
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
                    temp.SpeciesId = list.Pokemon[i].SpeciesId;
                    list.Pokemon[i] = temp;
                }
                else
                {
                    breader.Seek(full ? 48 : 32, SeekOrigin.Current); // sizeof pokemon
                }
            }

            // OT Names
            for (int i = 0; i < capacity; i++)
            {
                if (i < list.Count)
                {
                    list.Pokemon[i].OTName = breader.ReadString(11, charset);
                }
                else
                {
                    breader.ReadString(11, charset);
                }
            }

            // Names
            for (int i = 0; i < capacity; i++)
            {
                if (i < list.Count)
                {
                    list.Pokemon[i].Name = breader.ReadString(11, charset);
                }
                else
                {
                    breader.ReadString(11, charset);
                }
            }
            return list;
        }

        /// <summary>
        /// Completely parses a given file stream into a <see cref="TMPocket"/> .
        /// </summary>
        private TMPocket ParseTMPocket(IBinaryReader2 breader, ICharset charset)
        {
            var pocket = new TMPocket();
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
    }
}