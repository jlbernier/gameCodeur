using MapTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonInventory
{
    class Tile
    {
        public int id;
        public Texture2D texture;
    }

    class OverlayMapEditor : Overlay
    {
        private int[,] mapData;
        private List<Tile> lstTiles;

        private GridPicker TilePicker;
        private Vector2 TilePickerPosition;

        private GridPicker MapPicker;
        private Vector2 MapPickerPosition;

        public OverlayMapEditor(Game pGame, int pNbTiles, ref int[,] pmapData) : base(pGame)
        {
            mapData = pmapData;

            isActive = false;
            lstTiles = new List<Tile>();

            TilePickerPosition.X = 0;
            TilePickerPosition.Y = 20;

            MapPickerPosition.X = 0;
            MapPickerPosition.Y = TilePickerPosition.Y + 24 + 5;

            TilePicker = new GridPicker(pGame, 1, pNbTiles, 24, 24, 3, TilePickerPosition);
            MapPicker = new GridPicker(pGame, pmapData.GetLength(0), pmapData.GetLength(1), 24, 24, 3, MapPickerPosition);

            TilePicker.SelectionChanged = onTileSelect;
            MapPicker.SelectionChanged = onMapSelect;

        }

        public void AddTile(int pID, Texture2D pTexture)
        {
            Tile myTile = new Tile
            {
                id = pID,
                texture = pTexture
            };
            lstTiles.Add(myTile);

            TilePicker.SetTexture(0, lstTiles.Count-1, pTexture, pID);
        }

        public void UpdateGrid()
        {
            for (int l = 0; l < mapData.GetLength(0); l++)
            {
                for (int c = 0; c < mapData.GetLength(1); c++)
                {
                    MapPicker.SetTexture(l, c, lstTiles[mapData[l, c]].texture, mapData[l, c]);
                }
            }
        }

        public void onTileSelect(Cell cell, int pLine, int pColumn)
        {
            Debug.WriteLine("Delegate called : Tile!");
        }

        public void onMapSelect(Cell cell, int pLine, int pColumn)
        {
            Debug.WriteLine("Delegate called : Map!");

            if (TilePicker.CurrentCell != null)
            {
                mapData[pLine, pColumn] = TilePicker.CurrentCell.ID;
                UpdateGrid();
            }
        }

        public override void Update()
        {
            TilePicker.Update();
            MapPicker.Update();
        }

        public override void Draw(SpriteBatch pSpriteBatch, SpriteFont pFont)
        {
            if (isActive)
            {
                pSpriteBatch.DrawString(pFont, "INTEGRATED MAP EDITOR", new Vector2(0, 0), Color.White);

                // Draw the Tile Picker & the Map Editor
                TilePicker.Draw(pSpriteBatch);
                MapPicker.Draw(pSpriteBatch);
            }
        }
    }
}
