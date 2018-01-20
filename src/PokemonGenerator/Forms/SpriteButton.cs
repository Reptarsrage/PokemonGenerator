using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PokemonGenerator.Forms
{
    public class SpriteButton : CheckBox
    {
        private const int SPRITE_TILE_WIDTH = 56;
        private const int SPRITE_TILE_HEIGHT = 56;

        private readonly Color _unPressed = Color.FromArgb(255, 235, 235, 235);
        private readonly Color _pressed = Color.FromArgb(255, 225, 225, 225);
        private readonly Color _unPressedBorder = Color.FromArgb(255, 200, 200, 200);
        private readonly Color _pressedBorder = Color.FromArgb(255, 0, 0, 0);

        private readonly BackgroundWorker _backgroundWorker;
        private readonly int _index;
        private readonly Size _imageSize;

        public SpriteButton(int index, bool enabled, bool pressed) : base()
        {
            Image = null;
            TextImageRelation = TextImageRelation.ImageAboveText;
            TextAlign = ContentAlignment.BottomCenter;
            UseVisualStyleBackColor = true;
            Appearance = Appearance.Button;
            Checked = pressed;
            Enabled = enabled;
            Size = new Size(SPRITE_TILE_WIDTH + 50, SPRITE_TILE_HEIGHT + 50);
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;

            CheckedChanged += SpriteButton_CheckedChanged;
            SpriteButton_CheckedChanged(null, null);

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
            if (Checked)
            {
                BackColor = _pressed;
                FlatAppearance.MouseOverBackColor = _pressed;
                FlatAppearance.MouseDownBackColor = _pressed;
                FlatAppearance.CheckedBackColor = _pressed;
                FlatAppearance.BorderColor = _pressedBorder;
            }
            else
            {
                BackColor = _unPressed;
                FlatAppearance.MouseOverBackColor = _unPressed;
                FlatAppearance.MouseDownBackColor = _unPressed;
                FlatAppearance.CheckedBackColor = _unPressed;
                FlatAppearance.BorderColor = _unPressedBorder;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!Checked)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(180,220, 220, 220)), e.ClipRectangle);
            }

            // Draw Border using color specified in Flat Appearance
            Pen pen = new Pen(FlatAppearance.BorderColor, 1);
            Rectangle rectangle = new Rectangle(0, 0, Size.Width - 1, Size.Height - 1);
            e.Graphics.DrawRectangle(pen, rectangle);
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