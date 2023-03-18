using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage
{
    class Barbarian : Personnage
    {
        public Barbarian(string pName) : base(pName)
        {
            Console.WriteLine("Je suis un nouveau barbare");
            dicePointsOfLife = "1D12";
        }
        public override void Attack()
        {
            base.Attack();
            Console.WriteLine("Je suis un barbare et j'attaque !");
        }
        public override void Attaque()
        {
            Console.WriteLine("Je suis un barbare et j'attaque !");
        }
    }
}
                                                