using Svg;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PokemonGenerator.Controls
{
    public partial class SVGViewer : UserControl
    {
        private SvgDocument _doc;
        private byte[] _svgImage;

        private int width;
        private int height;

        public SVGViewer()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width != width || Height != height)
            {
                RenderSvg(_doc);
            }

            base.OnPaint(e);
        }

        public Image Image
        {
            get
            {
                return PictureBoxMain.Image;
            }
            set
            {
                PictureBoxMain.Image = value;
            }
        }

        public byte[] SvgImage
        {
            get
            {
                return _svgImage;
            }
            set
            {
                _svgImage = value;
                using (var s = new MemoryStream(_svgImage, false))
                {
                    _doc = SvgDocument.Open<SvgDocument>(s, null);
                    RenderSvg(_doc);
                }
            }
        }

        private void RenderSvg(SvgDocument svgDoc)
        {
            width = Width;
            height = Height;
            svgDoc.Width = width;
            svgDoc.Height = height;
            PictureBoxMain.Image = svgDoc.Draw();
        }
    }
}