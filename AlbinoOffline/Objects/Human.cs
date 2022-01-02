using AlbinoLibraryMonogame.Components;
using AlbinoLibraryMonogame.Components.Rendering;
using AlbinoLibraryMonogame.Objects;

namespace AlbinoOffline.Objects
{
    public class Human : UpdateableGameObject
    {
        public Human(string name = "")  : base(name)
        {
            Attach(new Transform());
            Attach(new HealthComponent());
            Attach(new SpriteRenderer());
            Attach(new HealthRenderer());
            
            Get<SpriteRenderer>().TextureKey = "zweiblatt";
        }
    }
}