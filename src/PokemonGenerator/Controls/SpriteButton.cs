using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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
        private const int SPRITE_TILE_WIDTH = 56;
        private const int SPRITE_TILE_HEIGHT = 56;


        private Color _tint;

        private readonly BackgroundWorker _backgroundWorker;
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

        public SpriteButton(int index, bool pressed) : base()
        {
            Image = null;
            TextImageRelation = TextImageRelation.ImageAboveText;
            TextAlign = ContentAlignment.BottomCenter;
            UseVisualStyleBackColor = true;
            Appearance = Appearance.Button;
            Checked = pressed;
            Size = new Size(SPRITE_TILE_WIDTH + 50, SPRITE_TILE_HEIGHT + 50);
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;

            CheckedChanged += SpriteButton_CheckedChanged;
            SpriteButton_CheckedChanged(null, null);

            _tint = Color.FromArgb(255, 235, 235, 235);

            _index = index;
            _imageSize = new Size(SPRITE_TILE_WIDTH, SPRITE_TILE_HEIGHT);

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
            var x = _index * SPRITE_TILE_WIDTH % Properties.Resources.sprites.Width;
            var y = _index * SPRITE_TILE_WIDTH / Properties.Resources.sprites.Width * SPRITE_TILE_HEIGHT;
            var cropArea = new Rectangle(x, y, SPRITE_TILE_WIDTH, SPRITE_TILE_HEIGHT);
            var target = new Bitmap(_imageSize.Width, _imageSize.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(Properties.Resources.sprites, new Rectangle(0, 0, target.Width, target.Height), cropArea, GraphicsUnit.Pixel);
                worker.ReportProgress(1, target);
            }
        }
    }
}