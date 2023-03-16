using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRPG
{
    class Personnage
    {
        public string Nom;
        public int Force;
        public int Dextérité;
        public int Constitution;
        public int Intelligence;
        public int Sagesse;
        public int Charisme;

        public Personnage()
        {
            Console.WriteLine("Personnage / Constructeur sans paramètres");
            Nom = "";
        }

        public Personnage(string pNom)
        {
            Console.WriteLine("Personnage / Constructeur avec nom");
            Nom = pNom;
        }

        public void Genere()
        {
            Console.WriteLine("Personnage / Génération d'un personnage");

            LanceurDé lanceur = new LanceurDé();
            
            Force = lanceur.Lance(4, "1D6", true);
            Dextérité = lanceur.Lance(4, "1D6", true);
            Constitution = lanceur.Lance(4, "1D6", true);
            Intelligence = lanceur.Lance(4, "1D6", true);
            Sagesse = lanceur.Lance(4, "1D6", true);
            Charisme = lanceur.Lance(4, "1D6", true);
        }

        public void Affiche()
        {
            Console.WriteLine("Personnage {0}", Nom);
            Console.WriteLine("Force : {0}", Force);
            Console.WriteLine("Dextérité : {0}", Dextérité);
            Console.WriteLine("Constitution : {0}", Constitution);
            Console.WriteLine("Intelligence : {0}", Intelligence);
            Console.WriteLine("Sagesse : {0}", Sagesse);
            Console.WriteLine("Charisme : {0}", Charisme);
        }

        public override string ToString()
        {
            string s;

            s = "Personnage " + Nom;
            s += "\n=========================\n";
            s += "Force : " + Force + "\n";
            s += "Dextérité : " + Dextérité + "\n";
            s += "Constitution : " + Constitution + "\n";
            s += "Intelligence : " + Intelligence + "\n";
            s += "Sagesse : " + Sagesse + "\n";
            s += "Charisme : " + Charisme + "\n";
            s += "\n";
            
            return s;
        }
    }
}
