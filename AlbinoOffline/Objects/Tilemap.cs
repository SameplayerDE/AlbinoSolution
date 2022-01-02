using AlbinoLibrary.ComponentSystem;
using AlbinoLibraryMonogame.Components;
using AlbinoLibraryMonogame.Components.Rendering;

namespace AlbinoOffline.Objects
{
    public class Tilemap : AlbinoObject
    {

        public Tilemap(string name = "") : base(name)
        {
            Attach(new AlbinoLibraryMonogame.Components.Tilemap());
            Attach(new TilemapRenderer());
            Attach(new Transform());
        }
        
    }
}