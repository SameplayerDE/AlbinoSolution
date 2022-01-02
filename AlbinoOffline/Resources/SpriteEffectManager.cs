using AlbinoLibraryMonogame.Resources;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoOffline.Resources
{
    public sealed class SpriteEffectManager : SpriteEffectLoader
    {
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        public static SpriteEffectManager Instance { get; } = new SpriteEffectManager();

        static SpriteEffectManager()
        {
            
        }
        
        private SpriteEffectManager()
        {
            
        }
        
        public override void LoadContent(ContentManager contentManager)
        {
            Add("edge_fade", contentManager.Load<Effect>("EdgeFade"));
            Add("radial_fade", contentManager.Load<Effect>("FadeRadial"));
            Add("dither_fade", contentManager.Load<Effect>("DitherFade"));
            
            /*
             * Load Dither Texture
             */
            Find("dither_fade").Parameters["DitherTexture"].SetValue(TextureManager.Instance.Find("ditherx2"));

        }
    }
}