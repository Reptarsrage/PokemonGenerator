using PokemonGenerator.Controls;
using PokemonGenerator.Forms;
using System;
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
            // Init DAL
            DapperMapper.Init();

            // Run GUI
            using (var injector = new DependencyInjector())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Pre-Load some Options Windows
                OptionsWindowController options = injector.Get<OptionsWindowController>();
                options.AddOption(injector.Get<PokemonOptionsWindow>());
                options.AddOption(injector.Get<RandomOptionsWindow>());

                // Run
                Application.Run(new MainForm(injector));
            }
        }
    }
}