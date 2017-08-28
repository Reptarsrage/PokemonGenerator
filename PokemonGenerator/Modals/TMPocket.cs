using System.Text;

namespace PokemonGenerator.Modals
{
    /// <summary>
    /// A modal used to store and serialize/deserialize information.
    /// </summary>
    internal class TMPocket
    {
        public byte[] TMs; // 50
        public byte[] HMs; // 7

        public TMPocket()
        {
            TMs = new byte[50];
            HMs = new byte[7];
        }

        /// <summary>
        /// Prettry prints the TMPocket contents
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < TMs.Length; i++)
            {
                if (TMs[i] > 0)
                {
                    builder.AppendLine($"\tTM{i + 1} x({TMs[i]})");
                }
            }
            for (int i = 0; i < HMs.Length; i++)
            {
                if (HMs[i] > 0)
                {
                    builder.AppendLine($"\tHM{i + 1} x({HMs[i]})");
                }
            }
            return builder.ToString();
        }
    }
}