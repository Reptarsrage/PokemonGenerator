using PokemonGenerator.Controls;
using System;
using System.Windows.Forms;
using PokemonGenerator.Forms;
using PokemonGenerator.Windows;
using PokemonGenerator.Windows.Options;

namespace PokemonGenerator
{
    /// <summary>
    /// Runs the program as a GUI
    /// </summary>
    static class Program
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

            // Generate GUI
            using (var injector = new DependencyInjector())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Pre-Load some Options Windows
                injector.Get<TeamSelectionWindow>();
                OptionsWindowController options = injector.Get<OptionsWindowController>();
                options.AddOption(injector.Get<PokemonSelectionWindow>());
                options.AddOption(injector.Get<RandomOptionsWindow>());
                options.AddOption(injector.Get<PokemonLikelinessWindow>());

                // Generate
                Application.Run(new MainForm(injector));
            }
        }
    }
}