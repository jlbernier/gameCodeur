using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Mirror
{
    public class Jelly
    {
        public Vector2 position;
        public int vitesse;
        public float scale;
        public float scaleVitesse;
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D img;
        Vector2 position;
        int vitesse;
        float scale;
        float scaleVitesse;
        List<Jelly> lstJelly;
        Random rnd;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            lstJelly = new List<Jelly>();
            rnd = new Random();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            position = new Vector2(100, 100);
            vitesse = 5;
            scale = 1.0f;
            scaleVitesse = -0.01f;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            img = this.Content.Load<Texture2D>("slimePurple2");

            for (int i=1; i<=20; i++)
            {
                Jelly myJelly = new Jelly();
                int y = rnd.Next(img.Height, GraphicsDevice.Viewport.Height);
                int x = rnd.Next(0, GraphicsDevice.Viewport.Width);
                myJelly.position = new Vector2(x, y);
                myJelly.vitesse = rnd.Next(1, 5);
                lstJelly.Add(myJelly);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //position.X += vitesse;

            foreach (Jelly item in lstJelly)
            {
                item.position.X += item.vitesse;

                if (item.position.X > GraphicsDevice.Viewport.Width)
                {
                    item.position.X = GraphicsDevice.Viewport.Width;
                    item.vitesse = 0 - item.vitesse;
                }
                if (item.position.X < 0)
                {
                    item.position.X = 0;
                    item.vitesse = 0 - item.vitesse;
                }
            }

            //if (position.X + img.Width >= GraphicsDevice.Viewport.Width)
            //{
            //    vitesse = 0 - vitesse;
            //}

            //if (position.X <= 0)
            //{
            //    vitesse = 0 - vitesse;
            //}

            //scale += scaleVitesse;
            //if (scale <= 0.5f)
            //{
            //    scale = 0.5f;
            //    scaleVitesse = 0 - scaleVitesse;
            //}
            //if (scale > 1.0f)
            //{
            //    scale = 1.0f;
            //    scaleVitesse = 0 - scaleVitesse;
            //}

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //spriteBatch.Draw(img, position);
            SpriteEffects effect;

            foreach (Jelly item in lstJelly)
            {
                effect = SpriteEffects.None;
                if (item.vitesse > 0)
                    effect = SpriteEffects.FlipHorizontally;
                spriteBatch.Draw(img, item.position, null, Color.White, 0, new Vector2(img.Width / 2, img.Height), new Vector2(scale, scale), effect, 0);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
