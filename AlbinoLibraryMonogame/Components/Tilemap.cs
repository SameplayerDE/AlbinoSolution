using System;
using System.Collections.Generic;
using System.IO;
using AlbinoLibrary.ComponentSystem;
using AlbinoLibraryMonogame.Objects;
using Microsoft.Xna.Framework;

namespace AlbinoLibraryMonogame.Components
{
    public class Tilemap : AlbinoComponent
    {

        public List<Tilebase> Tiles = new List<Tilebase>();

        public void LoadFromFile(string path)
        {
            if (File.Exists(path))
            {

                var defines = new Dictionary<string, Vector2>();
                
                var lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    if (line.StartsWith("#") || line.Length <= 0)
                    {
                        continue;
                    }
                    
                    var data = line.Split(" ");
                    
                    if (line.StartsWith("define"))
                    {
                        var keyword = data[0];
                        var key = data[1];
                        var c = data[2];
                        var r = data[3];
                        
                        defines.Add(key, new Vector2(Convert.ToInt32(c), Convert.ToInt32(r)));
                        
                    }
                    else
                    {
                        if (data.Length == 3) //has keyword
                        {
                            var x = data[0];
                            var y = data[1];
                            var key = data[2];
                            
                            Tiles.Add(new Tilebase() {Position = new Vector3(Convert.ToSingle(x), Convert.ToSingle(y), 0), Col = Convert.ToInt32(defines[key].X), Row = Convert.ToInt32(defines[key].Y)});

                        }else if (data.Length == 4) //no keyword
                        {
                            var x = data[0];
                            var y = data[1];
                            var col = data[2];
                            var row = data[3];
                    
                            Tiles.Add(new Tilebase() {Position = new Vector3(Convert.ToSingle(x), Convert.ToSingle(y), 0), Col = Convert.ToInt32(col), Row = Convert.ToInt32(row)});

                        }
                    
                        
                    }
                }
            }
        }
        
        public override byte[] GetBytes()
        {
            throw new System.NotImplementedException();
        }

        public override void FromBytes(byte[] data)
        {
            throw new System.NotImplementedException();
        }
    }
}