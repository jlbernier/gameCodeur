using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    public class Jelly
    {
        public Vector2 position;
        public int slimSpeed;
        public int speedX;
        public int speedY;
        public float angle;
        public float scale;
        public float scaleSpeed;
    }
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
        List<Jelly> jellyList;
        Random rnd;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            jellyList = new List<Jelly>();
            rnd = new Random();
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
            for (int i = 0; i<=20; i++) 
            {
                Jelly myJelly = new Jelly();
                jellyList.Add(myJelly);
                int x = rnd.Next(0, GraphicsDevice.Viewport.Width - img.Width);
                int y = rnd.Next(img.Height, GraphicsDevice.Viewport.Height);
                myJelly.position = new Vector2(x, y);
                myJelly.speedX = rnd.Next(1, 5);
                myJelly.speedY = 5;
                myJelly.scale = 1.0f;
                myJelly.scaleSpeed = -0.01f;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            speedX = Convert.ToInt32((slimSpeed * 60.0f) * (float)gameTime.ElapsedGameTime.TotalSeconds);
            foreach (Jelly item in jellyList)
            {
                item.position.X += item.speedX;

                if (item.position.X + img.Width/2 >= GraphicsDevice.Viewport.Width)
                {
                    item.position.X = GraphicsDevice.Viewport.Width - img.Width / 2;
                    item.speedX = -item.speedX;
                }
                if (item.position.X <= img.Width / 2)
                {
                    item.position.X = img.Width / 2;
                    item.speedX = -item.speedX;
                }
                if ((item.position.Y + img.Height >= GraphicsDevice.Viewport.Height) || (item.position.Y <= 0))
                {
                    item.speedY = -item.speedY;
                }
                item.scale += item.scaleSpeed;
                if (item.scale <= 0.5f || item.scale >= 1.0f) item.scaleSpeed = -item.scaleSpeed;
                //position.Y += speedY;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            SpriteEffects effect = SpriteEffects.None;
            angle = MathHelper.ToRadians(0.0f);
            foreach (Jelly item in jellyList)
            {
                if (item.speedX > 0)
                    effect = SpriteEffects.FlipHorizontally;

                _spriteBatch.Draw(img, item.position, null, Color.White, 0, new Vector2(img.Width / 2, img.Height), new Vector2(item.scale, item.scale), effect, 0);

            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}