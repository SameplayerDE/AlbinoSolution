using AlbinoLibrary.ComponentSystem;
using Microsoft.Xna.Framework;

namespace AlbinoLibraryMonogame.Components
{
    public class BoundingBox : AlbinoComponent
    {
        
        public Vector3 Center = Vector3.Zero;
        public Vector3 Size = Vector3.One;
        public Quaternion Rotation = Quaternion.Identity;

        public bool Contains(Vector3 position)
        {

            var center = Object.Get<Transform>().Position + Center;
            
            var maxX = center.X + Size.X / 2;
            var minX = center.X - Size.X / 2;
            
            var maxY = center.Y + Size.Y / 2;
            var minY = center.Y - Size.Y / 2;
            
            var maxZ = center.Z + Size.Z / 2;
            var minZ = center.Z - Size.Z / 2;

            if (position.X >= minX && position.X <= maxX)
            {
                if (position.Y >= minY && position.Y <= maxY)
                {
                    if (position.Z >= minZ && position.Z <= maxZ)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public bool Intersects(BoundingBox boundingBox)
        {
            var omaxX = boundingBox.Center.X + boundingBox.Size.X / 2;
            var ominX = boundingBox.Center.X - boundingBox.Size.X / 2;
            
            var omaxY = boundingBox.Center.Y + boundingBox.Size.Y / 2;
            var ominY = boundingBox.Center.Y - boundingBox.Size.Y / 2;
            
            var omaxZ = boundingBox.Center.Z + boundingBox.Size.Z / 2;
            var ominZ = boundingBox.Center.Z - boundingBox.Size.Z / 2;
            
            var maxX = Center.X + Size.X / 2;
            var minX = Center.X - Size.X / 2;
            
            var maxY = Center.Y + Size.Y / 2;
            var minY = Center.Y - Size.Y / 2;
            
            var maxZ = Center.Z + Size.Z / 2;
            var minZ = Center.Z - Size.Z / 2;

            
            return false;
        }
        
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