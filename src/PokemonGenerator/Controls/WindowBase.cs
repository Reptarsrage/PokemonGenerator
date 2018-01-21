using System;
using System.Windows.Forms;

namespace PokemonGenerator.Controls
{
    public class WindowEventArgs
    {
        public Type Window => _window;

        private readonly Type _window;

        public WindowEventArgs(Type t) : base()
        {
            _window = t;
        }
    }

    public abstract class WindowBase : UserControl
    {
        // Events (Show new and Close this)
        public delegate void WindowOpenedDelegate(object sender, WindowEventArgs args);
        public event WindowOpenedDelegate WindowOpenedEvent;

        public delegate void WindowClosedDelegate(object sender, WindowEventArgs args);
        public event WindowOpenedDelegate WindowClosedEvent;

        // Methods Shown() and Closed()
        public abstract void Shown();
        public abstract void Closed();

        protected void OnWindowOpenedEvent(object sender, WindowEventArgs args)
        {
            WindowOpenedEvent?.Invoke(sender, args);
        }

        protected void OnWindowClosedEvent(object sender, WindowEventArgs args)
        {
            WindowClosedEvent?.Invoke(sender, args);
        }
    }
}