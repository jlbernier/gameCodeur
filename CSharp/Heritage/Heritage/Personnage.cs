using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage
{
    abstract class Personnage
    {
        public string name { get; protected set; }
        public int totalPointsOfLife;
        protected string dicePointsOfLife;
        
        public Personnage(string pName)
        {
            Console.WriteLine("Je suis un nouveau personnage mon nom est "+pName);
            name = pName;
            dicePointsOfLife = "";
        }
        public abstract void Attack();
    }
}
