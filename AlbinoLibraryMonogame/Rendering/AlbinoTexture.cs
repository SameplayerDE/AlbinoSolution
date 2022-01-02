using System;
using System.Linq;
using AlbinoLibraryMonogame.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlbinoLibraryMonogame.Rendering
{
    
    public enum SpriteMode
    {
        Single,
        Multiple
    }
    
    public sealed class AlbinoTexture
    {

        public static TextureLoader Loader;
        public string Path;
        public Texture2D Texture2D;
        public SpriteMode SpriteMode = SpriteMode.Single;
        public Texture2D[] Texture2Ds = null;
        public Rectangle[] Rectangles = null;
        public int Sprites = 0;
        public int SpritesPerX = 0;
        public int SpritesPerY = 0;
        public int SpritesW = 0;
        public int SpritesH = 0;

        public Texture2D Get(int index)
        {
            if (index >= Sprites)
            {
                return Texture2Ds.Last();
            }

            if (index < 0)
            {
                return Texture2Ds.First();
            }

            return Texture2Ds[index];

        }
        
        public Rectangle GetRect(int index)
        {

            index = index % Sprites;
            
            if (index >= Sprites)
            {
                return Rectangles.Last();
            }

            if (index < 0)
            {
                return Rectangles.First();
            }

            return Rectangles[index];

        }
        
        public Rectangle GetRect(int col, int row)
        {

            int index = 0;
            index = col + SpritesPerX * row;
            return GetRect(index);

        }
        
        public static AlbinoTexture LoadSingle(GraphicsDevice graphicsDevice, string path)
        {
            var final = new AlbinoTexture();
            final.Path = path;
            final.Texture2D = Loader.Find(path);
            return final;
        }
        
        public static AlbinoTexture LoadMultiple(GraphicsDevice graphicsDevice, string path, int perX, int perY)
        {
            var final = new AlbinoTexture();
            final.Path = path;
            final.SpriteMode = SpriteMode.Multiple;
            final.Texture2D = Loader.Find(path);
            
            if (perY == -1)
            {
                var width = final.Texture2D.Width / perX;
                perY = final.Texture2D.Height / width;
            }
            else
            {
                final.Sprites = perX * perY;
            }

            final.SpritesPerX = perX;
            final.SpritesPerY = perY;
            final.Sprites = perX * perY;
            final.Texture2Ds = new Texture2D[final.Sprites];
            final.Rectangles = new Rectangle[final.Sprites];

            var singleW = final.Texture2D.Width / perX;
            var singleH = final.Texture2D.Height / perY;
            
            final.SpritesH = final.Texture2D.Height / perY;
            final.SpritesW = final.Texture2D.Width / perX;

            for (int y = 0; y < perY; y++)
            {
                for (int x = 0; x < perX; x++)
                {
                    //Texture2D cropTexture = new Texture2D(graphicsDevice, singleW, singleH);
                    //Color[] data = new Color[singleW * singleH];
                    //final.Texture2D.GetData(0, new Rectangle(x * singleW, y * singleH, singleW, singleH), data, 0, data.Length);
                    //cropTexture.SetData(data);
                    //final.Texture2Ds[x * y] = cropTexture;
                    final.Rectangles[x + perX * y] = new Rectangle(x * singleW, y * singleH, singleW, singleH);
                }
            }

            return final;
        }
        
    }
}