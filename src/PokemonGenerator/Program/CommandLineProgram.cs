using CommandLine;
using PokemonGenerator.Enumerations;
using PokemonGenerator.Generators;
using PokemonGenerator.Models;
using PokemonGenerator.Utilities;
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

            // Init DI
            var result = 0;
            using (var dependencyInjector = new DependencyInjector())
            {
                // Parse Command Line Options
                var parser = new Parser(config => config.HelpWriter = Console.Out);
                result = parser.ParseArguments<PokeGeneratorOptions>(args)
                    .MapResult(options => Run(options, dependencyInjector), _ => 1);
            }

            Environment.Exit(result);
        }

        static int Run(PokeGeneratorOptions options, DependencyInjector dependencyInjector)
        {
            var directoryUtility = dependencyInjector.Get<IDirectoryUtility>();

            // Set Game and save for player 1
            options.InputSaveOne = (options?.GameOne ?? PokemonGame.Gold.ToString()).Equals("Silver", StringComparison.InvariantCultureIgnoreCase) ?
                    Path.Combine(directoryUtility.ContentDirectory(), "Silver.sav") :
                    Path.Combine(directoryUtility.ContentDirectory(), "Gold.sav");

            // Set Game and save for player 2
            options.InputSaveTwo = (options?.GameTwo ?? PokemonGame.Gold.ToString()).Equals("Silver", StringComparison.InvariantCultureIgnoreCase) ?
                    Path.Combine(directoryUtility.ContentDirectory(), "Silver.sav") :
                    Path.Combine(directoryUtility.ContentDirectory(), "Gold.sav");

            // Run the generator

            var runner = dependencyInjector.Get<IPokemonGeneratorRunner>();
            var config = dependencyInjector.Get<PersistentConfig>();
            config.Options = options;
            runner.Run(config);

            return 0;
        }
    }
}