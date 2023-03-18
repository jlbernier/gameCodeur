using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Barbarian myBarbarian = new Barbarian("Conan");
            Druid myDruid = new Druid("Jaheira");
            List<Personnage> myListe = new List<Personnage>();
            myListe.Add(myDruid);
            myListe.Add(myBarbarian);
            foreach (Personnage perso in myListe)
            {
                perso.Attack();
            }
            Console.ReadKey();

        }
    }
}
