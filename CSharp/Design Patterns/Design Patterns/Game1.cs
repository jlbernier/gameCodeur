using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using SingletonName;
using Observer;
namespace Design_Patterns
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Singleton
            Singleton instance1 = Singleton.GetInstance();
            Singleton instance2 = Singleton.GetInstance();
            instance1.Value = "aaa";
            instance2.Value = "bbb";
            Debug.WriteLine($"instance1.Value: {instance1.Value}");
            //Observer
            Magasin appleStore = new Magasin();

            Client yoan = new Client("Yoan");
            Client jeremy = new Client("Jérémy");

            appleStore.Register(yoan);
            appleStore.Register(jeremy);

            appleStore.DoSomething();
            appleStore.DoSomething();
            appleStore.DoSomething();
            appleStore.DoSomething();

            appleStore.Unregister(jeremy);

            appleStore.DoSomething();
            appleStore.DoSomething();
            appleStore.DoSomething();
            appleStore.DoSomething();
            //
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}