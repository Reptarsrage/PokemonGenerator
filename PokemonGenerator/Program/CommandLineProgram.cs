using PokemonGenerator.Enumerations;
using PokemonGenerator.Models;
using System;
using System.IO;
using System.Reflection;

namespace PokemonGenerator
{
    /// <summary>
    /// Runs the program as a command line tool
    /// </summary>
    class CommandLineProgram
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        static void Main(string[] args)
        {
            // Configure Directories
#if (DEBUG)
            AppDomain.CurrentDomain.SetData("DataDirectory", CommandLineProgram.AssemblyDirectory);
            var contentDirectory = CommandLineProgram.AssemblyDirectory;
            var outputDirectory = Path.Combine(CommandLineProgram.AssemblyDirectory, "Out");
#else
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\"));
            var contentDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\");
            var outputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
#endif
            // Init DAL
            DapperMapper.Init();

            // Parse Command Line Options
            var options = new PokeGeneratorOptions();
            if (!CommandLine.Parser.Default.ParseArguments(args, options,
                (verb, subOptions) =>
                {
                    // Set Game and save for player 1
                    options.InputSaveOne = (options?.GameOne ?? PokemonGame.Gold.ToString()).Equals("Silver", StringComparison.InvariantCultureIgnoreCase) ?
                        Path.Combine(contentDirectory, "Silver.sav") :
                        Path.Combine(contentDirectory, "Gold.sav");

                    // Set Game and save for player 2
                    options.InputSaveTwo = (options?.GameTwo ?? PokemonGame.Gold.ToString()).Equals("Silver", StringComparison.InvariantCultureIgnoreCase) ?
                        Path.Combine(contentDirectory, "Silver.sav") :
                        Path.Combine(contentDirectory, "Gold.sav");

                    // Run the generator
                    using (var injection = new NinjectWrapper())
                    {
                        var runner = injection.Get<IPokemonGeneratorRunner>();
                        runner.Run(new PersistentConfig {
                            Options =  subOptions as PokeGeneratorOptions,
                            Configuration = new PokemonGeneratorConfig()
                        });
                    }
                }))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }
        }
    }
}