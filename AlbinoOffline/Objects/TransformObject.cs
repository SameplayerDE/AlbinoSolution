using AlbinoLibrary.ComponentSystem;
using AlbinoLibraryMonogame.Components;
using AlbinoLibraryMonogame.Components.Rendering;

namespace AlbinoOffline.Objects
{
    public class TransformObject : AlbinoGameObject
    {
        public TransformObject()
        {
            Attach(new Transform());
            Attach(new SpriteRenderer());
            Get<SpriteRenderer>().TextureKey = "human";
        }
    }
}