using System;
using System.Text;

namespace PokemonGenerator.Models.Serialization
{
    /// <summary>
    /// A modal used to store and serialize/deserialize information.
    /// </summary>
    public class SaveFileModel
    {
        /// Property Name  |  Length (in bytes)
        public ulong Options; // 8
        public uint PlayerTrainerID; // 2
        public string PlayerName; // 11
        public string UnusedPlayersmomname; // 11
        public string RivalName; // 11
        public string UnusedRedsname; // 11
        public string UnusedBluesname; // 11
        public bool Daylightsavings; // 1
        public uint TimePlayed; // 4
        public byte Playerpalette; // 1
        public uint Money; // 3
        public byte JohtoBadges; // 1
        public TMPocket TMpocket; // 57
        public ItemList PocketItemList; // 42 (capacity 20)
        public ItemList PocketKeyItemList; // 54 (capacity 26)
        public ItemList PocketBallItemList; // 26 (capacity 12)
        public ItemList PCItemList; // 102 (capacity 50)
        public byte CurrentPCBoxNumber; // 1 (ignore 4 high bits)
        public string[] PCBoxNames; // 126 (14 box names * 9 bytes each)
        public PokeList TeamPokemonList; // 428
        public bool[] PokedexOwned; // 32
        public bool[] PokedexSeen; // 32
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

            builder.AppendLine($"Name: {PlayerName} (ID {PlayerTrainerID})");
            builder.AppendLine($"Rival: {RivalName}");
            builder.AppendLine($"Daylight Savings: {Daylightsavings}");
            builder.AppendLine($"PlayTime: {(TimePlayed >> 24)}:{(TimePlayed >> 16 & 0xff)}:{(TimePlayed >> 8 & 0xff)}:{(TimePlayed >> 24 & 0xff)}");
            builder.AppendLine($"pallet: {Playerpalette}");
            builder.AppendLine($"Money: ${Money}");
            builder.AppendLine($"Badges: {JohtoBadges}");

            builder.AppendLine(TMpocket.ToString());
            builder.AppendLine("Items in Pocket:");
            builder.AppendLine(PocketItemList.ToString());
            builder.AppendLine("Key items in Pocket:");
            builder.AppendLine(PocketKeyItemList.ToString());
            builder.AppendLine("Balls in Pocket (kek):");
            builder.AppendLine(PocketBallItemList.ToString());
            builder.AppendLine("Items in PC:");
            builder.AppendLine(PCItemList.ToString());

            builder.AppendLine("Boxes:");
            for (int i = 0; i < PCBoxNames.Length; i++)
            {
                if (i == CurrentPCBoxNumber)
                {
                    builder.Append(PCBoxNames[i]);
                    builder.AppendLine(" <-- CURRENT");
                }
                else
                {
                    builder.AppendLine(PCBoxNames[i]);
                }
                if (Boxes[i] != null)
                {
                    foreach (var poke in Boxes[i].Pokemon)
                    {
                        builder.AppendLine($"\t{poke.Name}");
                    }

                }
            }

            for (int i = 0; i < TeamPokemonList.Count; i++)
            {
                builder.AppendLine(TeamPokemonList.Pokemon[i].ToString());
            }

            builder.AppendLine("Pokedex:");
            for (int i = 0; i < Math.Min(this.PokedexOwned.Length, this.PokedexSeen.Length); i++)
            {
                builder.Append($"No. {i + 1} ");
                if (this.PokedexOwned[i])
                {
                    builder.Append("Owned");
                }
                else if (this.PokedexSeen[i])
                {
                    builder.Append("Seen");
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}