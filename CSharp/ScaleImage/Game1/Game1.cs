using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D img;
        Vector2 position;
        int slimSpeed;
        int speedX;
        int speedY;
        float angle;
        float scale;
        float scaleSpeed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            position = new Vector2(100, 100);
            slimSpeed = 5;
            speedX = 5;
            speedY = 5;
            scale = 1.0f;
            scaleSpeed = -0.01f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            img = this.Content.Load<Texture2D>("slimePurple2");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            speedX = Convert.ToInt32((slimSpeed * 60.0f) * (float)gameTime.ElapsedGameTime.TotalSeconds);

            position.X += speedX;
            if ((position.X + img.Width >= GraphicsDevice.Viewport.Width) || (position.X <= 0))
            {
                slimSpeed = -slimSpeed;
            }
            if ((position.Y + img.Height >= GraphicsDevice.Viewport.Height) || (position.Y <= 0))
            {
                slimSpeed = -slimSpeed;
            }
            scale += scaleSpeed;
            if (scale <= 0.5f || scale >=1.0f) scaleSpeed = -scaleSpeed;
              //position.Y += speedY;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            SpriteEffects effect = SpriteEffects.None;
            angle = MathHelper.ToRadians(0.0f);
            if (speedX > 0)
                effect = SpriteEffects.FlipHorizontally;
             _spriteBatch.Draw(img, position, null, Color.White, angle, new Vector2(img.Width/2 , img.Height), new Vector2(scale,scale), effect, 0);
             _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}