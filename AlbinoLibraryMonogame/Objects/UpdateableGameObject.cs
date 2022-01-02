using System;
using System.Linq;
using AlbinoLibrary.ComponentSystem;
using Microsoft.Xna.Framework;

namespace AlbinoLibraryMonogame.Objects
{
    public abstract class UpdateableGameObject : AlbinoGameObject
    {
        
        public UpdateableGameObject(string name = "")  : base(name)
        {
        }
        
        public virtual void Update(GameTime gameTime)
        {
            // ReSharper disable once HeapView.ObjectAllocation.Possible
            // ReSharper disable once HeapView.ObjectAllocation
            foreach (var albinoComponent in Components.Where(component => component is IUpdateable))
            {
                var component = (IUpdateable) albinoComponent;
                component.Update(gameTime);
            }
        }
    }
}