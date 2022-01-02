using System.IO;
using AlbinoLibrary.ComponentSystem;

namespace AlbinoLibraryMonogame.Components
{
    public class FieldOfViewComponent : AlbinoComponent
    {
        public override byte[] GetBytes()
        {
            return new byte[] {0x00};
        }

        public override void FromBytes(byte[] data)
        {
            throw new System.NotImplementedException();
        }
    }
}