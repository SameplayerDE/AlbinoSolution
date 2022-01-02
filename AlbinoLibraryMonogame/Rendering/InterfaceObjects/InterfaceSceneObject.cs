using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoLibraryMonogame.Rendering.InterfaceObjects
{
    public abstract class InterfaceSceneObject
    {
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        public abstract void Update(GameTime gameTime);
    }
}