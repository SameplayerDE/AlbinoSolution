using System;
using System.IO;
using AlbinoLibraryMonogame;
using AlbinoLibraryMonogame.Components;
using AlbinoOffline.Objects;
using Microsoft.Xna.Framework;
using BoundingBox = AlbinoLibraryMonogame.Components.BoundingBox;

namespace AlbinoOffline.Scenes
{
    public abstract class AlbinoScene : Scene
    {
        public virtual void LoadFromFile(string path)
        {
            if (!File.Exists(path)) return;
            
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                if (line.StartsWith("#") || line.Length <= 0)
                {
                    continue;
                }

                var data = line.Split(" ");

                if (line.StartsWith("portal"))
                {
                    var keyword = data[0];
                    var key = data[1];
                    var x = data[2];
                    var y = data[3];
                    var w = data[4];
                    var h = data[5];

                    var portal = new TeleportTrigger(key);
                    
                    portal.Get<Transform>().Position.X = Convert.ToSingle(x);
                    portal.Get<Transform>().Position.Y = Convert.ToSingle(y);

                    portal.Get<BoundingBox>().Size = new Vector3(Convert.ToSingle(w), Convert.ToSingle(h), 0);
                    
                    World.Add(portal);
                    
                }
            }
        }
    }
}