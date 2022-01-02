using AlbinoLibraryMonogame.Components.Rendering;
using AlbinoLibraryMonogame.Rendering;
using AlbinoLibraryMonogame.Resources;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoOffline.Resources
{
    public class AlbinoTextureManager : AlbinoTextureLoader
    {
        
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        public static AlbinoTextureManager Instance { get; } = new AlbinoTextureManager();

        static AlbinoTextureManager()
        {
            
        }
        
        private AlbinoTextureManager()
        {
            
        }
        
        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
            Add("missing", AlbinoTexture.LoadSingle(graphicsDevice, "missing"));
            Add("hans", AlbinoTexture.LoadSingle(graphicsDevice, "hans"));
            Add("home", AlbinoTexture.LoadSingle(graphicsDevice, "home"));
            Add("zweiblatt", AlbinoTexture.LoadSingle(graphicsDevice, "zweiblatt"));
            Add("human_sprites", AlbinoTexture.LoadMultiple(graphicsDevice, "human_sprites", 12, 8));
            Add("tileset", AlbinoTexture.LoadMultiple(graphicsDevice, "tileset", 80, -1));
            Add("explosion", AlbinoTexture.LoadMultiple(graphicsDevice, "explosion", 4, -1));
            
            SpriteRenderer.Loader = this;
            TilemapRenderer.Loader = this;
        }
    }
}