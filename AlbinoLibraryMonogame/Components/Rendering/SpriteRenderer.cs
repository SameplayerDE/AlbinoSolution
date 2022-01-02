using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AlbinoLibrary.ComponentSystem;
using AlbinoLibrary.Resources;
using AlbinoLibraryMonogame.Rendering;
using AlbinoLibraryMonogame.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoLibraryMonogame.Components.Rendering
{
    public sealed class SpriteRenderer : AlbinoComponent, IDrawable
    {

        public static AlbinoTextureLoader Loader = null;
        public string TextureKey = string.Empty;
        public AlbinoTexture AlbinoTexture = null;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            if (Loader == null)
            {
                return;
            }
            
            var transform = Object.Get<Transform>();
            if (transform == null) return;
            
            AlbinoTexture ??= Loader.Find(TextureKey);
            var texture = AlbinoTexture.Texture2D;

            if (AlbinoTexture.SpriteMode == SpriteMode.Single)
            {
                spriteBatch.Draw(texture, transform.PositionVector2, null, Color.White, 0f, texture.Bounds.Center.ToVector2(), 1f, SpriteEffects.None, 0f);
            }
            else if (AlbinoTexture.SpriteMode == SpriteMode.Multiple)
            {
                var animationController = Object.Get<AnimationController>();

                if (animationController != null)
                {
                    var animation = animationController.Animation;
                    spriteBatch.Draw(
                        texture,
                        transform.PositionVector2,
                        AlbinoTexture.GetRect(animation.Index),
                        Color.White,
                        0f,
                        AlbinoTexture.GetRect(animation.Index).Size.ToVector2() / 2,
                        1f,
                        SpriteEffects.None,
                        1f
                    );

                    //spriteBatch.Draw(texture, transform.Vector2, Color.White);
                }
                else
                {
                    spriteBatch.Draw(
                        texture,
                        transform.PositionVector2,
                        AlbinoTexture.GetRect((int)(gameTime.TotalGameTime.TotalSeconds * 25)),
                        Color.White,
                        0f,
                        AlbinoTexture.GetRect(0).Center.ToVector2(),
                        10f,
                        SpriteEffects.None,
                        0f
                    );
                }
            }

        }

        public override byte[] GetBytes()
        {
            var writer = new SaveFileStream();

            writer.WriteString(GetType().FullName ?? GetType().Name);
            writer.WriteString(TextureKey);

            return new byte[] {0x00};
            return writer.ToFinalArray();
        }

        public override void FromBytes(byte[] data)
        {
            //TextureKey = BitConverter.ToString(reader.ReadBytes(BitConverter.ToInt32(reader.ReadBytes(4))));
        }
    }
}