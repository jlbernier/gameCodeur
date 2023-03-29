using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Character
    {
        public string Nom;
        public int Force;
        public int Dextérité;
        public int Constitution;
        public int Intelligence;
        public int Sagesse;
        public int Charisme;

        public Character()
        {
            Console.WriteLine("Personnage / Constructeur sans paramètres");
            Nom = "";
        }

        public Character(string pNom)
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
        }

        public override string ToString()
        {
            string s;

            s = "Personnage " + Nom;
            s += "\n=========================\n";
            s += "Force : " + Force;
            s += "\n";

            return s;
        }
    }
}
