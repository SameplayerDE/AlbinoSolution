using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;

namespace AlbinoLibraryMonogame.Rendering
{
    public class VirtualWindow
    {
        public readonly RenderTarget2D RenderTarget2D;
        public float ScaleX = 1f;
        public float ScaleY = 1f;

        public Vector2 Scale => new Vector2(ScaleX, ScaleY);

        private const double Aspect = 1.777777777777778;

        public Point Center => RenderTarget2D.Bounds.Center;

        public VirtualWindow(GraphicsDevice graphicsDevice, int width, int height)
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            RenderTarget2D = new RenderTarget2D(graphicsDevice, width, height);
        }

        public Vector2 GetMousePosition(GameWindow gameWindow)
        {
            var mousePos = Mouse.GetState().Position.ToVector2();
            var _windowsize= RenderTarget2D.Bounds.Size.ToVector2() * Scale;
            var center = _windowsize / 2;
            var topLeft = gameWindow.ClientBounds.Size.ToVector2() / 2;
            mousePos -= (topLeft.ToPoint() - center.ToPoint()).ToVector2();
            mousePos /= Scale;

            return mousePos;
        }

        public void Resize(GameWindow window)
        {
            var winHeight = window.ClientBounds.Size.Y;
            var winWidth = window.ClientBounds.Size.X;
            var aspect = (double)winWidth / (double)winHeight;
            
            if (aspect >= Aspect)
            {
                var scaleX = (float) window.ClientBounds.Size.Y / RenderTarget2D.Height;
                var diffX = scaleX - ScaleX;
                ScaleX += diffX;
                ScaleY += diffX;
            }
            else
            {
                var scaleX = (float) window.ClientBounds.Size.X / RenderTarget2D.Width;
                var diffX = scaleX - ScaleX;
                ScaleX += diffX;
                ScaleY += diffX;
            }
        }
        
    }

    public static class VirtualWindowExtensions
    {

        public static void Draw(this SpriteBatch spriteBatch, VirtualWindow virtualWindow, Vector2 position)
        {
            spriteBatch.Draw(virtualWindow.RenderTarget2D, position, Color.White);
        }
        
        public static void Draw(
            this SpriteBatch spriteBatch,
            VirtualWindow virtualWindow,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            float scale,
            SpriteEffects effects,
            float layerDepth)
        {
            spriteBatch.Draw(virtualWindow.RenderTarget2D, position, sourceRectangle, color, rotation, origin, virtualWindow.Scale * scale, effects, layerDepth);
        }
        
        public static void Draw(
            this SpriteBatch spriteBatch,
            VirtualWindow virtualWindow,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth)
        {
            spriteBatch.Draw(virtualWindow.RenderTarget2D, position, sourceRectangle, color, rotation, origin, virtualWindow.Scale * scale, effects, layerDepth);
        }

        public static void SetRenderTarget(this GraphicsDevice graphicsDevice, VirtualWindow virtualWindow)
        {
            graphicsDevice.SetRenderTarget(virtualWindow.RenderTarget2D);
        }
    }
}