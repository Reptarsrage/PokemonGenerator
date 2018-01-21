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

    public class WindowBase : UserControl
    {
        // Events (Show new and Close this)
        public delegate void WindowOpenedDelegate(object sender, WindowEventArgs args);
        public event WindowOpenedDelegate WindowOpenedEvent;

        public delegate void WindowClosedDelegate(object sender, WindowEventArgs args);
        public event WindowOpenedDelegate WindowClosedEvent;

        // Methods Shown() and Closed()
        public virtual void Shown() { }
        public virtual void Closed() { }

        protected virtual void OnWindowOpenedEvent(object sender, WindowEventArgs args)
        {
            WindowOpenedEvent?.Invoke(sender, args);
        }

        protected virtual void OnWindowClosedEvent(object sender, WindowEventArgs args)
        {
            WindowClosedEvent?.Invoke(sender, args);
        }
    }
}