using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MapTools
{
    [DataContract]
    internal class MapJSON
    {
        public void ConvertForWrite(int[,] pMapData)
        {
            MapData = new int[pMapData.GetLength(0)][];
            for (int l = 0; l < pMapData.GetLength(0); l++)
            {
                MapData[l] = new int[pMapData.GetLength(1)];
                for (int c = 0; c < pMapData.GetLength(1); c++)
                {
                    MapData[l][c] = pMapData[l, c];
                }
            }
        }

        public void ConvertForRead(ref int[,] pMapData)
        {
            int nbLines = MapData.Length;
            int nbColumns = MapData[0].Length;
            for (int l = 0; l < nbLines; l++)
            {
                for (int c = 0; c < nbColumns; c++)
                {
                    pMapData[l, c] = MapData[l][c];
                }
            }
        }

        [DataMember]
        public int[][] MapData;

        //[DataMember]
        //public string LevelName;
    }
}
