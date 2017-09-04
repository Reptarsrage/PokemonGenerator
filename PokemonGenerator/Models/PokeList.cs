using System.Linq;
using System.Text;

namespace PokemonGenerator.Models
{
    /// <summary>
    /// A modal used to store and serialize/deserialize information.
    /// </summary>
    public class PokeList
    {
        public byte Count;
        public byte[] Species; // Capactiy bytes + 1 byte terminator 0xFF
        public Pokemon[] Pokemon; // Capacity * 49
        public string[] OTNames; // Capacity * 11
        public string[] Names; // Capacity * 11

        public PokeList(int count)
        {
            Pokemon = new Pokemon[count];
            Species = new byte[count];
            Count = (byte)count;
            Names = new string[count];
            OTNames = new string[count];
        }

        /// <summary>
        /// Prettry prints the PokeList contents (short version)
        /// </summary>
        public override string ToString()
        {
            var b = new StringBuilder();
            var idx = 0;
            foreach (Pokemon p in Pokemon)
            {
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
            var b = new StringBuilder();
            foreach (Pokemon p in Pokemon)
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