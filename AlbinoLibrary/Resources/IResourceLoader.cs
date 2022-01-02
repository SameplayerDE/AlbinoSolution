using System.Collections.Generic;

namespace AlbinoLibrary.Resources
{
    public interface IResourceLoader<T>
    {
        public bool Has(string key);
        public T Find(string key);
        public void Add(string key, T value, bool overwrite = false);
    }
}