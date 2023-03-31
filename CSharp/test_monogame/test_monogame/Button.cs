using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_monogame
{
    public delegate void OnClick(Button pSender);
    public class Button
    {
        public Vector2 Position { get; set; }
        public Rectangle BoundingBox { get; set; }
        public bool isHover { get; private set; }
        private MouseState oldMouseState;
        public OnClick onClick { get; set; }
        public Texture2D Texture { get; }

        public Button(Texture2D pTexture, int x, int y)
        {
            Texture = pTexture;
            Position = new Vector2(x, y);
        }

        public void Update(GameTime pGameTime)
        {
            MouseState newMouseState = Mouse.GetState();
            Point MousePos = newMouseState.Position;

            if (BoundingBox.Contains(MousePos))
            {
                if (!isHover)
                {
                    isHover = true;
                    Debug.WriteLine("The button is now hover !");
                }
            }
            else
            {
                if (isHover)
                {
                    Debug.WriteLine("The button is no more hover !");
                }
                isHover = false;
            }
            if (isHover)
            {
                if ((newMouseState.LeftButton == ButtonState.Pressed) &&
                    (oldMouseState.LeftButton == ButtonState.Released))
                {
                    Debug.WriteLine("Button is click!");
                    if (onClick != null)
                    {
                        onClick(this);
                    }
                }
            }
            oldMouseState = newMouseState;
            BoundingBox = new Rectangle(
              (int)Position.X,
              (int)Position.Y,
              Texture.Width,
              Texture.Height);
        }
        public virtual void Draw(GameTime pGameTime, SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(Texture, Position, null, Color.White);
        }
    }
}
