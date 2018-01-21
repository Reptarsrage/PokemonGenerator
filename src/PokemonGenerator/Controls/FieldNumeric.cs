using System.Drawing;
using System.Windows.Forms;

namespace PokemonGenerator.Controls
{
    class FieldNumeric : FlowLayoutPanel
    {
        public string Text { get { return _label.Text; } set { _label.Text = value; } }
        public int DecimalPlaces { get { return _numeric.DecimalPlaces; } set { _numeric.DecimalPlaces = value; } }
        public decimal Increment { get { return _numeric.Increment; } set { _numeric.Increment = value; } }
        public decimal Value { get { return _numeric.Value; } set { _numeric.Value = value; } }
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
                _labelRange.Text = $"{_numeric.Minimum} - {_numeric.Maximum}";
            }
        }

        private readonly Label _label;
        private readonly NumericUpDown _numeric;
        private readonly Label _labelRange;

        public FieldNumeric()
            : base()
        {
            _label = new Label();
            _labelRange = new Label();
            _numeric = new NumericUpDown();

            _label.AutoSize = true;
            _label.Anchor = AnchorStyles.Left;
            _label.TextAlign = ContentAlignment.MiddleLeft;

            _labelRange.AutoSize = true;
            _labelRange.Anchor = AnchorStyles.Left;
            _labelRange.TextAlign = ContentAlignment.MiddleLeft;
            _labelRange.ForeColor = SystemColors.GrayText;

            Controls.Add(_label);
            Controls.Add(_numeric);
            Controls.Add(_labelRange);
        }
    }
}