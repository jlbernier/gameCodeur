using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MapTools
{
    [DataContract]
    class MapJSON
    {
        public void ConvertForWrite(int[,] pmapData)
        {
            rawMapData = new int[pmapData.GetLength(0)][];
            for (int l = 0; l < pmapData.GetLength(0); l++)
            {
                rawMapData[l] = new int[pmapData.GetLength(1)];
                for (int c = 0; c < pmapData.GetLength(1); c++)
                {
                    rawMapData[l][c] = pmapData[l, c];
                }
            }
        }

        public void ConvertForRead(ref int[,] pmapData)
        {
            int nbLines = rawMapData.Length;
            int nbColumns = rawMapData[0].Length;
            for (int l = 0; l < nbLines; l++)
            {
                for (int c = 0; c < nbColumns; c++)
                {
                    pmapData[l, c] = rawMapData[l][c];
                }
            }

        }

        [DataMember]
        public int[][] rawMapData;
    }
}
