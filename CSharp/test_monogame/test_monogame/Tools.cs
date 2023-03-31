using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace test_monogame
{
    internal class Tools
    {
        protected MainGame mainGame;
        public Tools(MainGame mainGame) : base() { this.mainGame = mainGame;}
        
        public void Init()
        {
            mainGame.IsMouseVisible = true;
        }
    }
}
