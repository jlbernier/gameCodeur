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
        public int oldSpeedX;
        public int speedY;
        public float angle;
        public float scale;
        public float scaleSpeed;
        public bool isClick;
        public Color colorSlim;

    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D img;
        List<Jelly> jellyList;
        Random rnd;
        MouseState oldMouseState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            jellyList = new List<Jelly>();
            rnd = new Random();
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //position = new Vector2(100, 100);
 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            img = this.Content.Load<Texture2D>("metalPanel");
            for (int i = 0; i<=20; i++) 
            {
                Jelly myJelly = new Jelly();
                jellyList.Add(myJelly);
                // dépend de l'origine défini dans le draw
                //int x = rnd.Next(0, GraphicsDevice.Viewport.Width - img.Width);
                //int y = rnd.Next(img.Height, GraphicsDevice.Viewport.Height);
                int x = rnd.Next(0, GraphicsDevice.Viewport.Width - img.Width);
                int y = rnd.Next(0, GraphicsDevice.Viewport.Height- img.Height);
                myJelly.position = new Vector2(x, y);
                myJelly.speedX = rnd.Next(1, 5);
                myJelly.slimSpeed = myJelly.speedX;
                myJelly.speedY = 5;
                myJelly.scale = 1.0f;
                myJelly.scaleSpeed = -0.01f;
                myJelly.colorSlim = Color.White * 0.5f;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            bool bClick = false;
            bool allreadyClick = false;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            MouseState newMouseState = Mouse.GetState();
            if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                bClick = true;
            }
            oldMouseState = newMouseState;
            for (int i = jellyList.Count - 1; i >= 0; i--)
            {
                Jelly item = jellyList[i];
                item.position.X += item.speedX;

                if (item.position.X + img.Width >= GraphicsDevice.Viewport.Width)
                {
                    item.position.X = GraphicsDevice.Viewport.Width - img.Width;
                    item.speedX = -item.speedX;
                    item.slimSpeed = item.speedX;
                }
                if (item.position.X <= 0)
                {
                    item.position.X = 0;
                    item.speedX = -item.speedX;
                    item.slimSpeed = item.speedX;
                }
                if ((item.position.Y + img.Height >= GraphicsDevice.Viewport.Height) || (item.position.Y <= 0))
                {
                    item.speedY = -item.speedY;
                }
                item.scale += item.scaleSpeed;
                if (item.scale <= 0.5f || item.scale >= 1.0f) item.scaleSpeed = -item.scaleSpeed;
                //position.Y += speedY;
                if (bClick && !allreadyClick)
                {
                    if (newMouseState.X >= item.position.X &&
                        newMouseState.X <= item.position.X + img.Width &&
                        newMouseState.Y >= item.position.Y &&
                        newMouseState.Y <= item.position.Y + img.Height)
                    {
                        allreadyClick = true;
                        if (item.isClick == true)
                        { 
                            item.speedX = item.slimSpeed;
                            item.colorSlim = Color.White * 0.5f;
                            item.isClick = false;
                        }
                        else
                        {
                            item.slimSpeed = item.speedX;
                            item.speedX = 0;
                            item.colorSlim = Color.Red;

                            item.isClick = true;
                        }

                    }
                }
                
            }
                     
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(50,50,50,50));

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            SpriteEffects effect = SpriteEffects.None;
            foreach (Jelly item in jellyList)
            {
                if (item.speedX > 0)
                    effect = SpriteEffects.FlipHorizontally;
                // pour change l'origine de l'image au centre en bas : new Vector2(img.Width / 2, img.Height)
                //_spriteBatch.Draw(img, item.position, null, Color.White *0.5f, 0, new Vector2(img.Width / 2, img.Height), new Vector2(item.scale, item.scale), effect, 0);
                _spriteBatch.Draw(img, item.position, null, item.colorSlim, 0, new Vector2(0, 0), new Vector2(item.scale, item.scale), effect, 0);

            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}