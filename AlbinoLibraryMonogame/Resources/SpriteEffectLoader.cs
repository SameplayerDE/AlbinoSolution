using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoLibraryMonogame.Resources
{
    public abstract class SpriteEffectLoader : IContentLoader<Effect>
    {
        public Dictionary<string, Effect> Resources = new Dictionary<string, Effect>();

        public bool Has(string name)
        {
            return Resources.ContainsKey(name);
        }

        public Effect Find(string name)
        {
            if (Has(name))
            {
                return Resources[name];
            }
            return Resources["missing"];
        }

        public void Add(string key, Effect value, bool overwrite = false)
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