using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonInventory
{
    public class OverlayInventory : Overlay
    {
        private Vector2 _position;
        private Texture2D _textureBG;
        private Texture2D _textureCadre;
        private Texture2D _textureCadreEnlight;

        public OverlayInventory(Game pGame, Vector2 position) : base(pGame)
        {
            this._position = position;
            _textureBG = pGame.Content.Load<Texture2D>("inventoryBG");
            _textureCadre = pGame.Content.Load<Texture2D>("Icons/BorderTemplate");
            _textureCadreEnlight = pGame.Content.Load<Texture2D>("Icons/BorderTemplateE");
        }

        public override void Update()
        {
            
        }

        public override void Draw(SpriteBatch pSpriteBatch, SpriteFont pFont)
        {
            pSpriteBatch.Draw(_textureBG, _position, Color.White);
            base.Draw(pSpriteBatch, pFont);
        }
    }
}
