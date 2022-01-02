using AlbinoLibraryMonogame.Components;
using AlbinoLibraryMonogame.Objects;

namespace AlbinoOffline.Objects
{
    public class Camera : UpdateableGameObject
    {

        public Camera(string name = "") : base(name)
        {
            Attach(new Transform());
        }
        
    }
}