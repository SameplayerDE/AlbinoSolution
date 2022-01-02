using System;
using System.IO;
using AlbinoLibrary.ComponentSystem;
using Microsoft.Xna.Framework;

namespace AlbinoLibraryMonogame.Components
{
    public class RandomWalker : AlbinoComponent, IUpdateable
    {

        public Random Random = new Random();
        public Vector2 Destination;
        public double IdleDuration = 1000;
        public double IdleStarted = -1000;

        public RandomWalker()
        {

        }
        
        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - IdleStarted >= IdleDuration)
            {
                Destination.X += Random.Next(-5, 5);
                Destination.Y += Random.Next(-5, 5);
                IdleStarted = gameTime.TotalGameTime.TotalMilliseconds;
            }
            var transform = Object.Get<Transform>();
            var distance = Vector2.Subtract(Destination + transform.PositionVector2, transform.PositionVector2) / 100f;
            
            transform?.Move(distance.X, distance.Y);

        }

        public bool Enabled { get; }
        public int UpdateOrder { get; }
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        public override byte[] GetBytes()
        {
            return new byte[] {0x00};
        }

        public override void FromBytes(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}