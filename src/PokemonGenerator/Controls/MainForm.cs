using PokemonGenerator.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PokemonGenerator.Controls
{
    public partial class MainForm : Form
    {
        private readonly Stack<WindowBase> _windows;
        private readonly DependencyInjector _injector;

        public MainForm(DependencyInjector injector)
        {
            InitializeComponent();

            _windows = new Stack<WindowBase>();
            _injector = injector;
            OpenWindow(this, new WindowEventArgs(typeof(MainWindow)));
        }

        private WindowBase GetWindowOfType(Type type)
        {
            if (!typeof(WindowBase).IsAssignableFrom(type))
            {
                throw new ArgumentException(nameof(type));
            }

            return _injector.Get(type) as WindowBase;
        }

        private void CloseWindow(object sender, WindowEventArgs args)
        {
            // Close current window
            CloseWindow(_windows.Pop());

            // Check if empty
            if (!_windows.Any())
            {
                Close();
            }

            // Show old window
            LoadWindow(_windows.Peek());
        }

        private void OpenWindow(object sender, WindowEventArgs args)
        {
            // Close current window if there is one
            if (_windows.Any())
            {
                CloseWindow(_windows.Peek());
            }

            // Show new window
            var window = GetWindowOfType(args.Window);
            _windows.Push(window);
            LoadWindow(window);
        }

        private void CloseWindow (WindowBase window)
        {
            // Close control
            window.SendToBack();
            window.Hide();
            Controls.Clear();

            // De-Init control
            window.WindowOpenedEvent -= OpenWindow;
            window.WindowClosedEvent -= CloseWindow;
            window.Closed();
        }

        private void LoadWindow(WindowBase window)
        {
            // Add control
            Controls.Add(window);
            window.Dock = DockStyle.Fill;

            // Init control
            window.Shown();
            window.BringToFront();
            window.WindowOpenedEvent += OpenWindow;
            window.WindowClosedEvent += CloseWindow;

            // Show
            window.Show();
        }
    }
}