using AlbinoLibrary.ComponentSystem;
using AlbinoLibraryMonogame.Components;
using AlbinoLibraryMonogame.Components.Rendering;

namespace AlbinoOffline.Objects
{
    public class Grid : AlbinoObject
    {

        public Grid(string name = "") : base(name)
        {

            Attach(new AlbinoLibraryMonogame.Components.Grid());
            Attach(new Transform());

        }
        
    }
}