using System.Windows.Forms;

namespace PokemonGenerator.Controls
{
    class FloatTrackBar : TrackBar
    {
        private double precision;
        private double largeChange;
        private double maximum;
        private double minimum;
        private double smallChange;
        private double dValue;

        public FloatTrackBar()
        {
            Precision = 1;
            LargeChange = 10;
            Maximum = 100;
            Minimum = 0;
            SmallChange = 1;
            Value = 50;
        }

        public double Precision
        {
            get
            {
                return precision;
            }
            set
            {
                precision = value;
                base.LargeChange = (int)(largeChange / precision);
                base.Maximum = (int)(maximum / precision);
                base.Value = (int)(dValue / precision);
                base.SmallChange = (int)(smallChange / precision);
                base.Minimum = (int)(minimum / precision);
                TickFrequency = (base.Maximum - base.Minimum) / 10;
            }
        }

        public new double LargeChange
        {
            get
            {
                return (base.LargeChange * precision);
            }
            set
            {
                base.LargeChange = (int)(value / precision);
                largeChange = value;
            }
        }

        public new double Maximum
        {
            get
            {
                return (base.Maximum * precision);
            }
            set
            {
                base.Maximum = (int)(value / precision);
                maximum = value;
                TickFrequency = (base.Maximum - base.Minimum) / 10;
            }
        }

        public new double Minimum
        {
            get
            {
                return (base.Minimum * precision);
            }
            set
            {
                base.Minimum = (int)(value / precision);
                minimum = value;
                TickFrequency = (base.Maximum - base.Minimum) / 10;
            }
        }

        public new double SmallChange
        {
            get
            {
                return (base.SmallChange * precision);
            }
            set
            {
                base.SmallChange = (int)(value / precision);
                smallChange = value;
            }
        }

        public new double Value
        {
            get
            {
                return (base.Value * precision);
            }
            set
            {
                base.Value = (int)(value / precision);
                dValue = value;
            }
        }
    }
}