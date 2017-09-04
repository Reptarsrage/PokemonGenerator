using PokemonGenerator.Editors;
using PokemonGenerator.Forms;
using PokemonGenerator.IO;
using PokemonGenerator.Validators;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PokemonGenerator
{
    /// <summary>
    /// Runs the program as a GUI
    /// </summary>
    static class GUIProgram
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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Cofigure Directories
#if (DEBUG)
            AppDomain.CurrentDomain.SetData("DataDirectory", GUIProgram.AssemblyDirectory);
            var contentDirectory = GUIProgram.AssemblyDirectory;
            var outputDirectory = Path.Combine(contentDirectory, "Output");
#else            
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\"));
            var contentDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\");
            var outputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
#endif

            // Init DAL
            DapperMapper.Init();

            // Run GUI
            using (var injector = new NinjectWrapper())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PokemonGeneratorForm(injector.Get<IPokemonGeneratorRunner>(),
                    injector.Get<IPersistentConfigManager>(),
                    injector.Get<IP64ConfigEditor>(),
                    injector.Get<INRageIniEditor>(),
                    injector.Get<IPokeGeneratorOptionsValidator>(),
                    contentDirectory, outputDirectory));
            }
        }
    }
}