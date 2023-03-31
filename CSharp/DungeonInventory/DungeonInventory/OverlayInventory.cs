using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dungeon
{
    public class OverlayInventory : Overlay
    {
        public delegate void onItemDrop(Item pItem);

        InventoryManager inventoryManager;

        public onItemDrop DropDelegate;

        private Vector2 Position;

        private MouseState oldMouseState;
        Vector2 mouseStart;

        private Texture2D textureCadre;
        private Texture2D textureCadreEnlight;
        private Texture2D textureBG;
        private List<InventoryIcon> lstIcons;
        private List<Rectangle> lstSlots;
        private InventoryIcon dragIcon;
        private int slotEnlight;
        private int slotFrom;

        private SoundEffect sndSlot;

        private const int LINES = 7;
        private const int COLS = 7;
        private const int WIDTHICON = 43;
        private const int HEIGHTICON = 43;

        public OverlayInventory(Game pGame, ref InventoryManager pInventoryManager, int pX, int pY) : base(pGame)
        {
            Position = new Vector2(pX, pY);
            dragIcon = null;

            inventoryManager = pInventoryManager;

            textureCadre = pGame.Content.Load<Texture2D>("Icons/BorderTemplate");
            textureCadreEnlight = pGame.Content.Load<Texture2D>("Icons/BorderTemplateE");
            textureBG = pGame.Content.Load<Texture2D>("InventoryBG");

            sndSlot = pGame.Content.Load<SoundEffect>("sounds/inventory_slot");

            lstIcons = new List<InventoryIcon>();
            lstSlots = new List<Rectangle>();
        }

        public void PopulateIcons()
        {
            lstIcons.Clear();
            lstSlots.Clear();
            slotEnlight = -1;
            slotFrom = -1;
            float x = Position.X + 11f;
            float y = Position.Y + 12f;
            int col = 1;
            int line = 1;
            int slot = 0;
            for (int i = 0; i < InventoryManager.MAXITEMS; i++)
            {
                float xItem, yItem;
                xItem = x + (col - 1) * (textureCadre.Width + 1);
                yItem = y + (line - 1) * (textureCadre.Height + 1);
                lstSlots.Add(new Rectangle((int)xItem, (int)yItem, WIDTHICON, HEIGHTICON));
                xItem++;
                yItem++;
                // Icon
                Item item = inventoryManager.GetItemAt(slot);
                if (item != null)
                {
                    Texture2D texture = ItemTextures.Textures[item.ID];
                    InventoryIcon icon = new InventoryIcon(texture, new Vector2(xItem, yItem), item.Quantity, true);
                    lstIcons.Add(icon);
                }
                // Next!
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
                // Which item is clicked?
                foreach (InventoryIcon icon in lstIcons)
                {
                    if (icon.HandleRect.Contains(newMouseState.Position) && icon.isDraggable)
                    {
                        dragIcon = icon;
                        icon.isDragging = true;
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
                    mouseStart = new Vector2(newMouseState.X, newMouseState.Y);
                    // CLIC!
                    dragIcon.Touch(new DragEvent {
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
                // MOVE
                dragIcon.Touch(new DragEvent
                {
                    phase = EPhase.move,
                    startX = mouseStart.X,
                    startY = mouseStart.Y,
                    X = newMouseState.X,
                    Y = newMouseState.Y
                });
                for (int i = 0; i < COLS*LINES; i++)
                {
                    if (lstSlots[i].Contains(dragIcon.Position + new Vector2(dragIcon.Texture.Width/2, dragIcon.Texture.Height/2)))
                    {
                        slotEnlight = i;
                    }
                }
            }
            else if (newMouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed && dragIcon != null)
            {
                slotEnlight = -1;
                // From where to where?
                int slotTo = -1;
                for (int i = 0; i < COLS * LINES; i++)
                {
                    if (lstSlots[i].Contains(dragIcon.GetCenter()))
                    {
                        sndSlot.Play();
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
                if (itFrom != null && slotTo != -1 && (slotFrom != slotTo))
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
                    else// if (itFrom.Quantity > 0)
                    {
                        itFrom.InventorySlot = slotTo;
                    }
                    if (itFrom.Quantity == 0)
                    {
                        inventoryManager.RemoveItem(slotFrom);
                    }
                    PopulateIcons();
                }
                else
                {
                    dragIcon.Touch(new DragEvent
                    {
                        phase = EPhase.cancelled,
                        startX = mouseStart.X,
                        startY = mouseStart.Y,
                        X = newMouseState.X,
                        Y = newMouseState.Y
                    });
                    // Delegate!
                    if (slotTo == -1 && DropDelegate != null)
                        DropDelegate(itFrom);
                }
                dragIcon = null;
            }

            oldMouseState = Mouse.GetState();
        }
        
        public override void Draw(SpriteBatch pSpriteBatch, SpriteFont pFont)
        {
            if (!isActive) return;

            float x = Position.X + 11f;
            float y = Position.Y + 12f;
            pSpriteBatch.Draw(textureBG, Position, Color.White);
            int line = 1;
            int col = 1;
            int slot = 0;
            for (int i = 0; i < InventoryManager.MAXITEMS; i++)
            {
                float xItem, yItem;
                xItem = x + (col - 1) * (textureCadre.Width + 1);
                yItem = y + (line - 1) * (textureCadre.Height + 1);
                // BG
                if (slot == slotEnlight)
                    pSpriteBatch.Draw(textureCadreEnlight, new Vector2(xItem, yItem), Color.White);
                else
                    pSpriteBatch.Draw(textureCadre, new Vector2(xItem, yItem), Color.White);
                // Next slot
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
                if (icon != dragIcon)
                {
                    icon.Draw(pSpriteBatch, pFont);
                }
            }
            if (dragIcon != null)
            {
                dragIcon.Draw(pSpriteBatch, pFont);
            }
        }
    }
}
