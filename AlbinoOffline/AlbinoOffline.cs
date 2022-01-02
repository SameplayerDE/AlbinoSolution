using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using AlbinoLibraryMonogame;
using AlbinoLibraryMonogame.Components;
using AlbinoLibraryMonogame.Components.Rendering;
using AlbinoLibraryMonogame.Objects;
using AlbinoLibraryMonogame.Rendering;
using AlbinoOffline.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AlbinoOffline.Resources;
using AlbinoOffline.Scenes;
using Microsoft.Xna.Framework.Input;

namespace AlbinoOffline
{
    public class AlbinoOffline : Game
    {
        // ReSharper disable once NotAccessedField.Local
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        public static GraphicsDeviceManager GraphicsDeviceManager;
        // ReSharper disable once NotAccessedField.Local
        private SpriteBatch _spriteBatch;
        
        private VirtualWindow _virtualWindow;
        private World world;

        private SpriteFont _font;

        public AlbinoOffline()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 16);
            IsFixedTimeStep = true;
            Window.AllowUserResizing = true;
            // ReSharper disable once HeapView.DelegateAllocation
            Window.ClientSizeChanged += OnResize;
            this.Exiting += OnExiting;

            GraphicsDeviceManager.PreferredBackBufferHeight = 720;
            GraphicsDeviceManager.PreferredBackBufferWidth = 1280;

            
            GraphicsDeviceManager.ApplyChanges();

        }
        
        private void OnResize(object sender, EventArgs e)
        {
            _virtualWindow.Resize(Window);
        }
        
        private void OnExiting(object sender, EventArgs args)
        {
            
            Zweiblattdorf.Instance.World.SaveToFile();
            
        }

        protected override void Initialize()
        {
            SceneManager.Instance.Current = Zweiblattdorf.Instance;
            LineRenderer.Init(GraphicsDevice);

            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _virtualWindow = new VirtualWindow(GraphicsDevice, 1280, 720);
            _virtualWindow.Resize(Window);
            
            base.Initialize();
        }

        

        protected override void LoadContent()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("debug");
            
            TextureManager.Instance.LoadContent(Content);
            SpriteEffectManager.Instance.LoadContent(Content);
            
            //_human.Get<HealthRenderer>().Font = _font;
            //_player.Get<HealthRenderer>().Font = _font;
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update(gameTime);

            lock (SceneManager.Instance.ChangeMutex)
            {
                SceneManager.Instance.Current?.Update(gameTime);
            }
            
            SceneManager.Instance.ExecuteChange();
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.SetRenderTarget(_virtualWindow);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            lock (SceneManager.Instance.ChangeMutex)
            {
                if (SceneManager.Instance.Current.World.Has<Camera>())
                {
                    var camera = SceneManager.Instance.Current.World.Find<Camera>();
                    var position = -camera.Get<Transform>().Position;

                    var x = (int)position.X;
                    var y = (int)position.Y;
                    var z = position.Z;
                    
                    _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateTranslation(new Vector3(x, y, z)));
                }
                else
                {
                    _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                }
                SceneManager.Instance.Current?.Draw(_spriteBatch, gameTime);
                _spriteBatch.End();
            }

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(samplerState: SamplerState.AnisotropicClamp, sortMode: SpriteSortMode.Immediate, blendState: BlendState.AlphaBlend);
            _spriteBatch.Draw(
                _virtualWindow,
                Window.ClientBounds.Size.ToVector2() / 2,
                null,
                Color.White,
                0f,
                _virtualWindow.Center.ToVector2(),
                1f,
                SpriteEffects.None,
                0f
                );
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
