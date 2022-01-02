using System;
using AlbinoLibraryMonogame.Components;
using AlbinoLibraryMonogame.Components.Rendering;
using Microsoft.Xna.Framework;

namespace AlbinoOffline.Objects
{
    public class Hans : Human
    {
        public Hans()
        {
            Get<SpriteRenderer>().TextureKey = "home";
        }
    }
}