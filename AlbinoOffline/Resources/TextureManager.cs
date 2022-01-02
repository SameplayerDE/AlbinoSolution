using AlbinoLibraryMonogame.Components.Rendering;
using AlbinoLibraryMonogame.Rendering;
using AlbinoLibraryMonogame.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoOffline.Resources
{
    public sealed class TextureManager : TextureLoader
    {
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        public static TextureManager Instance { get; } = new TextureManager();

        static TextureManager()
        {
            
        }
        
        private TextureManager()
        {
            
        }
        
        public override void LoadContent(ContentManager contentManager)
        {
            Add("missing", contentManager.Load<Texture2D>("missing"));
            Add("human_sprites", contentManager.Load<Texture2D>("rpg_player"));
            Add("explosion", contentManager.Load<Texture2D>("explosion"));
            Add("hans", contentManager.Load<Texture2D>("hans"));
            Add("zweiblatt", contentManager.Load<Texture2D>("zweiblatt"));
            Add("tileset", contentManager.Load<Texture2D>("AlbinoTiles"));
            Add("home", contentManager.Load<Texture2D>("home"));

            AlbinoTexture.Loader = this;
            AlbinoTextureManager.Instance.LoadContent(AlbinoOffline.GraphicsDeviceManager.GraphicsDevice);
        }
    }

    public static class TextureManagerSpriteBatchExpander
    {
        public static void Draw(this SpriteBatch spriteBatch, string key, Vector2 position, Color color)
        {
            var texture = TextureManager.Instance.Find(key);
            spriteBatch.Draw(texture, position, color);
        }
    }
    
}