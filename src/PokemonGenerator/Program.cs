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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Pre-Load some Options Windows
            DependencyInjector.Get<TeamSelectionWindow>();
            OptionsWindowController options = DependencyInjector.Get<OptionsWindowController>();
            options.AddOption(DependencyInjector.Get<PokemonSelectionWindow>());
            options.AddOption(DependencyInjector.Get<RandomOptionsWindow>());
            options.AddOption(DependencyInjector.Get<PokemonLikelinessWindow>());

            // Generate
            Application.Run(new MainForm());
            
        }
    }
}