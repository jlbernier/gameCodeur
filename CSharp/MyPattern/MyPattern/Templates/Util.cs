using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
