using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using PokemonGenerator.Controls;

namespace PokemonGenerator.Windows.Options
{
    public partial class OptionsWindowController : PageEnabledControl
    {
        private Dictionary<string, OptionsWindowBase> _options;
        private OptionsWindowBase _current;

        public OptionsWindowController()
        {
            InitializeComponent();

            _options = new Dictionary<string, OptionsWindowBase>();
        }

        public void AddOption(OptionsWindowBase optionWindow)
        {
            var text = TextOrDefault(optionWindow);
            _options[text] = optionWindow;
            ListOptions.Items.Add(text);

            if (_options.Count == 1)
            {
                ListOptions.SelectedIndex = 0;
                _current = optionWindow;
            }
        }

        public override void Shown(WindowEventArgs args)
        {
            _current?.Shown(args);
            base.Shown(args);
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            _current?.Closed(new WindowEventArgs(null));
            OnWindowClosedEvent(this, new WindowEventArgs(GetType()));
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            // Save all options
            var errorMsgs = new StringBuilder();
            foreach (var item in _options.Values)
            {
                try
                {
                    item.Save();
                }
                catch (InvalidOperationException ex)
                {
                    errorMsgs.AppendLine(ex.Message);
                }
            }

            // Deal with errors
            if (errorMsgs.Length > 0)
            {
                var response = MessageBox.Show($"{errorMsgs.ToString()}\nWould you like to close without saving?", "Unable to save", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (response == DialogResult.Cancel)
                {
                    return;
                }
            }

            // Close
            _current?.Closed(null);
            OnWindowClosedEvent(this, new WindowEventArgs(GetType()));
        }

        private void ListOptionsChanged(object sender, System.EventArgs e)
        {
            // sender
            var list = (ListBox)sender;

            // Close old
            _current?.Hide();
            _current?.Closed(null);
            PanelInner.Controls.Clear();

            // Open New
            _current = _options[(string)list.SelectedItem];
            _current.Dock = DockStyle.Fill;
            PanelInner.Controls.Add(_current);
            _current.Shown(null);
            _current.Show();
        }

        private string TextOrDefault(Control control)
        {
            return string.IsNullOrWhiteSpace(control.Text) ? control.GetType().Name : control.Text;
        }
    }
}