using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templates
{
    public class Sprite : IActor
    {
        //IActor
        public Vector2 Position { get; set; }
        public Rectangle BoundingBox { get; set; }
        public float velovityX;
        public float velovityY;
        //Sprite
        public Texture2D Texture { get; }
        public bool ToRemove { get; set; }

        public Sprite(Texture2D pTexture) 
        {
            Texture = pTexture;
            ToRemove = false;
        }
        public void Move(float pX, float pY)
        {
            Position = new Vector2(Position.X + pX,Position.Y + pY);
        }
        public virtual void TouchBy(IActor pBy)
        {

        }


        public virtual void Update(GameTime pGameTime)
        {
            Move(velovityX, velovityY);
            BoundingBox = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                Texture.Width,
                Texture.Height);
        }
        public virtual void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(Texture, Position, null, Color.White);
        }
    }
}
