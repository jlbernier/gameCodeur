using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestRPG
{
    class Program
    {
        static void Terminal(string pString)
        {
            Random random = new Random();
            Console.WriteLine("");
            foreach (char caractere in pString)
            {
                Console.Write("\b");
                Console.Write(caractere);
                Console.Write("#");
                Thread.Sleep(random.Next(10,50));
            }
            Console.Write("\b ");
            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            string ligne = new string('=', 100);
            LanceurDé monLanceur;
            monLanceur = new LanceurDé();
            LanceurDé monAutreLanceur;
            monAutreLanceur = new LanceurDé();

            Console.ForegroundColor = ConsoleColor.Green;
            Terminal("------- Création d'un personnage de RPG -------");
            Console.ForegroundColor = ConsoleColor.White;

            // 
            Personnage monPerso = new Personnage("Conan");

            monPerso.Genere();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(monPerso);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Personnage généré !");

            Console.Read();
        }
    }
}
