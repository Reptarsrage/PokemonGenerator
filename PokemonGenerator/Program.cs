using Fclp;
using PokemonGenerator.Modals;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PokemonGenerator
{
    class Program
    {
        private static FluentCommandLineParser<PokeGeneratorArguments> _parser;
        private static IPokemonGeneratorRunner runner;
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
            options.InputSavOne = (options.GameOne ?? string.Empty).Equals("Silver", StringComparison.InvariantCultureIgnoreCase) ?
                Path.Combine(contentDirectory, "Silver.sav") :
                Path.Combine(contentDirectory, "Gold.sav");

            // Set Game and save for player 2
            options.InputSavTwo = (options.GameTwo ?? string.Empty).Equals("Silver", StringComparison.InvariantCultureIgnoreCase) ?
                Path.Combine(contentDirectory, "Silver.sav") :
                Path.Combine(contentDirectory, "Gold.sav");

            // Run the generator
            runner = new PokemonGeneratorRunner(contentDirectory, outputDirectory, options);
            runner.Run();
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

            _parser.Setup<string>(arguments => arguments.InputSavOne)
                .As("i1")
                .SetDefault(Path.Combine(contentDirectory, "Gold.sav"))
                .WithDescription("The Pokemon Gold/Silver emulator sav file to modify for player 1. A default sav file is used when this parameter in omitted.");

            _parser.Setup<string>(arguments => arguments.InputSavTwo)
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

            _parser.Setup<string>(arguments => arguments.GameOne)
                .As("game1")
                .SetDefault("Gold")
                .WithDescription("The Game to use for player 1 (Gold or Silver). Default is Gold.");

            _parser.Setup<string>(arguments => arguments.GameTwo)
                .As("game2")
                .SetDefault("Gold")
                .WithDescription("The Game to use for player 2 (Gold or Silver). Default is Gold.");

            _parser.Setup<string>(arguments => arguments.NameOne)
                .As("name1")
                .SetDefault("Player1")
                .WithDescription("The Name to use for player 1. Default is Player1.");

            _parser.Setup<string>(arguments => arguments.NameTwo)
                .As("name2")
                .SetDefault("Player2")
                .WithDescription("The Name to use for player 2. Default is Player2.");

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