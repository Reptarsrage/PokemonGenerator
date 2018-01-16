﻿using PokemonGenerator.Forms;
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
                Application.Run(injector.Get<PokemonGeneratorForm>());
            }
        }
    }
}