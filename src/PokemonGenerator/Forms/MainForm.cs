using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PokemonGenerator.Controls;
using PokemonGenerator.Windows;

namespace PokemonGenerator.Forms
{
    public partial class MainForm : Form
    {
        private readonly Stack<PageEnabledControl> _windows;

        public MainForm()
        {
            InitializeComponent();

            _windows = new Stack<PageEnabledControl>();
            OpenWindow(this, new WindowEventArgs(typeof(MainWindow)));
        }

        private PageEnabledControl GetWindowOfType(Type type)
        {
            if (!typeof(PageEnabledControl).IsAssignableFrom(type))
            {
                throw new ArgumentException(nameof(type));
            }

            return DependencyInjector.Get(type) as PageEnabledControl;
        }

        private void CloseWindow(object sender, WindowEventArgs args)
        {
            // Close current window
            CloseWindow(_windows.Pop(), args);

            // Check if empty
            if (!_windows.Any())
            {
                Close();
            }

            // Show old window
            LoadWindow(_windows.Peek(), args);
        }

        private void OpenWindow(object sender, WindowEventArgs args)
        {
            // Close current window if there is one
            if (_windows.Any())
            {
                CloseWindow(_windows.Peek(), args);
            }

            // Show new window
            var window = GetWindowOfType(args.Window);
            _windows.Push(window);
            LoadWindow(window, args);
        }

        private void CloseWindow(PageEnabledControl window, WindowEventArgs args)
        {
            // Close control
            window.SendToBack();
            window.Hide();
            Controls.Clear();

            // De-Init control
            window.WindowOpenedEvent -= OpenWindow;
            window.WindowClosedEvent -= CloseWindow;
            window.Closed(args);
        }

        private void LoadWindow(PageEnabledControl window, WindowEventArgs args)
        {
            // Add control
            Controls.Add(window);
            window.Dock = DockStyle.Fill;

            // Init control
            window.Shown(args);
            window.BringToFront();
            window.WindowOpenedEvent += OpenWindow;
            window.WindowClosedEvent += CloseWindow;

            // Show
            window.Show();
        }
    }
}