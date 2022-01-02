using System.Collections.Generic;
using AlbinoLibraryMonogame.Rendering.InterfaceObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoLibraryMonogame.Rendering
{
    public abstract class InterfaceScene
    {
        private List<InterfaceSceneObject> _objects;

        public void Add(InterfaceSceneObject interfaceSceneObject)
        {
            _objects.Add(interfaceSceneObject);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            lock (_objects)
            {
                foreach (var sceneObject in _objects)
                {
                    sceneObject.Draw(spriteBatch, gameTime);
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            lock (_objects)
            {
                foreach (var sceneObject in _objects)
                {
                    sceneObject.Update(gameTime);
                }
            }
        }
    }
}