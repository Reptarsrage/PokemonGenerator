using PokemonGenerator.Models;
using System;
using System.IO;

namespace PokemonGenerator
{
    class Program
    {
        private static IPokemonGeneratorRunner runner;
        private static string contentDirectory;
        private static string outputDirectory;

        static void Main(string[] args)
        {
            // Used to set the defaults determined by system
#if (DEBUG)
            // SET DataDirectory for connection string
            AppDomain.CurrentDomain.SetData("DataDirectory", PokemonGeneratorRunner.AssemblyDirectory);
            // SET content and output directories
            contentDirectory = PokemonGeneratorRunner.AssemblyDirectory;
            outputDirectory = Path.Combine(PokemonGeneratorRunner.AssemblyDirectory, "Out");
#else
            // SET DataDirectory for connection string
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\"));
            // SET content and output directories
            contentDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\");
            outputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
#endif
            // Init DAL
            DapperMapper.Init();

            // ParseCommandLineOptions options
            var options = new PokeGeneratorArguments();
            if (!CommandLine.Parser.Default.ParseArguments(args, options,
                (verb, subOptions) =>
                {
                    // Set Game and save for player 1
                    options.InputSavOne = (options.GameOne ?? string.Empty).Equals("Silver", StringComparison.InvariantCultureIgnoreCase) ?
                        Path.Combine(contentDirectory, "Silver.sav") :
                        Path.Combine(contentDirectory, "Gold.sav");

                    // Set Game and save for player 2
                    options.InputSavTwo = (options.GameTwo ?? string.Empty).Equals("Silver", StringComparison.InvariantCultureIgnoreCase) ?
                        Path.Combine(contentDirectory, "Silver.sav") :
                        Path.Combine(contentDirectory, "Gold.sav");

                    // Run the generator
                    runner = new PokemonGeneratorRunner(contentDirectory, outputDirectory, subOptions as PokeGeneratorArguments);
                    runner.Run();
                }))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }
        }
    }
}