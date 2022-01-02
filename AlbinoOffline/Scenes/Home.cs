using AlbinoLibraryMonogame;
using AlbinoLibraryMonogame.Components;
using AlbinoLibraryMonogame.Rendering;
using AlbinoOffline.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using BoundingBox = AlbinoLibraryMonogame.Components.BoundingBox;

namespace AlbinoOffline.Scenes
{
    public class Home : AlbinoScene
    {
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        public static Home Instance { get; } = new Home();

        
        static Home()
        {
            
        }
        
        private Home()
        {
            World.Add(new Hans());
            World.Add(new Player());
            
            World.Add(new Camera("camera"));
            World.Add(new TeleportTrigger("zweiblattdorf", "zweiblattdorf"));
            
            World.Find("zweiblattdorf").Get<BoundingBox>().Center = new Vector3(-17, 275, 0);
            World.Find("zweiblattdorf").Get<BoundingBox>().Size = new Vector3(25, 25, 0);
        }

        public override void Prepare()
        {
            var transform = World.Find("zweiblattdorf").Get<BoundingBox>();
            var camera = World.Find("camera");
            World.Find("player").Get<Transform>().Position = transform.Center;
            World.Find("player").Get<Transform>().Position.Y -= transform.Size.Y;
            
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
            var camera = World.Find("camera");
            var player = World.Find("player");
            camera.Get<Transform>().Position = player.Get<Transform>().Position - (new Vector3(1280, 720, 0) / 2);
            
            var teleportHome = World.Find("zweiblattdorf");
            
            
            if (teleportHome.Get<BoundingBox>().Contains(player.Get<Transform>().Position))
            {
                SceneManager.Instance.ChangeScene(Zweiblattdorf.Instance);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            World.Draw(spriteBatch, gameTime);
            var teleportHome = World.Find("zweiblattdorf");
            spriteBatch.DrawBoundingBox(teleportHome.Get<BoundingBox>());
        }
    }
}