using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace LunarLander
{
    public class Lander
    {
        public Vector2 position { get; set; } = Vector2.Zero;
        public Vector2 velocity { get; set; } = Vector2.Zero;
        public float angle { get; set; } = 0.0f;
        public bool engineOn { get; set; } = false;
        public float speed { get; set; } = 0.02f;
        private float speedMax = 2.0f;
        public Texture2D img;
        public Texture2D imgEngine;
        /*
        public void update()
        {

        }*/
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Lander lander;
        Texture2D img;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Lander lander = new Lander();
           /* {
                position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
                img = Content.Load<Texture2D>("ship"),
                imgEngine = Content.Load<Texture2D>("engine")
            };*/
           lander.position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            img = Content.Load<Texture2D>("engine");


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
            _spriteBatch.Begin();


            // TODO: Add your drawing code here
            Vector2 orinImg = new Vector2(lander.img.Width / 2, lander.img.Height / 2);
            _spriteBatch.Draw(img,lander.position, null, Color.White, 0.0f, orinImg, new Vector2(1.0f,1.0f), SpriteEffects.None, 0);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}