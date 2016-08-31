/// <summary>
/// Author: Justin Robb
/// Date: 8/30/2016
/// 
/// Description:
/// Generates a team of six Gen II pokemon for use in Pokemon Gold or Silver.
/// Built in order to supply Pokemon Stadium 2 with a better selection of Pokemon.
/// 
/// </summary>


namespace PokemonGeneratorGUI
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Reads  and Writes to Projects 64's  NRage ini file to get some info to auto-fill some forms.
    /// </summary>
    public class NRageIniEditor
    {
        private string filename;

        public NRageIniEditor(string ini)
        {
            this.filename = ini;

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"{ini} file not found.");
            }
        }

        /// <summary>
        /// Gets the ROM location for the player
        /// And the sav file location
        /// </summary>
        public Tuple<string, string> GetRomAndSavFileLocation(int playerNum) {

            using (var file = File.OpenRead(this.filename))
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

        /// <summary>
        /// Sets the sav file location for the player, and the ROM location as well
        /// </summary>
        internal bool ChangeSavLocations(string text1, string text2)
        {
            string text;

            using (var file = File.OpenRead(this.filename))
            using (var stream = new StreamReader(file))
            {
                text = stream.ReadToEnd();
            }

            var texts = Regex.Split(text, @"\[Controller [0-9]+\]");

            if (texts.Length < 3)
            {
                return false;
            }


            texts[1] = Regex.Replace(texts[1], @"^GBRomSave=.*$", $"GBRomSave={text1}");
            texts[2] = Regex.Replace(texts[2], @"^GBRomSave=.*$", $"GBRomSave={text2}");

            StringBuilder builder = new StringBuilder();

            builder.Append(texts[0]);
            for (int i = 1; i < texts.Length; i++)
            {
                builder.Append($"[Controller {i}]");
                builder.Append(texts[i]);
            }

            text = builder.ToString();

            File.WriteAllText(filename, text);

            return true;
        }
    }
}
