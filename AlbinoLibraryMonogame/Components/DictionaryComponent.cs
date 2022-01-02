using System.Collections.Generic;
using AlbinoLibrary.ComponentSystem;

namespace AlbinoLibraryMonogame.Components
{
    public class DictionaryComponent<K, T> : AlbinoComponent
    {

        public readonly Dictionary<K, T> Dictionary = new Dictionary<K, T>();

        public void Add(K key, T value)
        {
            Dictionary.Add(key, value);
        }
        
        public T Get(K key)
        {
            return Dictionary[key];
        }
        
        public bool Has(K key)
        {
            return Dictionary.ContainsKey(key);
        }
        
        public void Set(K key, T value)
        {
            Dictionary[key] = value;
        }
        
        public override byte[] GetBytes()
        {
            throw new System.NotImplementedException();
        }

        public override void FromBytes(byte[] data)
        {
            throw new System.NotImplementedException();
        }
    }
}