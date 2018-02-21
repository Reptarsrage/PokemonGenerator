using PokemonGenerator.Providers;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PokemonGenerator.Controls
{
    public partial class SVGViewer : UserControl
    {
        private readonly BackgroundWorker _worker;
        private readonly ISpriteProvider _spriteProvider;

        private string _svgImage;
        private int _width;
        private int _height;

        public SVGViewer()
        {
            InitializeComponent();

            _spriteProvider = DependencyInjector.Get<ISpriteProvider>();
            _worker = new BackgroundWorker();
            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            PictureBoxMain.Click += PictureBoxMainClick;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PictureBoxMain.Image = (Bitmap)e.Result;
            Update();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_svgImage == null)
            {
                return;
            }

            _width = Width;
            _height = Height;
            e.Result = _spriteProvider.RenderSvg(_svgImage, new Size(_width, _height));
        }

        private void PictureBoxMainClick(object sender, System.EventArgs e)
        {
            OnClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width != _width || Height != _height)
            {
                if (!_worker.IsBusy)
                    _worker.RunWorkerAsync();
            }

            base.OnPaint(e);
        }

        public Image Image
        {
            get => PictureBoxMain.Image;
            set => PictureBoxMain.Image = value;
        }

        public string SvgImage
        {
            get => _svgImage;
            set
            {
                _svgImage = value;
                if (!_worker.IsBusy)
                    _worker.RunWorkerAsync();
            }
        }
    }
}