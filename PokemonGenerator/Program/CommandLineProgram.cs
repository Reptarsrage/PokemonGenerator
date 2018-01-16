using PokemonGenerator.Enumerations;
using PokemonGenerator.Models;
using PokemonGenerator.Utilities.Interfaces;
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
            // Init DAL
            DapperMapper.Init();

            using (var injection = new NinjectWrapper())
            {
                // Parse Command Line Options
                var options = new PokeGeneratorOptions();
                var directoryUtility = injection.Get<IDirectoryUtility>();
                if (!CommandLine.Parser.Default.ParseArguments(args, options,
                    (verb, subOptions) =>
                    {
                        // Set Game and save for player 1
                        options.InputSaveOne = (options?.GameOne ?? PokemonGame.Gold.ToString()).Equals("Silver", StringComparison.InvariantCultureIgnoreCase) ?
                                Path.Combine(directoryUtility.ContentDirectory(), "Silver.sav") :
                                Path.Combine(directoryUtility.ContentDirectory(), "Gold.sav");

                        // Set Game and save for player 2
                        options.InputSaveTwo = (options?.GameTwo ?? PokemonGame.Gold.ToString()).Equals("Silver", StringComparison.InvariantCultureIgnoreCase) ?
                                Path.Combine(directoryUtility.ContentDirectory(), "Silver.sav") :
                                Path.Combine(directoryUtility.ContentDirectory(), "Gold.sav");

                        // Run the generator

                        var runner = injection.Get<IPokemonGeneratorRunner>();
                        var config = injection.Get<PersistentConfig>();
                        config.Options = subOptions as PokeGeneratorOptions;
                        runner.Run(config);

                    }))
                {
                    Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
                }
            }
        }
    }
}