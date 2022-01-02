using System.IO;
using AlbinoLibrary.ComponentSystem;

namespace AlbinoLibraryMonogame.Components
{
    public class HealthComponent : AlbinoComponent
    {
        public int Health = 100;
        public bool Alive => Health > 0;

        public void Decrease(int amount = 1)
        {
            Health -= amount;
        }

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