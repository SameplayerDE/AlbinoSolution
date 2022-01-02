using AlbinoLibrary.Resources;
using Microsoft.Xna.Framework.Content;

namespace AlbinoLibraryMonogame.Resources
{
    public interface IContentLoader<T> : IResourceLoader<T>
    {
        public void LoadContent(ContentManager contentManager);
    }
}