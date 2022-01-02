using AlbinoLibrary.ComponentSystem;
using Microsoft.Xna.Framework;

namespace AlbinoLibraryMonogame.Components
{
    public class Grid : AlbinoComponent
    {

        public Vector3 CellSize = Vector3.One;
        public Vector3 CellGap = Vector3.Zero;
        
        public override byte[] GetBytes()
        {
            throw new System.NotImplementedException();
        }

        public override void FromBytes(byte[] data)
        {
            throw new System.NotImplementedException();
        }
    }
}