using System;
using System.IO;
using AlbinoLibrary.ComponentSystem;
using AlbinoLibraryMonogame.Components;
using AlbinoLibraryMonogame.Objects;
using Microsoft.Xna.Framework;
using BoundingBox = AlbinoLibraryMonogame.Components.BoundingBox;

namespace AlbinoOffline.Objects
{
    public class TeleportTrigger : UpdateableGameObject
    {
        public TeleportTrigger(string destination, string name = "") : base(name)
        {
            Attach(new BoundingBox());
            Attach(new Transform());
            Attach(new DictionaryComponent<string, string>());
            
            Get<DictionaryComponent<string, string>>().Add("destination", destination);
        }
    }
}