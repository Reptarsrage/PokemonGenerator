using PokemonGenerator.Forms;
using System;
using System.Windows.Forms;

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
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\"));
#endif

            // Init DAL
            DapperMapper.Init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Generate
            Application.Run(new MainForm());

        }
    }
}