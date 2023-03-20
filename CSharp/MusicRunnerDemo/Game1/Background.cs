using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRunnerDemo
{
    class Background
    {
        private Vector2 position;
        private Texture2D image;
        private float speed;
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }
        public Texture2D Image
        {
            get
            {
                return image;
            }
        }

        public Background(Texture2D pTexture, float pSpeed)
        {
            image = pTexture;
            speed = pSpeed;
            position = new Vector2(0, 0);
        }

        public void Update()
        {
            position.X += speed;
            if (position.X <= 0 - image.Width)
                position.X += image.Width;
        }

    }

}
