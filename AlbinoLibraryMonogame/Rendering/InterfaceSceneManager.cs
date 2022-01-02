using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoLibraryMonogame.Rendering
{
    public sealed class InterfaceSceneManager
    {
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        public static InterfaceSceneManager Instance { get; } = new InterfaceSceneManager();

        private List<InterfaceScene> _scenes;

        static InterfaceSceneManager()
        {
            
        }
        
        private InterfaceSceneManager()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _scenes = new List<InterfaceScene>();
        }
        
        public void Add(InterfaceScene interfaceScene)
        {
            lock (_scenes)
            {
                _scenes.Add(interfaceScene);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            lock (_scenes)
            {
                foreach (var scene in _scenes)
                {
                    scene.Draw(spriteBatch, gameTime);
                }
            }
        }
        
        public void Update(GameTime gameTime)
        {
            lock (_scenes)
            {
                foreach (var scene in _scenes)
                {
                    scene.Update(gameTime);
                }
            }
        }
        
    }
}