using System;
using System.IO;
using AlbinoLibrary.ComponentSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoLibraryMonogame.Components.Rendering
{
    public class HealthRenderer : AlbinoComponent, IDrawable
    {
        
        public SpriteFont Font = null;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Font == null)
            {
                return;
            }
            
            var healthComp = Object.Get<HealthComponent>();
            var transform = Object.Get<Transform>();

            if (transform != null)
            {
                spriteBatch.DrawString(Font, healthComp?.Health.ToString(), transform.PositionVector2, Color.White);
            }
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