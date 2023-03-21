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
    internal class SceneGamePlay : Scene
    {
        private KeyboardState oldKeyboardState;
        public SceneGamePlay(MainGame pGame) : base(pGame)
        {
            Debug.WriteLine("New SceneGamePlay");
        }
        public override void Load()
        {
            oldKeyboardState = Keyboard.GetState();
            //Debug.WriteLine("SceneGamePlay.Load");
            base.Load();
        }

        public override void UnLoad()
        {
            Debug.WriteLine("SceneGamePlay.UnLoad");
            base.UnLoad();
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState newKeyboardState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                Debug.WriteLine("touche left control");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !oldKeyboardState.IsKeyDown(Keys.Space))
            {
                Debug.WriteLine("touche space");
            }

            //Debug.WriteLine("SceneGamePlay.Update");
            oldKeyboardState = newKeyboardState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //Debug.WriteLine("SceneGamePlay.Draw");
            mainGame.spriteBatch.Begin();
            mainGame.spriteBatch.DrawString(AssetManager.MainFont,
                "This is the GamePlay", new Vector2(1, 1), Color.White);
            mainGame.spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}