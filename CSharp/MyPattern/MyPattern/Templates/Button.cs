using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templates
{
    public delegate void OnClick(Button pSender);
    public class Button : Sprite
    {
        public bool isHover {  get; private set; }
        private MouseState oldMouseState;
        public OnClick onClick { get; set; }
        public Button(Texture2D pTexture) : base(pTexture)
        {
        }
        public override void Update(GameTime pGameTime)
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
            base.Update(pGameTime);
        }
    }
}
