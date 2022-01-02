using AlbinoLibrary.ComponentSystem;
using Microsoft.Xna.Framework;

namespace AlbinoLibraryMonogame.Components
{
    public abstract class UpdateableComponent : AlbinoComponent
    {
        public abstract void Update(GameTime gameTime);
    }
}