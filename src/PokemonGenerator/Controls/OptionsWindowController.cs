using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PokemonGenerator.Controls
{
    public partial class OptionsWindowController : WindowBase
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

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            _current?.Closed();
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
                } catch (InvalidOperationException ex)
                {
                    errorMsgs.AppendLine(ex.Message);
                }
            }

            // Deal with errors
            if (errorMsgs.Length > 0)
            {
                var response = MessageBox.Show(errorMsgs.ToString(), "Unable to save", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (response == DialogResult.Cancel)
                {
                    return;
                }
            }

            // Close
            _current?.Closed();
            OnWindowClosedEvent(this, new WindowEventArgs(GetType()));
        }

        private void ListOptionsChanged(object sender, System.EventArgs e)
        {
            // sender
            var list = (ListBox)sender;

            // Close old
            _current?.Hide();
            _current?.Closed();
            PanelInner.Controls.Clear();

            // Open New
            _current = _options[(string)list.SelectedItem];
            _current.Dock = DockStyle.Fill;
            PanelInner.Controls.Add(_current);
            _current.Shown();
            _current.Show();
        }

        private string TextOrDefault(Control control)
        {
            return string.IsNullOrWhiteSpace(control.Text) ? control.GetType().Name : control.Text;
        }
    }
}