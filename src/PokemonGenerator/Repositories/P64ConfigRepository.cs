using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PokemonGenerator.Repositories
{
    /// <summary>
    /// Reads Projects 64 Config file to get some info to auto-fill some forms.
    /// </summary>
    public interface IP64ConfigRepository
    {
        /// <summary>
        /// Path to the Project64 config file
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// Gets the most recently played ROM.
        /// </summary>
        string GetRecentRom();
    }

    /// <inheritdoc />
    public class P64ConfigRepository : IP64ConfigRepository
    {
        private string fileName;

        public P64ConfigRepository()
        {
            fileName = string.Empty;
        }

        /// <inheritdoc />
        public string FileName
        {
            get => fileName;
            set => fileName = File.Exists(value) ? value : throw new FileNotFoundException($"{value} file not found.");
        }

        /// <inheritdoc />
        public string GetRecentRom()
        {
            FileName = fileName; // quick validation
            using (var file = File.OpenRead(this.FileName))
            using (var stream = new StreamReader(file))
            {
                while (!stream.EndOfStream && !stream.ReadLine().Trim().Equals("[Recent File]")) { }

                if (stream.EndOfStream)
                {
                    return null;
                }

                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    if (line.StartsWith("Recent Rom", StringComparison.CurrentCultureIgnoreCase))
                    {
                        var ret = Regex.Replace(line, @"Recent Rom [0-9]+=", "");
                        return ret;
                    }
                }
                return null;
            }
        }
    }
}