using System.Collections.Generic;
using AlbinoLibrary.Resources;
using AlbinoLibraryMonogame.Rendering;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoLibraryMonogame.Resources
{
    public abstract class AlbinoTextureLoader : IResourceLoader<AlbinoTexture>
    {
        public Dictionary<string, AlbinoTexture> Resources = new Dictionary<string, AlbinoTexture>();

        public bool Has(string name)
        {
            return Resources.ContainsKey(name);
        }

        public AlbinoTexture Find(string name)
        {
            if (Has(name))
            {
                return Resources[name];
            }
            return Resources["missing"];
        }

        public void Add(string key, AlbinoTexture value, bool overwrite = false)
        {
            if (overwrite)
            {
                Resources[key] = value;
            }
            else
            {
                Resources.Add(key, value);
            }
        }

        public abstract void LoadContent(GraphicsDevice graphicsDevice);
    }
}