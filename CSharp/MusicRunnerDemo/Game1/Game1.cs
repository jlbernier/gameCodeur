using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MusicRunnerDemo;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D imgBackground0;
        Texture2D imgBackground1;
        Texture2D imgHeroSheet;
        Background background0;
        Background background1;
        SpriteRunner sprHero;
        KeyboardState oldKBState;
        Vector2 ScreenSize;
        // Musics and Sounds
        Song musicCool;
        Song musicTechno;
        GCMusicManager MusicManager;
        int nMusicCool;
        int nMusicTechno;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 512 * 2;
            graphics.PreferredBackBufferHeight = 256 * 2;
            ScreenSize.X = graphics.PreferredBackBufferWidth / 2;
            ScreenSize.Y = graphics.PreferredBackBufferHeight / 2;
            graphics.ApplyChanges();
            MusicManager = new GCMusicManager();
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
            oldKBState = Keyboard.GetState();

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
            imgBackground0 = Content.Load<Texture2D>("forest");
            imgBackground1 = Content.Load<Texture2D>("volcano");
            imgHeroSheet = Content.Load<Texture2D>("herosheet");

            background0 = new Background(imgBackground0, -5);
            background1 = new Background(imgBackground1, -5);
            SoundEffect sndJump = Content.Load<SoundEffect>("sfx_movement_jump13");
            SoundEffect sndLanding = Content.Load<SoundEffect>("sfx_movement_jump13_landing");
            sprHero = new SpriteRunner(spriteBatch, imgHeroSheet);
            sprHero.SetSounds(sndJump, sndLanding);
            sprHero.AjouteAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 1f / 12f);
            sprHero.LanceAnimation("run");
            sprHero.x = ScreenSize.X / 4;
            sprHero.SetGroundPosition((int)ScreenSize.Y - 25);

            // Load musics and sounds
            musicCool = Content.Load<Song>("cool");
            musicTechno = Content.Load<Song>("techno");
            nMusicCool = MusicManager.AddMusic(musicCool);
            nMusicTechno = MusicManager.AddMusic(musicTechno);
            MusicManager.PlayMusic(nMusicCool);
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
            background0.Update();
            background1.Update();

            MusicManager.Update(gameTime);

            GCSprite.UpdateAll(gameTime);

            // Get a new keyboard state to get the player input or compare with the old one...
            KeyboardState newKBState = Keyboard.GetState();
            // Keyboard test for the jump
            if (newKBState.IsKeyDown(Keys.Space) && !oldKBState.IsKeyDown(Keys.Space))
            {
                Debug.WriteLine("Jump !");
                sprHero.Jump();
            }
            oldKBState = newKBState;
            // Keyboard test for moving the hero left and right
            if (newKBState.IsKeyDown(Keys.Left) && sprHero.x > 0)
            {
                sprHero.x -= 2;
            }
            if (newKBState.IsKeyDown(Keys.Right) && sprHero.x < ScreenSize.X)
            {
                sprHero.x += 2;
            }

            // Select the music depending of the hero position
            if (sprHero.x > ScreenSize.X / 2)
            {
                MusicManager.PlayMusic(nMusicTechno);
            }
            else
            {
                MusicManager.PlayMusic(nMusicCool);
            }

            base.Update(gameTime);
        }

        private void AfficheBackground(Background pBackground)
        {
            spriteBatch.Draw(pBackground.Image, pBackground.Position, null, Color.White);
            if (pBackground.Position.X < 0)
                spriteBatch.Draw(pBackground.Image, new Vector2(pBackground.Position.X + pBackground.Image.Width, 0), null, Color.White);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(2));

            // Draw the backgroud according to the hero position
            if (sprHero.x > ScreenSize.X / 2)
            {
                AfficheBackground(background1);
            }
            else
            {
                AfficheBackground(background0);
            }

            GCSprite.DrawAll(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}