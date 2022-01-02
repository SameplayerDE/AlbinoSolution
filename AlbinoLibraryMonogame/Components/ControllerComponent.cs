using System;
using System.IO;
using AlbinoLibrary.ComponentSystem;
using Microsoft.Xna.Framework;

namespace AlbinoLibraryMonogame.Components
{
    public class ControllerComponent : AlbinoComponent, IUpdateable
    {

        public float Speed = 60f;
        private float _horizontal = 0f;
        private float _vertical = 0f;
        
        public void Update(GameTime gameTime)
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = -Input.GetAxis("Vertical");

            var direction = new Vector2(_horizontal, _vertical);
            
            if (direction.Length() == 0) return;
            direction.Normalize();
            direction *=  Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            Object.Get<Transform>()?.Move(direction);

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