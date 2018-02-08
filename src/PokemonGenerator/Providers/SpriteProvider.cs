using System.Drawing;

namespace PokemonGenerator.Providers
{
    public interface ISpriteProvider
    {
        Bitmap RenderSprite(int index, Size imageSize);
    }

    public class SpriteProvider : ISpriteProvider
    {
        private const int SPRITE_TILE_WIDTH = 56;
        private const int SPRITE_TILE_HEIGHT = 56;

        public Bitmap RenderSprite(int index, Size imageSize)
        {
            var x = index * SPRITE_TILE_WIDTH % Properties.Resources.sprites.Width;
            var y = index * SPRITE_TILE_WIDTH / Properties.Resources.sprites.Width * SPRITE_TILE_HEIGHT;
            var cropArea = new Rectangle(x, y, SPRITE_TILE_WIDTH, SPRITE_TILE_HEIGHT);
            var target = new Bitmap(imageSize.Width, imageSize.Height);

            using (var g = Graphics.FromImage(target))
            {
                g.DrawImage(Properties.Resources.sprites, new Rectangle(0, 0, target.Width, target.Height), cropArea, GraphicsUnit.Pixel);
                return target;
            }
        }
    }
}