using System.Collections.Generic;
using System.Linq;
using AlbinoLibrary.ComponentSystem;
using AlbinoLibraryMonogame.Components;
using AlbinoLibraryMonogame.Objects;
using Microsoft.Xna.Framework;

namespace AlbinoLibraryMonogame
{
    public abstract class Simulation
    {
        public List<AlbinoObject> GameObjects;
        public Simulation()
        {
            GameObjects = new List<AlbinoObject>();
        }

        public AlbinoObject Find(string name)
        {
            lock (GameObjects)
            {
                // ReSharper disable once HeapView.ClosureAllocation
                // ReSharper disable once HeapView.DelegateAllocation
                return GameObjects.FirstOrDefault(gameObject => gameObject.Name == name);
            }
        }
        
        public List<T> FindAll<T>() where T : AlbinoObject
        {
            var results = new List<T>();
            lock (GameObjects)
            {
                // ReSharper disable once HeapView.ObjectAllocation.Possible
                // ReSharper disable once HeapView.ObjectAllocation
                foreach (var o in GameObjects)
                {
                    if (o is T albinoObject)
                    {
                        results.Add(o as T);
                    }
                }
            }

            return results;
        }

        public T Find<T>() where T : AlbinoObject
        {
            lock (GameObjects)
            {
                // ReSharper disable once HeapView.ObjectAllocation.Possible
                // ReSharper disable once HeapView.ObjectAllocation
                foreach (var o in GameObjects)
                {
                    if (o is T albinoObject)
                    {
                        return albinoObject;
                    }
                }
            }

            return null;
        }
        
        public bool Has<T>() where T : AlbinoObject
        {
            lock (GameObjects)
            {
                // ReSharper disable once HeapView.ObjectAllocation.Possible
                // ReSharper disable once HeapView.ObjectAllocation
                if (GameObjects.OfType<T>().Any())
                {
                    return true;
                }
            }

            return false;
        }

        public void Add(AlbinoObject gameObject)
        {
            lock (GameObjects)
            {
                GameObjects.Add(gameObject);
            }
            
        }
        
        public void Remove(AlbinoObject gameObject)
        {
            lock (GameObjects)
            {
                GameObjects.Remove(gameObject);
            }
        }

        protected void UpdateGameObjects(GameTime gameTime)
        {
            lock (GameObjects)
            {
                // ReSharper disable once HeapView.ObjectAllocation.Possible
                // ReSharper disable once HeapView.ObjectAllocation
                foreach (var o in GameObjects.Where(gameObject => gameObject is UpdateableGameObject))
                {
                    var gameObject = (UpdateableGameObject) o;
                    gameObject.Update(gameTime);
                }
            }
        }

        public abstract void Update(GameTime gameTime);

    }
}