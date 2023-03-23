using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Atelier3DIso
{
    internal class TileMap
    {
        public int mapWidth { get; private set; }
        public int mapHeight { get; private set; }
        public int tileWidth2D { get; private set; }
        public int tileHeight2D { get; private set; }
        public int tileWidth3D { get; private set; }
        public int tileHeight3D { get; private set; }
        public int[,] _data;
        public TileMap()
        {

        }
        public void set2DTileSize(int pwidth, int pheight)
        {
            tileWidth2D = pwidth;
            tileHeight2D = pheight;
        }
        public void set3DTileSize(int pwidth, int pheight)
        {
            tileWidth3D = pwidth;
            tileHeight3D = pheight;
        }
        public void SetData(int[,] pArray)
        {
            Trace.WriteLine("Setdata: begin ======================================");
            _data = pArray;
            mapHeight = pArray.GetLength(0);
            mapWidth = pArray.GetLength(1);
            Trace.WriteLine("Height: " + mapHeight);
            Trace.WriteLine("Width: " + mapWidth);
            Trace.WriteLine("SetData: end =========================================");
        }
        public int GetID(int pline, int pcolumn)
        {
            if (pline >=0 && pline < mapHeight && pcolumn >= 0 && pcolumn < mapWidth)
            {
                return _data[pline, pcolumn];
            }
            return -1;
        }
        public Vector2 To3D(Vector2 pCoord2D)
        {
            Vector2 newCoord = new Vector2();
            newCoord.X = pCoord2D.X - pCoord2D.Y;
            newCoord.Y = (pCoord2D.X + pCoord2D.Y) / 2;
            return newCoord;
        }
      }
}
