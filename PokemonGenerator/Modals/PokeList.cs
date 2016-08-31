/// <summary>
/// Author: Justin Robb
/// Date: 8/30/2016
/// 
/// Description:
/// Generates a team of six Gen II pokemon for use in Pokemon Gold or Silver.
/// Built in order to supply Pokemon Stadium 2 with a better selection of Pokemon.
/// 
/// </summary>

namespace PokemonGenerator.Modals
{
    using System.Text;

    /// <summary>
    /// A modal used to store and serialize/deserialize information.
    /// </summary>
    class PokeList
    {
        public byte Count;
        public byte[] Species; // Capactiy bytes + 1 byte terminator 0xFF
        public Pokemon[] Pokemon; // Capacity * 49
        public string[] OTNames; // Capacity * 11
        public string[] Names; // Capacity * 11
        private int v;

        public PokeList(int v)
        {
            this.Pokemon = new Pokemon[v];
            this.Species = new byte[v];
            this.Count = (byte)v;
            this.Names = new string[v];
            this.OTNames = new string[v];
        }

        /// <summary>
        /// Prettry prints the PokeList contents (short version)
        /// </summary>
        public override string ToString()
        {
            StringBuilder b = new StringBuilder();

            int idx = 0;
            foreach (Pokemon p in this.Pokemon) {
                //b.AppendLine(Names[idx]);
                b.AppendLine(p.ToString());
                b.AppendLine("\n");
               idx++;
            }

            return b.ToString();
        }

        /// <summary>
        /// Prettry prints the PokeList contents (long version)
        /// </summary>
        public string ToShortString()
        {
            StringBuilder b = new StringBuilder();

            int idx = 0;
            foreach (Pokemon p in this.Pokemon)
            {
                b.Append(p.Name);
                b.Append("\t");
                b.AppendLine(string.Join(",", p.Types.ToArray()));

                b.Append("\t");
                b.AppendLine(p.MoveName1);

                b.Append("\t");
                b.AppendLine(p.MoveName2);

                b.Append("\t");
                b.AppendLine(p.MoveName3);

                b.Append("\t");
                b.AppendLine(p.MoveName4);
                b.Append("\n");
            }

            return b.ToString();
        }
    }
}
