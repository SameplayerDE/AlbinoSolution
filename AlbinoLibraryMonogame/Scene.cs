using System;
using AlbinoLibraryMonogame.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoLibraryMonogame
{
    public abstract class Scene
    {
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        public World World = new World();
        public VirtualWindow VirtualWindow;
        public bool Enabled = true;

        public abstract void Prepare();
        public abstract void InitialPrepare();
        public abstract void SubsequentPrepare();
        
        public abstract void Init(GraphicsDevice graphicsDevice);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        public void ClearMemory()
        {
            World.GameObjects.Clear();
        }
        
        public void SaveToFile()
        {
            
        }
        
    }
}