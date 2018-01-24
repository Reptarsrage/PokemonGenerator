using System;
using System.Drawing;
using System.Windows.Forms;

namespace PokemonGenerator.Controls
{
    class FieldNumeric : Panel
    {
        public new string Text
        {
            get
            {
                return _label.Text;
            }
            set
            {
                _label.Text = value;
            }
        }

        public int DecimalPlaces
        {
            get
            {
                return _numeric.DecimalPlaces;
            }
            set
            {
                _numeric.DecimalPlaces = value;
                _trackBar.Precision = Math.Pow(10D, -1D * value);
            }
        }

        public decimal Increment
        {
            get
            {
                return _numeric.Increment;
            }
            set
            {
                _numeric.Increment = value;
                _trackBar.SmallChange = (double)value;
                _trackBar.LargeChange = (double)value * 10D;
            }
        }

        public decimal Value
        {
            get
            {
                return _numeric.Value;
            }
            set
            {
                _numeric.Value = value;
                _trackBar.Value = (double)value;
            }
        }

        public new ControlBindingsCollection DataBindings => _numeric.DataBindings;

        public decimal Minimum
        {
            get
            {
                return _numeric.Minimum;
            }
            set
            {
                _numeric.Minimum = value;
                _trackBar.Minimum = (double)value;
                _labelRange.Text = $"{_numeric.Minimum} - {_numeric.Maximum}";
            }
        }

        public decimal Maximum
        {
            get
            {
                return _numeric.Maximum;
            }
            set
            {
                _numeric.Maximum = value;
                _trackBar.Maximum = (double)value;
                _labelRange.Text = $"{_numeric.Minimum} - {_numeric.Maximum}";
            }
        }

        private readonly Label _label;
        private readonly NumericUpDown _numeric;
        private readonly FloatTrackBar _trackBar;
        private readonly Label _labelRange;

        public FieldNumeric()
            : base()
        {
            _label = new Label();
            _label.AutoSize = true;
            _label.Dock = DockStyle.Left;
            _label.TextAlign = ContentAlignment.MiddleRight;

            _trackBar = new FloatTrackBar();
            _trackBar.AutoSize = true;
            _trackBar.Dock = DockStyle.Fill;

            _labelRange = new Label();
            _labelRange.AutoSize = true;
            _labelRange.Dock = DockStyle.Right;
            _labelRange.TextAlign = ContentAlignment.MiddleLeft;
            _labelRange.ForeColor = SystemColors.GrayText;

            _numeric = new NumericUpDown();
            _labelRange.AutoSize = true;
            _numeric.Dock = DockStyle.Right;

            Controls.Add(_label);
            Controls.Add(_trackBar);
            Controls.Add(_numeric);
            Controls.Add(_labelRange);

            _numeric.ValueChanged += NumericValueChanged;
            _trackBar.ValueChanged += TrackBarValueChanged; ;
        }

        private void TrackBarValueChanged(object sender, EventArgs e)
        {
            _numeric.ValueChanged -= NumericValueChanged;
            _numeric.Value = (decimal)_trackBar.Value;
            _numeric.DataBindings[0].WriteValue();
            _numeric.ValueChanged += NumericValueChanged;
        }

        private void NumericValueChanged(object sender, EventArgs e)
        {
            _trackBar.ValueChanged -= TrackBarValueChanged;
            _trackBar.Value = (double)_numeric.Value;
            _trackBar.ValueChanged += TrackBarValueChanged;
        }
    }
}