using System;
using System.IO;
using System.Linq;
using AlbinoLibrary.ComponentSystem;
using AlbinoLibrary.Resources;
using AlbinoLibraryMonogame.Components.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = AlbinoLibraryMonogame.Components.IDrawable;

namespace AlbinoLibraryMonogame
{
    [Serializable]
    public class World : Simulation
    {
        public override void Update(GameTime gameTime)
        {
            UpdateGameObjects(gameTime);
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            lock (GameObjects)
            {
                // ReSharper disable once HeapView.ObjectAllocation.Possible
                foreach (var albinoGameObject in GameObjects)
                {
                    lock (albinoGameObject.Components)
                    {
                        // ReSharper disable once HeapView.ObjectAllocation
                        foreach (var o in albinoGameObject.Components.Where(o => o is IDrawable))
                        {
                            var component = o as IDrawable;
                            // ReSharper disable once PossibleNullReferenceException
                            component.Draw(spriteBatch, gameTime);
                        }
                    }
                }
            }
        }

        public void SaveToFile()
        {
            var file = File.Create("World.bin");
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            var writer = new SaveFileStream(file);
            
            /*
             * DATA
             */
            writer.WriteInt32(GameObjects.Count);
            foreach (var gameObject in GameObjects)   
            {
                //writer.Write(gameObject.GetBytes().Length);
                writer.Write(gameObject.GetBytes());
            }
            
            writer.Flush();
            file.Flush();
            writer.Close();
            file.Close();

        }
        
        public void LoadFromFile()
        {
            GameObjects.Clear();
            var file = File.OpenRead("World.bin");
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            var reader = new SaveFileStream(file);
            
            /*
             * DATA
             */
            var objectCount = reader.ReadInt32();
            
            for (int i = 0; i < objectCount; i++)
            {
                var bytecount = reader.ReadInt32();
                AlbinoObject albinoObject = new AlbinoObject();
                //albinoObject.FromBytes(reader.Read(bytecount));
                Add(albinoObject);
            }
           
            reader.Close();
            file.Close();

        }
        
    }
}