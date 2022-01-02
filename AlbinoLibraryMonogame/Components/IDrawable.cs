using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoLibraryMonogame.Components
{
    public interface IDrawable
    {
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}