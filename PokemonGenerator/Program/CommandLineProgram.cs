using PokemonGenerator.Models;
using System;
using System.IO;

namespace PokemonGenerator
{
    /// <summary>
    /// Runs the program as a command line tool
    /// </summary>
    class CommandLineProgram
    {
        static void Main(string[] args)
        {
            // Configure Directories
#if (DEBUG)
            AppDomain.CurrentDomain.SetData("DataDirectory", PokemonGeneratorRunner.AssemblyDirectory);
            var contentDirectory = PokemonGeneratorRunner.AssemblyDirectory;
            var outputDirectory = Path.Combine(PokemonGeneratorRunner.AssemblyDirectory, "Out");
#else
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\"));
            var contentDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\");
            var outputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
#endif
            // Init DAL
            DapperMapper.Init();

            // Parse Command Line Options
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
                    using (var injection = new NinjectWrapper())
                    {
                        var runner = injection.Get<IPokemonGeneratorRunner>();
                        runner.Run(contentDirectory, outputDirectory, subOptions as PokeGeneratorArguments);
                    }
                }))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }
        }
    }
}