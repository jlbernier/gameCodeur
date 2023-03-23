using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templates;

namespace MTemplates
{
    internal class Util
    {
        static Random RandomGen = new();
        public static void SetRandomSeed(int pSeed)
        {
            RandomGen = new Random(pSeed);
        }
        public static int GetInt(int pMin, int pMax)
        {
            return RandomGen.Next(pMin, pMax + 1);
        }
        public static bool CollideByBox(IActor p1, IActor p2)
        {
            return p1.BoundingBox.Intersects(p2.BoundingBox);
        }
    }
}
