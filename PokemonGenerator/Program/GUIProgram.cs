using PokemonGenerator.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace PokemonGenerator
{
    /// <summary>
    /// Runs the program as a GUI
    /// </summary>
    static class GUIProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Cofigure Directories
#if (DEBUG)
            var projN64Location = @"G:\Project64\Project64.exe"; // TODO remove
            AppDomain.CurrentDomain.SetData("DataDirectory", PokemonGeneratorRunner.AssemblyDirectory);
            var contentDirectory = PokemonGeneratorRunner.AssemblyDirectory;
            var outputDirectory = Path.Combine(contentDirectory, "Output");
#else
            var projN64Location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Project64\Project64.exe");
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
                Application.Run(new PokemonGeneratorForm(injector.Get<IPokemonGeneratorRunner>(), contentDirectory, outputDirectory, projN64Location));
            }
        }
    }
}