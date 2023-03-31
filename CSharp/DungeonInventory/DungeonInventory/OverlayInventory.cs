using DungeonInventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonInventory
{
    public class OverlayInventory : Overlay
    {
        private InventoryManager inventoryManager;
        private Vector2 Position;

        private Texture2D textureBG;
        private Texture2D textureCadre;
        private Texture2D textureCadreEnlight;

        private List<InventoryIcon> lstIcons;
        private List<Rectangle> lstSlots;
        private int slotEnlight;
        private int slotFrom;

        public const int COLS = 7;
        public const int LINES = 7;

        private MouseState oldMouseState;
        private Vector2 mouseStart;
        InventoryIcon dragIcon;

        public const int WIDTHICON = 43;
        public const int HEIGHTICON = 43;

        public OverlayInventory(Game pGame, InventoryManager pInventory, int pX, int pY) : base(pGame)
        {
            inventoryManager = pInventory;
            Position = new Vector2(pX, pY);

            textureBG = pGame.Content.Load<Texture2D>("InventoryBG");
            textureCadre = pGame.Content.Load<Texture2D>("Icons/BorderTemplate");
            textureCadreEnlight = pGame.Content.Load<Texture2D>("Icons/BorderTemplateE");

            lstIcons = new List<InventoryIcon>();
            lstSlots = new List<Rectangle>();
        }

        public void Populate()
        {
            lstIcons.Clear();
            lstSlots.Clear();
            float x = Position.X + 11f;
            float y = Position.Y + 12f;
            slotEnlight = -1;
            slotFrom = -1;
            int col = 1;
            int line = 1;
            int slot = 0;
            for (int i = 0; i < InventoryManager.MAXITEM; i++)
            {
                float xIcon, yIcon;
                xIcon = x + (col - 1) * (textureCadre.Width + 1);
                yIcon = y + (line - 1) * (textureCadre.Height + 1);
                lstSlots.Add(new Rectangle((int)xIcon, (int)yIcon, WIDTHICON, HEIGHTICON));

                xIcon++;
                yIcon++;

                Item item = inventoryManager.GetItemAt(slot);
                if (item != null)
                {
                    Texture2D texture = ItemTextures.Textures[item.ID];
                    InventoryIcon icon = new InventoryIcon(texture, new Vector2(xIcon, yIcon), item.Quantity, true);
                    lstIcons.Add(icon);
                }

                slot++;
                col++;
                if (col > COLS)
                {
                    line++;
                    col = 1;
                    if (line > LINES)
                    {
                        break;
                    }
                }
            }
        }

        public override void Update()
        {
            MouseState newMouseState = Mouse.GetState();

            if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                foreach (InventoryIcon icon in lstIcons)
                {
                    if (icon.HandleRect.Contains(newMouseState.Position) && icon.isDraggable)
                    {
                        icon.isDragging = true;
                        dragIcon = icon;
                        break;
                    }
                }
                if (dragIcon != null)
                {
                    for (int i = 0; i < COLS * LINES; i++)
                    {
                        if (lstSlots[i].Contains(newMouseState.Position))
                        {
                            slotFrom = i;
                        }
                    }
                    mouseStart = newMouseState.Position.ToVector2();
                    dragIcon.Touch(new DragEvent
                    {
                        phase = EPhase.began,
                        startX = mouseStart.X,
                        startY = mouseStart.Y,
                        X = newMouseState.X,
                        Y = newMouseState.Y
                    });
                }
            }
            else if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Pressed && dragIcon != null)
            {
                dragIcon.Touch(new DragEvent
                {
                    phase = EPhase.move,
                    startX = mouseStart.X,
                    startY = mouseStart.Y,
                    X = newMouseState.X,
                    Y = newMouseState.Y
                });
                for (int i = 0; i < COLS * LINES; i++)
                {
                    if (lstSlots[i].Contains(dragIcon.GetCenter()))
                    {
                        slotEnlight = i;
                    }
                }
            }
            else if (newMouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed && dragIcon != null)
            {
                slotEnlight = -1;
                int slotTo = -1;
                for (int i = 0; i < COLS * LINES; i++)
                {
                    if (lstSlots[i].Contains(dragIcon.GetCenter()))
                    {
                        slotTo = i;
                        dragIcon.Touch(new DragEvent
                        {
                            phase = EPhase.ended,
                            startX = mouseStart.X,
                            startY = mouseStart.Y,
                            X = newMouseState.X,
                            Y = newMouseState.Y
                        });
                    }
                }
                // Deal!
                Item itFrom = inventoryManager.GetItemAt(slotFrom);
                Item itTo = inventoryManager.GetItemAt(slotTo);
                if (itFrom != null && slotTo != -1 && (slotTo != slotFrom))
                {
                    if (itTo != null)
                    {
                        if (itFrom.Collectible && itTo.Collectible && (itFrom.ID == itTo.ID))
                        {
                            itTo.Quantity += itFrom.Quantity;
                            if (itTo.Quantity > itTo.MaxPerSlot)
                            {
                                int dif = itTo.Quantity - itTo.MaxPerSlot;
                                itTo.Quantity = itTo.MaxPerSlot;
                                itFrom.Quantity = dif;
                            }
                            else
                            {
                                itFrom.Quantity = 0;
                            }
                        }
                        else
                        {
                            itTo.InventorySlot = itFrom.InventorySlot;
                            itFrom.InventorySlot = slotTo;
                        }
                    }
                    else
                    {
                        itFrom.InventorySlot = slotTo;
                    }
                    if (itFrom.Quantity == 0)
                    {
                        inventoryManager.RemoteItem(slotFrom);
                    }
                }
                dragIcon.Touch(new DragEvent
                {
                    phase = EPhase.cancelled,
                    startX = mouseStart.X,
                    startY = mouseStart.Y,
                    X = newMouseState.X,
                    Y = newMouseState.Y
                });
                Populate();
                dragIcon = null;
            }
            oldMouseState = newMouseState;
        }

        public override void Draw(SpriteBatch pSpriteBatch, SpriteFont pFont)
        {
            pSpriteBatch.Draw(textureBG, Position, Color.White);

            float x = Position.X + 11f;
            float y = Position.Y + 12f;

            int line = 1;
            int col = 1;
            int slot = 0;

            for (int i = 0; i < (7 * 7); i++)
            {
                float xItem, yItem;
                xItem = x + (col - 1) * (textureCadre.Width + 1);
                yItem = y + (line - 1) * (textureCadre.Height + 1);
                if (slot == slotEnlight)
                    pSpriteBatch.Draw(textureCadreEnlight, new Vector2(xItem, yItem), Color.White);
                else
                    pSpriteBatch.Draw(textureCadre, new Vector2(xItem, yItem), Color.White);
                //pSpriteBatch.DrawString(pFont, slot.ToString(), new Vector2(xItem, yItem), Color.White);

                slot++;

                col++;
                if (col > COLS)
                {
                    line++;
                    col = 1;
                    if (line > LINES)
                    {
                        break;
                    }
                }
            }

            foreach (InventoryIcon icon in lstIcons)
            {
                icon.Draw(pSpriteBatch, pFont);
            }

            base.Draw(pSpriteBatch, pFont);
        }
    }
}
