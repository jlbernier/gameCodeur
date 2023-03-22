﻿using Microsoft.Xna.Framework;
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
        protected List<IActor> listActors;
        protected Scene(MainGame pGame)
        {
            mainGame = pGame;
            listActors = new List<IActor>();
        }
        public virtual void Load()
        {

        }

        public virtual void UnLoad()
        {

        }
        public virtual void Update(GameTime gameTime)
        {
            foreach (IActor actor in listActors)
            {
                actor.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (IActor actor in listActors)
            {
                actor.Draw(mainGame.spriteBatch);

            }
        }
    }
}
