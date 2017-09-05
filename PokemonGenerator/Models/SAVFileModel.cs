using System;
using System.Text;

namespace PokemonGenerator.Models
{
    /// <summary>
    /// A modal used to store and serialize/deserialize information.
    /// </summary>
    public class SAVFileModel
    {
        /// Property Name  |  Length (in bytes)
        public ulong Options; // 8
        public uint PlayerTrainerID; // 2
        public string Playername; // 11
        public string UnusedPlayersmomname; // 11
        public string Rivalname; // 11
        public string UnusedRedsname; // 11
        public string UnusedBluesname; // 11
        public bool Daylightsavings; // 1
        public uint Timeplayed; // 4
        public byte Playerpalette; // 1
        public uint Money; // 3
        public byte JohtoBadges; // 1
        public TMPocket TMpocket; // 57
        public ItemList Itempocketitemlist; // 42 (capacity 20)
        public ItemList Keyitempocketitemlist; // 54 (capacity 26)
        public ItemList Ballpocketitemlist; // 26 (capacity 12)
        public ItemList PCitemlist; // 102 (capacity 50)
        public byte CurrentPCBoxnumber; // 1 (ignore 4 high bits)
        public string[] PCBoxnames; // 126 (14 box names * 9 bytes each)
        public PokeList TeamPokemonlist; // 428
        public bool[] Pokédexowned; // 32
        public bool[] Pokédexseen; // 32
        public PokeList CurrentBoxPokemonlist; // 1102
        public byte Playergender; // 1
        public PokeList[] Boxes; // 1102 * 14
        public ushort Checksum1; // 2
        public ushort Checksum2; // 2

        /// <summary>
        /// Pretty prints the SAV file contents.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"Name: {Playername} (ID {PlayerTrainerID})");
            builder.AppendLine($"Rival: {Rivalname}");
            builder.AppendLine($"Daylight Savings: {Daylightsavings}");
            builder.AppendLine($"PlayTime: {(Timeplayed >> 24)}:{(Timeplayed >> 16 & 0xff)}:{(Timeplayed >> 8 & 0xff)}:{(Timeplayed >> 24 & 0xff)}");
            builder.AppendLine($"pallet: {Playerpalette}");
            builder.AppendLine($"Money: ${Money}");
            builder.AppendLine($"Badges: {JohtoBadges}");

            builder.AppendLine(TMpocket.ToString());
            builder.AppendLine("Items in Pocket:");
            builder.AppendLine(Itempocketitemlist.ToString());
            builder.AppendLine("Key items in Pocket:");
            builder.AppendLine(Keyitempocketitemlist.ToString());
            builder.AppendLine("Balls in Pocket (kek):");
            builder.AppendLine(Ballpocketitemlist.ToString());
            builder.AppendLine("Items in PC:");
            builder.AppendLine(PCitemlist.ToString());

            builder.AppendLine("Boxes:");
            for (int i = 0; i < PCBoxnames.Length; i++)
            {
                if (i == CurrentPCBoxnumber)
                {
                    builder.Append(PCBoxnames[i]);
                    builder.AppendLine(" <-- CURRENT");
                }
                else
                {
                    builder.AppendLine(PCBoxnames[i]);
                }
                if (Boxes[i] != null)
                {
                    foreach (var poke in Boxes[i].Pokemon)
                    {
                        builder.AppendLine($"\t{poke.Name}");
                    }

                }
            }

            for (int i = 0; i < TeamPokemonlist.Count; i++)
            {
                builder.AppendLine(TeamPokemonlist.Pokemon[i].ToString());
            }

            builder.AppendLine("Pokedex:");
            for (int i = 0; i < Math.Min(this.Pokédexowned.Length, this.Pokédexseen.Length); i++)
            {
                builder.Append($"No. {i + 1} ");
                if (this.Pokédexowned[i])
                {
                    builder.Append("Owned");
                }
                else if (this.Pokédexseen[i])
                {
                    builder.Append("Seen");
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}