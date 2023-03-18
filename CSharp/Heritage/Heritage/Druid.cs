using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage
{
    internal class Druid : Personnage
    {
        public Druid(string pName) : base(pName)
        {
            Console.WriteLine("Je suis un nouveau druide");
            dicePointsOfLife = "1D8";
        }
        public void LancerUnSort(string pSpell)
        {
            Console.WriteLine("Je lance le sort : " + pSpell);
        }
        public override void Attack()  
        {
            base.Attack();
            Console.WriteLine("Je suis un druide et j'attaque !");
        }
        public override void Attaque()
        {
            Console.WriteLine("Je suis un druide et j'attaque !");
        }
    }
}
