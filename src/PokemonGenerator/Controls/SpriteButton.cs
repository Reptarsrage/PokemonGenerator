using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PokemonGenerator.Providers;

namespace PokemonGenerator.Controls
{
    public class ItemSelectedEventArgs : EventArgs
    {
        public bool Selected => _selected;

        private readonly bool _selected;

        public ItemSelectedEventArgs(bool selected) : base()
        {
            _selected = selected;
        }
    }

    public class SpriteButton : CheckBox
    {
        private Color _tint;
        private readonly BackgroundWorker _backgroundWorker;
        private readonly ISpriteProvider _spriteProvider;
        private readonly int _index;
        private readonly Size _imageSize;

        // Tint
        public Color Tint
        {
            get
            {
                return _tint;
            }
            set
            {
                _tint = value;
                SetColors();
            }
        }

        // Index
        public int Index => _index;

        // Assuming you need a custom signature for your event. If not, use an existing standard event delegate
        public delegate void ItemSelctedDelegate(object sender, ItemSelectedEventArgs args);

        // Expose the event off your component
        public event ItemSelctedDelegate ItemSelctedEvent;

        public SpriteButton(
            ISpriteProvider spriteProvider,
            int index, 
            bool pressed) : base()
        {
            Image = null;
            TextImageRelation = TextImageRelation.ImageAboveText;
            TextAlign = ContentAlignment.BottomCenter;
            UseVisualStyleBackColor = true;
            Appearance = Appearance.Button;
            Checked = pressed;
            Size = new Size(106, 106);
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;

            CheckedChanged += SpriteButton_CheckedChanged;
            SpriteButton_CheckedChanged(null, null);

            _imageSize = new Size(56, 56);
            _tint = Color.FromArgb(255, 235, 235, 235);
            _spriteProvider = spriteProvider;
            _index = index;

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.ProgressChanged += _backgroundWorker_ProgressChanged;

            _backgroundWorker.RunWorkerAsync();
        }

        private void SpriteButton_CheckedChanged(object sender, EventArgs e)
        {
            SetColors();
            ItemSelctedEvent?.Invoke(this, new ItemSelectedEventArgs(Checked));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!Checked)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(180, 220, 220, 220)), e.ClipRectangle);
            }

            // Draw Border using color specified in Flat Appearance
            Pen pen = new Pen(FlatAppearance.BorderColor, 1);
            Rectangle rectangle = new Rectangle(0, 0, Size.Width - 1, Size.Height - 1);
            e.Graphics.DrawRectangle(pen, rectangle);
        }

        private void SetColors()
        {
            var mod = Checked ? 244 : 255;
            var color = Color.FromArgb(mod, _tint);

            BackColor = color;
            FlatAppearance.MouseOverBackColor = color;
            FlatAppearance.MouseDownBackColor = color;
            FlatAppearance.CheckedBackColor = color;
            FlatAppearance.BorderColor = color;
        }

        private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Image = e.UserState as Bitmap;
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var target = _spriteProvider.RenderSprite(_index, _imageSize);
            worker.ReportProgress(1, target);
        }
    }
}