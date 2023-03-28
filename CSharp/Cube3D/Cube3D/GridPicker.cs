using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cube3D
{
    internal class Cell
    {
        public bool selected;
        public GCRectangle RectCell;
        public int ID;
        public Texture2D Texture;
        public Cell()
        {
            this.selected = false;
        }
        public void Select()
        {
            selected = !selected;
        }
    }
    internal class GridPicker
    {
        private Cell[,] gridCell;
        public GridPicker(MainGame pGame, int pLines, int pColumns, int pW, int pH, int pSpace, Vector2 pPosition)
        {
            gridCell = new Cell[pLines, pColumns];
            for (int l = 0; l < pLines; l++)
            {
                for (int c = 0; c < pColumns; c++)
                {
                    Cell myCell = new Cell();
                    myCell.RectCell = new GCRectangle(pGame, GCRectangle.Type.outline,
                        (c * (pW + pSpace)) + (int)pPosition.X,
                        (l * (pH + pSpace)) + (int)pPosition.Y,
                        pW, pH, Color.Black, Color.White);
                    gridCell[l, c] = myCell;
                }
            }
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
        public void Draw(SpriteBatch pSpriteBatch)
        {
            for (int l = 0; l < gridCell.GetLength(0); l++)
            {
                for (int c = 0; c < gridCell.GetLength(1); c++)
                {
                    Cell myCell = gridCell[l, c];
                    myCell.RectCell.Draw(pSpriteBatch);
                    if (myCell.Texture != null)
                    {
                        pSpriteBatch.Draw(myCell.Texture, new Vector2(myCell.RectCell.Rect.X, myCell.RectCell.Rect.Y), Color.White);
                    }
                }
            }
        }
    }
}
