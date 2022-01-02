using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoLibraryMonogame.Resources
{
    public abstract class TextureLoader : IContentLoader<Texture2D>
    {
        public Dictionary<string, Texture2D> Resources = new Dictionary<string, Texture2D>();

        public bool Has(string name)
        {
            return Resources.ContainsKey(name);
        }

        public Texture2D Find(string name)
        {
            if (Has(name))
            {
                return Resources[name];
            }
            return Resources["missing"];
        }

        public void Add(string key, Texture2D value, bool overwrite = false)
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

        public abstract void LoadContent(ContentManager contentManager);
    }
}