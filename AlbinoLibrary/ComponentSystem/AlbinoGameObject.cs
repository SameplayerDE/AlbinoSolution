using System;
using System.Collections.Generic;

namespace AlbinoLibrary.ComponentSystem
{
    [Serializable]
    public abstract class AlbinoGameObject : AlbinoObject
    {
        public AlbinoGameObject(string name = "") : base(name)
        {
        }
    }
}