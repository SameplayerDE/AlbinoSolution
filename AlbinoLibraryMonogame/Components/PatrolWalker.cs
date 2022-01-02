using System;
using System.Collections.Generic;
using System.IO;
using AlbinoLibrary.ComponentSystem;
using AlbinoLibraryMonogame.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoLibraryMonogame.Components
{

    public struct PatrolPoint
    {
        public Vector2 Destination;
    }
    
    public class PatrolWalker : AlbinoComponent, IUpdateable, IDrawable
    {

        public List<PatrolPoint> PatrolPoints;
        private int _index = 0;

        public PatrolWalker()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            PatrolPoints = new List<PatrolPoint>();
        }

        public void AddPatrolPoint(PatrolPoint patrolPoint)
        {
            PatrolPoints.Add(patrolPoint);
        }
        
        public void Update(GameTime gameTime)
        {
            var transform = Object.Get<Transform>();
            if (Vector2.Distance(PatrolPoints[_index].Destination, transform.PositionVector2) < 1f)
            {
                _index = (_index + 1) % PatrolPoints.Count;
            }
            var distance = Vector2.Subtract(PatrolPoints[_index].Destination, transform.PositionVector2);
            distance.Normalize();
            distance *= (float)gameTime.ElapsedGameTime.TotalSeconds;
            distance *= 32f;
            
            transform?.Move(distance.X, distance.Y);
        }

        public bool Enabled { get; }
        public int UpdateOrder { get; }
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var transform = Object.Get<Transform>();
            spriteBatch.DrawLine(PatrolPoints[_index].Destination, transform.PositionVector2, Color.Green, 5f);
        }

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