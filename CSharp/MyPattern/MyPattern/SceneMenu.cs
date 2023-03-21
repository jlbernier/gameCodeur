using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        public SceneMenu(MainGame pGame) : base(pGame)
        {
            Debug.WriteLine("New SceneMenu");
        }
        public override void Load()
        {
            oldKeyboardState = Keyboard.GetState();
            Debug.WriteLine("SceneMenu.Load");
            base.Load();
        }

        public override void UnLoad()
        {
            Debug.WriteLine("SceneMenu.UnLoad");
            base.UnLoad();
        }
        public override void Update(GameTime gameTime)
        {
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
            //Debug.WriteLine("SceneMenu.Draw");
            mainGame.spriteBatch.Begin();
            mainGame.spriteBatch.DrawString(AssetManager.MainFont,
                "This is the menu", new Vector2(1,1), Color.White);
            mainGame.spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
