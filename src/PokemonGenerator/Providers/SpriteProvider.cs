using System.Drawing;

namespace PokemonGenerator.Providers
{
    public interface ISpriteProvider
    {
        Bitmap RenderSprite(int index, Size imageSize);
    }

    public class SpriteProvider : ISpriteProvider
    {
        private const int SpriteTileWidth = 56;
        private const int SpriteTileHeight = 56;

        public Bitmap RenderSprite(int index, Size imageSize)
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
        }
    }
}