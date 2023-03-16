using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TestRPG
{
    class LanceurDé
    {
        private Random random;
        public int dernierRésultat;

        public LanceurDé()
        {
            random = new Random();
        }

        public int Lance(int pNombre, string pType, bool pDropLowest = false)
        {
            int total = 0;
            int lowest = 0;

            for (int i = 0; i < pNombre; i++)
            {
                Lance(pType);
                if (lowest == 0 || dernierRésultat < lowest)
                {
                    lowest = dernierRésultat;
                }
                total = total + dernierRésultat;
                Console.WriteLine("Lance dé n°{0}, résultat:{1}, total est maintenant {2}", i, dernierRésultat, total);
            }
            if (pDropLowest)
            {
                Console.WriteLine("Droplowest ({0})", lowest);
                total -= lowest;
            }

            dernierRésultat = total;
            return total;
        }

        public int Lance(string pType)
        {
            dernierRésultat = 0;

            switch (pType)
            {
                case "1D6":
                    dernierRésultat = random.Next(1, 6 + 1);
                    break;
                case "1D20":
                    dernierRésultat = random.Next(1, 20 + 1);
                    break;
                default:
                    Debug.Fail("Mauvaise utilisation de Lance, type inconnu");
                    break;
            }

            return dernierRésultat;
        }
    }


}
