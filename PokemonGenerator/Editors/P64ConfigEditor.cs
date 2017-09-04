using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PokemonGenerator.Editors
{
    /// <summary>
    /// Reads Projects 64 Config file to get some info to auto-fill some forms.
    /// </summary>
    public class P64ConfigEditor : IP64ConfigEditor
    {
        private string filename;

        public P64ConfigEditor(string cfg)
        {
            this.filename = cfg;

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"{cfg} file not found.");
            }
        }

        /// <summary>
        /// Gets the most recently played ROM.
        /// </summary>
        public string GetRecentRom()
        {

            using (var file = File.OpenRead(this.filename))
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