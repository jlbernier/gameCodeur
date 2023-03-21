using MyPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templates
{
     public class GameState
    {
        public enum SceneType
        {
            Menu,
            GamePlay,
            GameOver
        }
        protected MainGame mainGame;
        public Scene CurrentScene { get; set; }
        public GameState(MainGame pGame)
        {
            this.mainGame = pGame;
        }
        public void ChangeScene(SceneType psceneType)
        {
            if (CurrentScene != null)
            {
                CurrentScene.UnLoad();
                CurrentScene = null;
            }
            switch (psceneType)
            {
                case SceneType.Menu:
                    CurrentScene = new SceneMenu(mainGame);
                    break; 
                case SceneType.GamePlay:
                    CurrentScene = new SceneGamePlay(mainGame);
                    break; 
                case SceneType.GameOver:
                    CurrentScene = new SceneGameOver(mainGame);
                    break;
                default:
                    break;
            }
            CurrentScene.Load();
        }
    }
}
