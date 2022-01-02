using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using BoundingBox = AlbinoLibraryMonogame.Components.BoundingBox;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Transform = AlbinoLibraryMonogame.Components.Transform;

namespace AlbinoLibraryMonogame.Rendering
{
    public static class LineRenderer
    {

        private static Texture2D _line;
        
        public static void Init(GraphicsDevice graphicsDevice)
        {
            _line = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _line.SetData(new[] { Color.White });
        }
        
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness = 1f)
        {
            var distance = Vector2.Distance(point1, point2);
            var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            DrawLine(spriteBatch, point1, distance, angle, color, thickness);
        }

        public static void DrawBoundingBox(this SpriteBatch spriteBatch, BoundingBox boundingBox)
        {
            var Center = boundingBox.Object.Get<Transform>().Position + boundingBox.Center;
            var Size = boundingBox.Size;
            var maxX = Center.X + Size.X / 2;
            var minX = Center.X - Size.X / 2;
            
            var maxY = Center.Y + Size.Y / 2;
            var minY = Center.Y - Size.Y / 2;
            
            var maxZ = Center.Z + Size.Z / 2;
            var minZ = Center.Z - Size.Z / 2;
            LineRenderer.DrawLine(spriteBatch, new Vector2(minX, minY), new Vector2(maxX, minY), Color.Aqua);
            LineRenderer.DrawLine(spriteBatch, new Vector2(maxX, minY), new Vector2(maxX, maxY), Color.Aqua);
            LineRenderer.DrawLine(spriteBatch, new Vector2(maxX, maxY), new Vector2(minX, maxY), Color.Aqua);
            LineRenderer.DrawLine(spriteBatch, new Vector2(minX, maxY), new Vector2(minX, minY), Color.Aqua);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness = 1f)
        {
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(_line, point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }
        
    }
}