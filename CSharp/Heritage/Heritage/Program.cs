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
            Barbarian monBarbare = new Barbarian("Conan");
            Druid monDruide = new Druid("Jaheira");
            monDruide.LancerUnSort("eclair");
            Console.WriteLine("Le nom du personnage est " + monBarbare.name);
            monBarbare.Attack();
            Console.ReadKey();

        }
    }
}
