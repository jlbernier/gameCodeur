 using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public abstract class Overlay
    {
        public bool isActive { get; set; }
        protected Game mainGame;

        public Overlay(Game pGame)
        {
            mainGame = pGame;
        }

        public void Active()
        {
            isActive = !isActive;
        }

        public abstract void Update();

        public virtual void Draw(SpriteBatch pSpriteBatch, SpriteFont pFont)
        {
            if (isActive)
            {
                pSpriteBatch.DrawString(pFont, "OVERLAY ACTIVE", new Vector2(0, 0), Color.White);
            }
        }
    }
}
