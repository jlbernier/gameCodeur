using Microsoft.Xna.Framework;
using MyPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templates
{
    abstract public class Scene
    {
        protected MainGame mainGame;
        protected Scene(MainGame pGaim) 
        {
            mainGame = pGaim;
        }
        public virtual void Load()
        {

        }

         public virtual void UnLoad()
        {

        }
        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime)
        {

        }
    }
}
