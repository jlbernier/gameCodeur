using Gamecodeur;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCodeur
{
    public delegate void onCellSelect(Cell cell, int pLine, int pColumn);

    public class Cell
    {
        public bool Selected;
        public GCRectangle RectCell;
        public GCRectangle RectSelection;
        public int ID;
        public Texture2D Texture;

        public Cell(Game pGame, int pPosX, int pPosY, int pW, int pH)
        {
            Selected = false;
            RectCell = new GCRectangle(pGame, GCRectangle.Type.outline, pPosX, pPosY, pW, pH, Color.Black, Color.White);
            RectSelection = new GCRectangle(pGame, GCRectangle.Type.outline, pPosX - 1, pPosY - 1, pW + 2, pH + 2, Color.Black, Color.Red);
        }

        public void Select()
        {
            Selected = !Selected;
            if (Selected)
            {
                Debug.WriteLine("Cell {0} selection changed", ID);
            }
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            RectCell.Draw(pSpriteBatch);
            if (Selected)
            {
                RectSelection.Draw(pSpriteBatch);
            }
        }
    }

    class GridPicker
    {
        private Cell[,] gridCells;
        public Cell CurrentCell;

        private MouseState oldMouseState;
        public onCellSelect SelectionChanged;

        public GridPicker(Game pGame, int pLines, int pColumns, int pW, int pH, int space, Vector2 pPosition)
        {
            CurrentCell = null;
            gridCells = new Cell[pLines, pColumns];
            for (int l = 0; l < pLines; l++)
            {
                for (int c = 0; c < pColumns; c++)
                {
                    Cell myCell = new Cell(pGame, (c * (pW + space)) + (int)pPosition.X, (l * (pH + space)) + (int)pPosition.Y, pW, pH);
                    gridCells[l, c] = myCell;
                }
            }

            oldMouseState = Mouse.GetState();
        }

        public void SetTexture(int pLine, int pColumn, Texture2D pTexture, int pID)
        {
            if (pLine < gridCells.GetLength(0) && pColumn < gridCells.GetLength(1))
            {
                gridCells[pLine, pColumn].Texture = pTexture;
                gridCells[pLine, pColumn].ID = pID;
            }
        }

        public void UnselectAll()
        {
            CurrentCell = null;
            for (int l = 0; l < gridCells.GetLength(0); l++)
            {
                for (int c = 0; c < gridCells.GetLength(1); c++)
                {
                    Cell myCell = gridCells[l, c];
                    if (myCell.Selected)
                        myCell.Select();
                }
            }
        }

        public void Update()
        {
            MouseState newMouseState = Mouse.GetState();

            if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                Debug.WriteLine("Mouse pressed !");
                // Test all the cells
                for (int l = 0; l < gridCells.GetLength(0); l++)
                {
                    for (int c = 0; c < gridCells.GetLength(1); c++)
                    {
                        Cell myCell = gridCells[l, c];
                        if (myCell.RectCell.InsideRect.Contains(newMouseState.Position))
                        {
                            UnselectAll();
                            myCell.Select();
                            CurrentCell = myCell;
                            SelectionChanged.Invoke(myCell, l, c);
                        }
                    }
                }
            }

            oldMouseState = newMouseState;
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            for (int l = 0; l < gridCells.GetLength(0); l++)
            {
                for (int c = 0; c < gridCells.GetLength(1); c++)
                {
                    Cell myCell = gridCells[l, c];
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
