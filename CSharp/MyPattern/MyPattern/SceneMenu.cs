using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templates;

namespace MyPattern
{
    internal class SceneMenu : Scene
    {
        private KeyboardState oldKeyboardState;
        private GamePadState oldGamePadState;
        private Button MyButton;
        private Song music;
        public SceneMenu(MainGame pGame) : base(pGame)
        {
            Debug.WriteLine("New SceneMenu");
        }
        public void onClickPlay(Button pSender)
        {
            mainGame.gameState.ChangeScene(GameState.SceneType.GamePlay);
        }
        public override void Load()
        {
            oldKeyboardState = Keyboard.GetState();
            Rectangle Screen = mainGame.Window.ClientBounds;
            music = mainGame.Content.Load<Song>("cool");
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;

            MyButton = new Button(mainGame.Content.Load<Texture2D>("button"));
            MyButton.Position = new Vector2(
                Screen.Width / 2 - MyButton.Texture.Width / 2,
                Screen.Height / 2 - MyButton.Texture.Height / 2);
            MyButton.onClick = onClickPlay;
            listActors.Add(MyButton);

            Debug.WriteLine("SceneMenu.Load");
            base.Load();
        }

        public override void UnLoad()
        {
            MediaPlayer.Stop();
            base.UnLoad();
        }
        public override void Update(GameTime gameTime)
        {
            MouseState newMouseState = Mouse.GetState();
            if (newMouseState.LeftButton == ButtonState.Pressed)
            {

            }
            KeyboardState newKeyboardState = Keyboard.GetState();
             if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                Debug.WriteLine("touche enter");
                mainGame.gameState.ChangeScene(GameState.SceneType.GamePlay);
            }

            oldKeyboardState = newKeyboardState;
            //Debug.WriteLine("SceneMenu.Update");
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.spriteBatch.DrawString(AssetManager.MainFont,
                "This is the menu", new Vector2(1,1), Color.White);
              base.Draw(gameTime);
        }


    }
}
