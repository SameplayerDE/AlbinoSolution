using System;
using System.IO;

namespace AlbinoLibrary.ComponentSystem
{
    public abstract class AlbinoComponent
    {
        public AlbinoObject Object;
        public string Tag => Object.Tag;

        protected AlbinoComponent()
        {
            
        }

        public abstract byte[] GetBytes();
        public abstract void FromBytes(byte[] data);
    }
}