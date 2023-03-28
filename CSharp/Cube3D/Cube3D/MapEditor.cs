using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube3D
{
    internal class Tile
    {
        public int id;
        public Texture2D texture;
    }
    internal class MapEditor
    {
        private MainGame mainGame;
        public bool isActive {  get; set; } 
        private SpriteFont Font;
        private int[,] mapData;
        private List<Tile> lstTiles;
        private GridPicker TilePicker;
        private Vector2 TilepickerPosition;
        private Vector2 MappickerPosition;
        private GridPicker MapPicker;
        const int TILESIZE = 24;
        public MapEditor(MainGame pGame, int NBTiles, ref int[,] pMapData)
        {
            mainGame = pGame;
            mapData = pMapData;
            Font = mainGame.Content.Load<SpriteFont>("pixelfont");
            isActive = false;
            lstTiles = new List<Tile>();
            TilepickerPosition.X = 0;
            TilepickerPosition.Y = 20;
            MappickerPosition = new Vector2(0, TilepickerPosition.Y + TILESIZE + 5);
            TilePicker = new GridPicker(pGame, 1, NBTiles, TILESIZE, TILESIZE, 3, TilepickerPosition);
            MapPicker = new GridPicker(pGame, pMapData.GetLength(0), pMapData.GetLength(1), TILESIZE, TILESIZE, 3, MappickerPosition);
        }
        public void AddTile(int pID, Texture2D pTexture)
        {
            Tile myTile = new Tile()
            {
                id = pID,
                texture = pTexture,
            };
            lstTiles.Add(myTile);
            TilePicker.SetTexture(0, lstTiles.Count - 1, pTexture, pID);
        }
        public void UpdateGrid()
        {
            for (int l = 0; l < mapData.GetLength(0); l++)
            {
                for (int c = 0; c < mapData.GetLength(0); c++)
                {
                    
                }
            }
        }
        public void Active()
        {
            isActive = !isActive;
        }
        public void Draw(SpriteBatch pSpriteBatch)
        {
            if (isActive)
            {
                pSpriteBatch.DrawString(Font, "INTEGRATED MAP EDITOR", new Vector2(0, 0), Color.White);
                TilePicker.Draw(pSpriteBatch);
                MapPicker.Draw(pSpriteBatch);
            }
        }
    }
}
