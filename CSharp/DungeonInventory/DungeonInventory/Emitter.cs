using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Life;
        public float LifeStart;

        public Particle(float pLife)
        {
            LifeStart = pLife;
            Life = pLife;
        }
    }

    public class Emitter
    {
        Texture2D texParticle;
        List<Particle> lstParticle;

        public Emitter(Game pGame)
        {
            lstParticle = new List<Particle>();
            texParticle = pGame.Content.Load<Texture2D>("particle");
        }

        public void Start(int pX, int pY)
        {
            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                Particle myParticle = new Particle(rnd.Next(20*2, 40*2));
                myParticle.Position = new Vector2(pX - texParticle.Width/2, pY - texParticle.Height/2);
                myParticle.Position.X += rnd.Next(-5, 5);
                myParticle.Position.Y += rnd.Next(-5, 5);
                myParticle.Velocity = new Vector2(rnd.Next(-4, 4), rnd.Next(-4, 4));
                myParticle.Velocity *= 3;
                lstParticle.Add(myParticle);
            }
        }

        public void Update()
        {
            for (int i = lstParticle.Count-1; i >= 0; i--)
            {
                Particle p = lstParticle[i];
                p.Position.X = p.Position.X + p.Velocity.X/10;
                p.Position.Y = p.Position.Y + p.Velocity.Y/10;
                p.Life--;
                if (p.Life <= 0)
                {
                    lstParticle.Remove(p);
                }
            }
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            foreach (Particle p in lstParticle)
            {
                float alpha = p.Life / p.LifeStart;
                pSpriteBatch.Draw(texParticle, p.Position, Color.White * alpha);
            }
        }
    }
}
