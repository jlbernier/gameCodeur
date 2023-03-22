using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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

            base.Update(pGameTime);
        }
    }
}
