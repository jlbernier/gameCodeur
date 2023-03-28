using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MapTools
{
    public delegate void onCellSelect(int pID, int pLine, int pColumn);
    internal class Cell
    {
        public bool selected;
        public GCRectangle RectCell;
        public GCRectangle RectSelection;
        public int ID;
        public Texture2D Texture;
        public Cell(Game pGame, int pPosX, int pPosY, int pW, int pH)
        {
            this.selected = false;
            this.RectCell = new GCRectangle(pGame, GCRectangle.Type.outline,
                                         pPosX,
                                         pPosY,
                                         pW,
                                         pH,
                                         Color.Black, 
                                         Color.White);
            this.RectSelection = new GCRectangle(pGame, GCRectangle.Type.outline,
                                           pPosX - 1,
                                           pPosY - 1,
                                           pW + 2,
                                           pH + 2,
                                           Color.Black, 
                                           Color.Red);
        }
        public void Select()
        {
            selected = !selected;
        }
        public void Draw(SpriteBatch pSpriteBatch)
        {
            RectCell.Draw(pSpriteBatch);
            if (selected)
            {
                RectSelection.Draw(pSpriteBatch);
            }

        }
    }
    internal class GridPicker
    {
        private Cell[,] gridCell;
        public Cell currentCell;
        public onCellSelect selectionChanged;
        MouseState oldMouseState;
        public GridPicker(Game pGame, int pLines, int pColumns, int pW, int pH, int pSpace, Vector2 pPosition)
        {
            gridCell = new Cell[pLines, pColumns];
            for (int l = 0; l < pLines; l++)
            {
                for (int c = 0; c < pColumns; c++)
                {
                    Cell myCell = new Cell(pGame,
                        (c * (pW + pSpace)) + (int)pPosition.X,
                        (l * (pW + pSpace)) + (int)pPosition.Y, 
                        pW, pH);
 
                    gridCell[l, c] = myCell;
                }
            }
            oldMouseState = Mouse.GetState();
        }
        public void SetTexture(int pLine, int pColumn, Texture2D pTexture, int pID)
        {
            // Debug.Assert uniquement en mode debug
            Debug.Assert(pLine < gridCell.GetLength(0) && pColumn < gridCell.GetLength(1) && pLine >= 0 && pColumn >= 0, "Values out of band for GridPicker.SetTexture");
            if (pLine <gridCell.GetLength(0) && pColumn < gridCell.GetLength(1) && pLine >=0 && pColumn >= 0)
            {
                gridCell[pLine, pColumn].Texture = pTexture;
                gridCell[pLine, pColumn].ID = pID;
            }
            else
            {
                // on pourrait faire un fichier log 
                Debug.WriteLine("Values out of band for GridPicker.SetTexture");
            }
        }
        private void UnselectAll()
        {
            for (int l = 0; l < gridCell.GetLength(0); l++)
            {
                for (int c = 0; c < gridCell.GetLength(1); c++)
                {
                    Cell myCell = gridCell[l, c];
                    if (myCell.selected)
                    {
                        myCell.Select();
                    }
                }
            }

        }
        public void Update()
        {
            MouseState newMouseState = Mouse.GetState();
            if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                for (int l = 0; l < gridCell.GetLength(0); l++)
                {
                    for (int c = 0; c < gridCell.GetLength(1); c++)
                    {
                        Cell myCell = gridCell[l, c];
                        if (myCell.RectCell.Rect.Contains(newMouseState.Position))
                        {
                            UnselectAll();
                            myCell.Select();
                            selectionChanged.Invoke(myCell.ID, l, c);
                            currentCell = myCell;
                        }
                    }
                }
            }
            oldMouseState = newMouseState; 
        }
        public void Draw(SpriteBatch pSpriteBatch)
        {
            for (int l = 0; l < gridCell.GetLength(0); l++)
            {
                for (int c = 0; c < gridCell.GetLength(1); c++)
                {
                    Cell myCell = gridCell[l, c];
                    myCell.Draw(pSpriteBatch);
                    if (myCell.Texture != null)
                    {
                        pSpriteBatch.Draw(myCell.Texture, new Vector2(myCell.RectCell.Rect.X, myCell.RectCell.Rect.Y), Color.White);
                    }
                }
            }
        }
    }
}
