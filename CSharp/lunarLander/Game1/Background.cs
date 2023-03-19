using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListImages
{
    internal class Background
    {
        private Vector2 position;
        public Vector2 Position
        { get { return position; } }
        private Texture2D image;
        public Texture2D Image
        { get { return image; } }
        private float speed;

        public Background(Texture2D pTexture, float pSpeed) 
        {
            this.image = pTexture;
            this.speed = pSpeed;
            this.position = new Vector2(0, 0);
        }
        public void update()
        {
            position.X += speed;
            if (position.X <= 0 - image.Width) position.X = 0;
        }
    }
}
