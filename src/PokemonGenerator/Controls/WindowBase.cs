using System;
using System.Windows.Forms;

namespace PokemonGenerator.Controls
{
    public class WindowEventArgs
    {
        public WindowEventArgs(Type t)
        {
            Window = t;
        }

        public Type Window { get; }

        public int Player { get; set; }
    }

    public class WindowBase : UserControl
    {
        // Events (Show new and Close this)
        public delegate void WindowOpenedDelegate(object sender, WindowEventArgs args);
        public event WindowOpenedDelegate WindowOpenedEvent;

        public delegate void WindowClosedDelegate(object sender, WindowEventArgs args);
        public event WindowOpenedDelegate WindowClosedEvent;

        // Methods Shown() and Closed()
        public virtual void Shown(WindowEventArgs args) { }
        public virtual void Closed(WindowEventArgs args) { }

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