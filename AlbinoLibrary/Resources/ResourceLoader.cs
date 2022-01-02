using System.Collections.Generic;

namespace AlbinoLibrary.Resources
{
    public class ResourceLoader<T> : IResourceLoader<T>
    {
        
        public Dictionary<string, T> Resources = new Dictionary<string, T>();
        
        public bool Has(string name)
        {
            return Resources.ContainsKey(name);
        }

        public T Find(string name)
        {
            if (Has(name))
            {
                return Resources[name];
            }
            return Resources["debug"];
        }

        public void Add(string key, T value, bool overwrite = false)
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
    }
}