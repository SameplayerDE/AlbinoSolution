using AlbinoLibrary.ComponentSystem;
using AlbinoLibraryMonogame.Components;
using AlbinoLibraryMonogame.Components.Rendering;

namespace AlbinoOffline.Objects
{
    public class Sprite : AlbinoGameObject
    {
        public Sprite()
        {
            Attach(new Transform());
            Attach(new SpriteRenderer());
        }
    }
}