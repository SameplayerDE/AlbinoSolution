using System;
using AlbinoLibrary.ComponentSystem;
using AlbinoLibraryMonogame;
using AlbinoLibraryMonogame.Components;
using AlbinoLibraryMonogame.Components.Rendering;
using AlbinoLibraryMonogame.Objects;
using AlbinoLibraryMonogame.Rendering;
using AlbinoOffline.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BoundingBox = AlbinoLibraryMonogame.Components.BoundingBox;
using Gr = AlbinoOffline.Objects.Grid;
using Tilemap = AlbinoOffline.Objects.Tilemap;

namespace AlbinoOffline.Scenes
{
    public class Zweiblattdorf : AlbinoScene
    {
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        public static Zweiblattdorf Instance { get; } = new Zweiblattdorf();

        static Zweiblattdorf()
        {
            
        }
        
        private Zweiblattdorf()
        {
            
            //World.Add(new Camera("camera"));

            Gr grid = new Gr();

            Tilemap @base = new Tilemap();
            @base.Parent = grid;
            
            Tilemap @top = new Tilemap();
            @top.Parent = grid;
            
           
            @base.Get<TilemapRenderer>().TextureKey = "tileset";
            @base.Get<AlbinoLibraryMonogame.Components.Tilemap>().LoadFromFile("zweiblattdorf.txt");
            /*for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    @base.Get<AlbinoLibraryMonogame.Components.Tilemap>().Tiles.Add(new Tilebase() {Position = new Vector3(j, i, 0), Col = 35, Row = 3});
                }
            }*/

            World.Add(grid);
            World.Add(@base);
            
            World.Add(new Player());
            
            
            LoadFromFile("zweiblattdorf_ent.txt");

        }

        public override void Prepare()
        {
            if (SceneManager.Instance.Prev == null) return;
            //var transform = World.Find("home").Get<BoundingBox>();
            //var camera = World.Find("camera");
            //World.Find("player").Get<Transform>().Position = transform.Center;
            //World.Find("player").Get<Transform>().Position.Y += transform.Size.Y;
        }

        public override void InitialPrepare()
        {
            throw new System.NotImplementedException();
        }

        public override void SubsequentPrepare()
        {
            throw new System.NotImplementedException();
        }

        public override void Init(GraphicsDevice graphicsDevice)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            World.Update(gameTime);

            //var player = World.Find("player");
            //var position = player.Get<Transform>().PositionVector2;
            //{
                //SceneManager.Instance.ChangeScene(Home.Instance);
            //}

            //var camera = World.Find("camera");
            var player = World.Find("player");

            //camera.Get<Transform>().Position = player.Get<Transform>().Position - (new Vector3(1280, 720, 0) / 2);

            foreach (var portal in World.FindAll<TeleportTrigger>())
            {
                if (portal.Get<BoundingBox>().Contains(player.Get<Transform>().Position))
                {

                    var destination = portal.Get<DictionaryComponent<string, string>>().Get("destination");
                    if (destination == "Home")
                    {
                        SceneManager.Instance.ChangeScene(Home.Instance);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            World.Draw(spriteBatch, gameTime);
            foreach (var portal in World.FindAll<TeleportTrigger>())
            {
                spriteBatch.DrawBoundingBox(portal.Get<BoundingBox>());
            }
            
        }
    }
}