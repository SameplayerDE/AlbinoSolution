using System;
using AlbinoLibrary.ComponentSystem;
using AlbinoLibraryMonogame.Rendering;
using AlbinoLibraryMonogame.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoLibraryMonogame.Components.Rendering
{
    public class TilemapRenderer : AlbinoComponent, IDrawable
    {
        
        public static AlbinoTextureLoader Loader = null;
        public string TextureKey = string.Empty;
        public AlbinoTexture AlbinoTexture = null;
        
        public override byte[] GetBytes()
        {
            throw new System.NotImplementedException();
        }

        public override void FromBytes(byte[] data)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            if (Loader == null)
            {
                return;
            }
            
            var transform = Object.Get<Transform>();
            var grid = Object.Parent.Get<Grid>();
            if (transform == null) return;
            if (grid == null) return;
            
            AlbinoTexture ??= Loader.Find(TextureKey);
            var texture = AlbinoTexture.Texture2D;

            if (AlbinoTexture.SpriteMode == SpriteMode.Single)
            {
                var tilemap = Object.Get<Tilemap>();

                lock (tilemap.Tiles)
                {
                    foreach (var tilebase in tilemap.Tiles) 
                    {
                        spriteBatch.Draw(texture,  new Vector2(tilebase.Position.X, tilebase.Position.Y), null, Color.White, 0f, texture.Bounds.Center.ToVector2(), 1f, SpriteEffects.None, 0f);
                    }
                }
            }
            else if (AlbinoTexture.SpriteMode == SpriteMode.Multiple)
            {
                var tilemap = Object.Get<Tilemap>();

                lock (tilemap.Tiles)
                {
                    foreach (var tilebase in tilemap.Tiles)
                    {
                        spriteBatch.Draw(
                            texture,
                            new Vector2((int)tilebase.Position.X, (int)tilebase.Position.Y) * (new Vector2(grid.CellSize.X, grid.CellSize.Y) * 16),
                            AlbinoTexture.GetRect(tilebase.Col, tilebase.Row),
                            Color.White,
                            0f,
                            AlbinoTexture.GetRect(tilebase.Col, tilebase.Row).Size.ToVector2() / 2,
                            1f,
                            SpriteEffects.None,
                            1f
                        );
                    }
                }
            }
            
            
            
        }
    }
}