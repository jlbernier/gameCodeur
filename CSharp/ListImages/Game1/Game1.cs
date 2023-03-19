using ListImages;
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
        public Texture2D img;

    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D imgBackground0;
        Texture2D imgBackground1;
        Texture2D imgBackground2;
        Texture2D imgBackground3;
        Background background0;
        Background background1;
        Background background2;
        Background background3;
 //       Texture2D img;
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
            imgBackground0 = this.Content.Load<Texture2D>("urban_scrolling0");
            imgBackground1 = this.Content.Load<Texture2D>("urban_scrolling1");
            imgBackground2 = this.Content.Load<Texture2D>("urban_scrolling2");
            imgBackground3 = this.Content.Load<Texture2D>("urban_scrolling3");
            background0 = new Background(imgBackground0, -1);
            background1 = new Background(imgBackground1, -3);
            background2 = new Background(imgBackground2, -5);
            background3 = new Background(imgBackground3, -10);

            for (int i = 0; i<=20; i++) 
            {
                Jelly myJelly = new Jelly();
                jellyList.Add(myJelly);
                // dépend de l'origine défini dans le draw
                //int x = rnd.Next(0, GraphicsDevice.Viewport.Width - img.Width);
                //int y = rnd.Next(img.Height, GraphicsDevice.Viewport.Height);
                myJelly.img = this.Content.Load<Texture2D>("metalPanel");
                int x = rnd.Next(0, GraphicsDevice.Viewport.Width - myJelly.img.Width);
                int y = rnd.Next(0, GraphicsDevice.Viewport.Height- myJelly.img.Height);
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
            //bgPosition.X -= 5;
            //if (bgPosition.X <= 0-imgBackground.Width) bgPosition.X = 0;
            background0.update();
            background1.update();
            background2.update();
            background3.update();
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

                if (item.position.X + item.img.Width >= GraphicsDevice.Viewport.Width)
                {
                    item.position.X = GraphicsDevice.Viewport.Width - item.img.Width;
                    item.speedX = -item.speedX;
                    item.slimSpeed = item.speedX;
                }
                if (item.position.X <= 0)
                {
                    item.position.X = 0;
                    item.speedX = -item.speedX;
                    item.slimSpeed = item.speedX;
                }
                if ((item.position.Y + item.img.Height >= GraphicsDevice.Viewport.Height) || (item.position.Y <= 0))
                {
                    item.speedY = -item.speedY;
                }
                item.scale += item.scaleSpeed;
                if (item.scale <= 0.5f || item.scale >= 1.0f) item.scaleSpeed = -item.scaleSpeed;
                //position.Y += speedY;
                if (bClick && !allreadyClick)
                {
                    if (newMouseState.X >= item.position.X &&
                        newMouseState.X <= item.position.X + item.img.Width &&
                        newMouseState.Y >= item.position.Y &&
                        newMouseState.Y <= item.position.Y + item.img.Height)
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

        private void AfficheBackground(Background pBackground)
        {
            _spriteBatch.Draw(pBackground.Image, pBackground.Position, null, Color.White);
            if (pBackground.Position.X < 0)
            {
                _spriteBatch.Draw(pBackground.Image, new Vector2(pBackground.Position.X + pBackground.Image.Width, 0), null, Color.White);
            }

        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            // TODO: Add your drawing code here
            //_spriteBatch.Draw(imgBackground, bgPosition, null, Color.White);
            //if (bgPosition.X < 0)
            //{
            //    _spriteBatch.Draw(imgBackground, new Vector2 (bgPosition.X+imgBackground.Width, 0), null, Color.White);

            //}
            AfficheBackground(background0);
            AfficheBackground(background1);
            AfficheBackground(background2);
            AfficheBackground(background3);
            SpriteEffects effect = SpriteEffects.None;
            foreach (Jelly item in jellyList)
            {
                if (item.speedX > 0)
                    effect = SpriteEffects.FlipHorizontally;
                // pour change l'origine de l'image au centre en bas : new Vector2(img.Width / 2, img.Height)
                //_spriteBatch.Draw(img, item.position, null, Color.White *0.5f, 0, new Vector2(img.Width / 2, img.Height), new Vector2(item.scale, item.scale), effect, 0);
                _spriteBatch.Draw(item.img, item.position, null, item.colorSlim, 0, new Vector2(0, 0), new Vector2(item.scale, item.scale), effect, 0);

            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}