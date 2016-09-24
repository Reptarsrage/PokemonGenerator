/// <summary>
/// Author: Justin Robb
/// Date: 8/30/2016
/// 
/// Description:
/// Generates a team of six Gen II pokemon for use in Pokemon Gold or Silver.
/// Built in order to supply Pokemon Stadium 2 with a better selection of Pokemon.
/// 
/// </summary>

namespace PokemonGenerator
{
    using Fclp;
    using Modals;
    using System.IO;
    using System.Reflection;
    using IO;
    using System.Linq;
    using System;
    using System.Diagnostics;

    class Program
    {
        private static FluentCommandLineParser<PokeGeneratorArguments> _parser;
        private static string contentDirectory;
        private static string outputDirectory;

        static void Main(string[] args)
        {
            // Used to set the defaults determined by system
#if (DEBUG)
            // SET DataDirectory for connection string
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program)).Location));
            // SET content and output directories
            contentDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program)).Location);
            outputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
#else
            // SET DataDirectory for connection string
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\"));
            // SET content and output directories
            contentDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\");
            outputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
#endif
            // ParseCommandLineOptions options
            var options = ParseCommandLineOptions(args);
            if (options == null)
            {
                return;
            }

            // Set Game and save for player 1
            if (!string.IsNullOrWhiteSpace(options.Game1) && options.Game1.Equals("Silver", StringComparison.InvariantCultureIgnoreCase))
            {
                options.InputSav1 = Path.Combine(contentDirectory, "Gold.sav");
            }
            else
            {
                options.InputSav1 = Path.Combine(contentDirectory, "Silver.sav");
            }

            // Set Game and save for player 2
            if (!string.IsNullOrWhiteSpace(options.Game2) && options.Game2.Equals("Silver", StringComparison.InvariantCultureIgnoreCase))
            {
                options.InputSav2 = Path.Combine(contentDirectory, "Silver.sav");
            }
            else
            {
                options.InputSav1 = Path.Combine(contentDirectory, "Gold.sav");
            }

            var sav = readSavProperties(options.InputSav1);
            var gen = new PokemonGenerator();
            copyAndGen(options.OutputSav1, options.InputSav1, gen, sav, options.Level);
            copyAndGen(options.OutputSav2, options.InputSav2, gen, sav, options.Level);
        }

        /// <summary>
        /// Rakes a sav file, copies it (or replaces it), generates a team of six pokemon, and saves the team to the output file.
        /// </summary>
        /// <param name="out">Full path to the ouput file.</param>
        /// <param name="in">Full path to the input file.</param>
        /// <param name="gen">The <see cref="PokemonGenerator"/> to use.</param>
        /// <param name="sav">The <see cref="SAVFileModel" to use when saving./></param>
        /// <param name="level">The level to generate pokemon at. Must be 5-100.</param>
        private static void copyAndGen(string @out, string @in, PokemonGenerator gen, SAVFileModel sav, int level)
        {
            var list = gen.GenerateRandomPokemon(level, Entropy.Low); // TODO Entropy stuffs
            sav.TeamPokémonlist = list;
            writeSavProperties(@out, @in, sav);
            readSavProperties(@out);
            Debug.Print($"Created file {@out}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outname">Full path to the file to write to</param>
        /// <param name="filename">Full path to Tthe input file (needed so that we don't accidentally delete it if it's also the output file.)</param>
        /// <param name="sav">The <see cref="SAVFileModel" to use when saving.</param>
        private static void writeSavProperties(string outname, string filename, SAVFileModel sav)
        {
            if (!File.Exists(filename)) {
                throw new FileNotFoundException($"The provided input sav file was not found or inaccessible. '{filename}'.");
            }

            if (File.Exists(outname) && !Path.GetFullPath(filename).Equals(Path.GetFullPath(outname)))
            {
                File.Delete(outname);
            }
            else if (!Path.GetFullPath(filename).Equals(Path.GetFullPath(outname)))
            {
                File.Copy(filename, outname);
            }
            
            var charset = new Charset();
            var parser = new PokeSerializer();
            parser.SerializeSAVFileModal(outname, charset, sav);
        }

        /// <summary>
        /// Reads the Pokemon Gold/Silver sav file and deserializes it into a <see cref="SAVFileModel" modal./> 
        /// </summary>
        /// <param name="filename">Full path to the sav file to read.</param>
        /// <returns></returns>
        private static SAVFileModel readSavProperties(string filename)
        {
            using (FileStream reader = File.OpenRead(filename))
            using (BinaryReader2 breader = new BinaryReader2(reader))
            {
                var charset = new Charset();
                var parser = new PokeDeserializer();
                var sav = parser.ParseSAVFileModel(breader, charset);
                return sav;
            }
        }

        /// <summary>
        /// Prints usage information
        /// </summary>
        private static void Usage()
        {
            // triggers the SetupHelp Callback which writes the text to the console
            _parser.HelpOption.ShowHelp(_parser.Options);
        }

        /// <summary>
        /// Fluent Command Line Parser configuration. Take cmd line parameters and parses them.
        /// </summary>
        /// <param name="args">Raw cmd line arguments.</param>
        /// <returns>A <see cref="PokeGeneratorArguments"/> object containing all pcmd line parameter information.</returns>
        private static PokeGeneratorArguments ParseCommandLineOptions(string[] args)
        {
            _parser = new FluentCommandLineParser<PokeGeneratorArguments>();

            _parser.Setup<int>(arguments => arguments.Level)
                .As('l', "level")
                .SetDefault(50)
                .WithDescription("The Pokemon level to generate for.  Default is 50");

            _parser.Setup<string>(arguments => arguments.EntropyVal)
                .As('e', "entropy")
                .SetDefault("low")
                .WithDescription("Amount of randomness to use when generating Pokemon. See README for full info.");

            _parser.Setup<string>(arguments => arguments.InputSav1)
                .As("i1")
                .SetDefault(Path.Combine(contentDirectory, "Gold.sav"))
                .WithDescription("The Pokemon Gold/Silver emulator sav file to modify for player 1. A default sav file is used when this parameter in omitted.");

            _parser.Setup<string>(arguments => arguments.InputSav2)
                .As("i2")
                .SetDefault(Path.Combine(contentDirectory, "Gold.sav"))
                .WithDescription("The Pokemon Gold/Silver emulator sav file to modify for player 2. A default sav file is used when this parameter in omitted.");

            _parser.Setup<string>(arguments => arguments.OutputSav1)
                .As("o1")
                .SetDefault(Path.Combine(outputDirectory, "Player1.sav"))
                .WithDescription("The path to the desired output location for the Pokemon Gold/Silver emulator sav file for player 1. Defaults to 'Player1.sav' on the current user's desktop.");

            _parser.Setup<string>(arguments => arguments.OutputSav2)
                .As("o2")
                .SetDefault(Path.Combine(outputDirectory, "Player2.sav"))
                .WithDescription("The path to the desired output location for the Pokemon Gold/Silver emulator sav file for player 2. Defaults to 'Player2.sav' on the current user's desktop.");

            _parser.Setup<string>(arguments => arguments.Game1)
                .As("game1")
                .SetDefault("Gold")
                .WithDescription("The Game to use (Gold or Silver). Default is Gold.");

            _parser.Setup<string>(arguments => arguments.Game2)
                .As("game2")
                .SetDefault("Gold")
                .WithDescription("The Game to use (Gold or Silver). Default is Gold.");

            _parser.SetupHelp("?", "help")
                .Callback(text => Console.WriteLine("\nUsage:\n" + text));

            var result = _parser.Parse(args);

            if (result.HasErrors)
            {
                Console.WriteLine(result.Errors.Aggregate("Errors parsing the command line arguments:",
                    (seed, error) =>
                    {
                        var commandLineOption = error.Option;
                        var erroredItem = commandLineOption.HasLongName ? commandLineOption.LongName : commandLineOption.ShortName;
                        return $"{seed}{Environment.NewLine}  \"{erroredItem}:{{{commandLineOption.Description}}}\"";
                    }));
                return null;
            }

            return _parser.Object;
        }
    }
}
