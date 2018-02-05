using PokemonGenerator.Controls;
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
            // Set Directory based on build
#if (DEBUG)
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
#else
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\"));
#endif

            // Init DAL
            DapperMapper.Init();

            // Run GUI
            using (var injector = new DependencyInjector())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Pre-Load some Options Windows
                OptionsWindowController options = injector.Get<OptionsWindowController>();
                options.AddOption(injector.Get<PokemonSelectionWindow>());
                options.AddOption(injector.Get<RandomOptionsWindow>());
                options.AddOption(injector.Get<PokemonLikelinessWindow>());

                // Run
                Application.Run(new MainForm(injector));
            }
        }
    }
}