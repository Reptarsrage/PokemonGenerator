using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PokemonGenerator.Repositories
{
    /// <summary>
    /// Reads  and Writes to Projects 64's  NRage ini file to get some info to auto-fill some forms.
    /// </summary>
    public interface INRageConfigRepository
    {
        /// <summary>
        /// Path to the NRage plug-in .ini file
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// Gets the ROM location for the player
        /// And the sav file location
        /// </summary>
        Tuple<string, string> GetRomAndSavFileLocation(int playerNum);

        /// <summary>
        /// Sets the sav file location for the player, and the ROM location as well
        /// </summary>
        bool ChangeSavLocations(string text1, string text2);
    }

    /// <inheritdoc />
    public class NRageConfigRepository : INRageConfigRepository
    {
        private string _fileName;

        public NRageConfigRepository()
        {
            _fileName = string.Empty;
        }

        /// <inheritdoc />
        public string FileName
        {
            get => _fileName;
            set => _fileName = File.Exists(value) ? value : throw new FileNotFoundException($"{value} file not found.");
        }

        /// <inheritdoc />
        public Tuple<string, string> GetRomAndSavFileLocation(int playerNum)
        {
            FileName = _fileName; // quick validation
            using (var file = File.OpenRead(FileName))
            using (var stream = new StreamReader(file))
            {
                while (!stream.EndOfStream && !stream.ReadLine().Equals($"[Controller {playerNum}]")) { }

                if (stream.EndOfStream)
                {
                    return null;
                }

                string two = null;
                string one = null;
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    if (line.StartsWith("GBRomFile", StringComparison.CurrentCultureIgnoreCase))
                    {
                        one = line.Split('=')[1];
                    }

                    if (line.StartsWith("GBRomSave", StringComparison.CurrentCultureIgnoreCase))
                    {
                        two = line.Split('=')[1];
                    }

                    if (one != null && two != null)
                    {
                        return new Tuple<string, string>(one, two);
                    }

                    if (line.StartsWith("[Controller ", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return null;
                    }
                }
                return null;
            }
        }

        /// <inheritdoc />
        public bool ChangeSavLocations(string text1, string text2)
        {
            FileName = _fileName; // quick validation
            string text;
            using (var file = File.OpenRead(FileName))
            using (var stream = new StreamReader(file))
            {
                text = stream.ReadToEnd();
            }

            var texts = Regex.Split(text, @"\[Controller [0-9]+\]");

            if (texts.Length < 3)
            {
                return false;
            }

            texts[1] = Regex.Replace(texts[1], @"GBRomSave=.*\r\n", $"GBRomSave={text1}\r\n");
            texts[2] = Regex.Replace(texts[2], @"GBRomSave=.*\r\n", $"GBRomSave={text2}\r\n");

            var builder = new StringBuilder();

            builder.Append(texts[0]);
            for (int i = 1; i < texts.Length; i++)
            {
                builder.Append($"[Controller {i}]");
                builder.Append(texts[i]);
            }

            text = builder.ToString();

            File.WriteAllText(FileName, text);

            return true;
        }
    }
}