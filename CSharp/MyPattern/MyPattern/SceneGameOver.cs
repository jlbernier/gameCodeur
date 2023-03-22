using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templates;

namespace MyPattern
{
    internal class SceneGameOver : Scene
    {
        public SceneGameOver(MainGame pGame) : base(pGame)
        {
            Debug.WriteLine("New SceneGameOver");
        }
        public override void Load()
        {
            Debug.WriteLine("SceneGameOver.Load");
            base.Load();
        }

        public override void UnLoad()
        {
            Debug.WriteLine("SceneGameOver.UnLoad");
            base.UnLoad();
        }
        public override void Update(GameTime gameTime)
        {
            Debug.WriteLine("SceneGameOver.Update");
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Debug.WriteLine("SceneGameOver.Draw");
             mainGame.spriteBatch.DrawString(AssetManager.MainFont,
                "This is the GameOver", new Vector2(1, 1), Color.White);
           base.Draw(gameTime);
        }


    }
}
