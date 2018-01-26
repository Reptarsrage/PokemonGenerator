using CommandLine;
using PokemonGenerator.Enumerations;
using PokemonGenerator.Generators;
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
#if (DEBUG)
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
#else
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\"));
#endif

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
            var contentDir = (string)AppDomain.CurrentDomain.GetData("DataDirectory");

            // Set Game and save for player 1
            options.InputSaveOne = (options?.GameOne ?? PokemonGame.Gold.ToString()).Equals("Silver", StringComparison.InvariantCultureIgnoreCase) ?
                    Path.Combine(contentDir, "Silver.sav") :
                    Path.Combine(contentDir, "Gold.sav");

            // Set Game and save for player 2
            options.InputSaveTwo = (options?.GameTwo ?? PokemonGame.Gold.ToString()).Equals("Silver", StringComparison.InvariantCultureIgnoreCase) ?
                    Path.Combine(contentDir, "Silver.sav") :
                    Path.Combine(contentDir, "Gold.sav");

            // Run the generator

            var runner = dependencyInjector.Get<IPokemonGeneratorRunner>();
            var config = dependencyInjector.Get<PersistentConfig>();
            config.Options = options;
            runner.Run(config);

            return 0;
        }

        private static int IOptions<T>()
        {
            throw new NotImplementedException();
        }
    }
}