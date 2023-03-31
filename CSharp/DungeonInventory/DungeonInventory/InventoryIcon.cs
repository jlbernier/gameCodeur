using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonInventory
{
    public enum EPhase
    {
        began,
        move,
        ended,
        cancelled
    }
    public class DragEvent
    {
        public float X;
        public float Y;
        public float startX;
        public float startY;
        public EPhase phase;
    }

    public class InventoryIcon
    {
        public Vector2 Position;
        public Texture2D Texture;
        public int Quantity;
        public bool isDraggable;
        public bool isDragging;
        private Vector2 StartPosition;
        public InventoryIcon(Texture2D pTexture, Vector2 pPosition, int pQuantity, bool pbDraggable)
        {
            Position = pPosition;
            Texture = pTexture;
            Quantity = pQuantity;
            isDraggable = pbDraggable;
        }
        public Rectangle HandleRect
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }
        public Vector2 GetCenter()
        {
            return new Vector2(Position.X + Texture.Width / 2, Position.Y + Texture.Height / 2);
        }

        public void Touch(DragEvent pDragEvent)
        {
            if (!isDraggable)
                return;

            if (pDragEvent.phase == EPhase.began)
            {
                isDragging = true;
                StartPosition = Position;
            }
            else if (pDragEvent.phase == EPhase.move)
            {
                float x = pDragEvent.X - pDragEvent.startX + StartPosition.X;
                float y = pDragEvent.Y - pDragEvent.startY + StartPosition.Y;
                Position = new Vector2(x, y);
            }
            else if (pDragEvent.phase == EPhase.ended || pDragEvent.phase == EPhase.cancelled)
            {
                isDragging = false;
                if (pDragEvent.phase == EPhase.cancelled)
                {
                    Position = StartPosition;
                }
            }
        }  

        public void Draw(SpriteBatch pSpriteBatch, SpriteFont pFont)
        {
            if (Texture != null)
            {
                Color c;
                if (isDragging)
                    c = Color.White * .5f;
                else
                    c = Color.White;
                pSpriteBatch.Draw(Texture, Position, c);

                if (Quantity > 1)
                {
                    string sQte = Quantity.ToString();
                    Vector2 size = pFont.MeasureString(sQte);
                    float x = Position.X + Texture.Width - size.X - 2;
                    float y = Position.Y + Texture.Height - size.Y + 2;
                    pSpriteBatch.DrawString(pFont, sQte, new Vector2(x, y), Color.White);
                }
            }
        }
    }
}
