using System.Drawing;
using System.IO;
using Microsoft.Extensions.Caching.Memory;
using Svg;

namespace PokemonGenerator.Providers
{
    public interface ISpriteProvider
    {
        Bitmap RenderSprite(int index, Size imageSize);
        Bitmap RenderSvg(string name, Size imageSize);
    }

    public class SpriteProvider : ISpriteProvider
    {
        private readonly IMemoryCache _cache;
        private const int SpriteTileWidth = 56;
        private const int SpriteTileHeight = 56;

        public SpriteProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Bitmap RenderSprite(int index, Size imageSize)
        {
            return _cache.GetOrCreate($"SPRITE_{index}_{imageSize.Width}x{imageSize.Height}", entry =>
            {
                var x = index * SpriteTileWidth % Properties.Resources.sprites.Width;
                var y = index * SpriteTileWidth / Properties.Resources.sprites.Width * SpriteTileHeight;
                var cropArea = new Rectangle(x, y, SpriteTileWidth, SpriteTileHeight);
                var target = new Bitmap(imageSize.Width, imageSize.Height);

                using (var g = Graphics.FromImage(target))
                {
                    g.DrawImage(Properties.Resources.sprites, new Rectangle(0, 0, target.Width, target.Height), cropArea, GraphicsUnit.Pixel);
                    return target;
                }
            });
        }

        public Bitmap RenderSvg(string name, Size imageSize)
        {
            return _cache.GetOrCreate($"SVG_{name}_{imageSize.Width}x{imageSize.Height}", entry =>
            {
                using (var s = new MemoryStream(Properties.Resources.ResourceManager.GetObject(name) as byte[]))
                {
                    var doc = SvgDocument.Open<SvgDocument>(s, null);
                    doc.Width = imageSize.Width;
                    doc.Height = imageSize.Height;
                    return doc.Draw();
                }
            });
        }
    }
}