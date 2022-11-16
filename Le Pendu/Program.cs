using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ce script est le program du pendu.
namespace Le_Pendu
{
    class Program
    {
        private static string[] motsAleatoires = { "dictionnaire", "gestionnaire", "infini", "parachute", "constitution", "routine", "prisonnier", "approbation", "theoricien", "reseau" };
        private static Mots motsChoisis;
        private static char[] lettresADeviner;
        private static int vies;
        private static bool victoire;
        private static List <string> lettresUtilisees = new List<string>();

        static void Main(string[] args)
        {
            vies = 7;

            //Affiche le mot aléatoire.
            ChoisirMot();

            while (GameOver() == false)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(AfficherMotADeviner());
                Console.ResetColor();

                TesterLettre(ChoisirUneLettre());
                Console.WriteLine("\n");
                AfficherLettresUtilisees();
            }
            AfficherResultat();
        }
        private static void ChoisirMot()
        {
            //Génère le mot aléatoire.
            Random rnd = new Random();

            int mIndex = rnd.Next(motsAleatoires.Length);

            motsChoisis = new Mots(motsAleatoires[mIndex]);//création d'un objet de type mot

            lettresADeviner = new char[motsChoisis.tailleDuMot];

            //Cache le mot aléatoire.
            for (int i = 0; i < lettresADeviner.Length; i++)
            {
                lettresADeviner[i] = '*';
            }
        }

        //Affiche le mot à deviner.
        static string AfficherMotADeviner()
        {
            string mot = new string(lettresADeviner);

            return mot;
        }

        static string ChoisirUneLettre()
        {
            string input;

            do
            {
                //Demande au joueur une lettre.
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Choisissez UNE lettre");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                Console.ResetColor();
            }
            while (input.Length != 1);
            StockerLettresUtilisees(input);
            return input;
        }

        static void StockerLettresUtilisees(string input)
        {
            lettresUtilisees.Add(input);
            
        }

        static void AfficherLettresUtilisees()
        {
            string affichage = "";

            foreach (string lettre in lettresUtilisees)
            {
                affichage += lettre;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Lettres utilisées : " + affichage);
            Console.ResetColor();
        }
        static void TesterLettre(string input)
        {
            bool lettrePresente = false;

            //Teste la lettre choisie contre les lettres du mot aléatoire.
            for (int i = 0; i < motsChoisis.tailleDuMot; i++)
            {
                if (motsChoisis.mots[i] == input[0])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Bien joué la lettre correspond !");
                    Console.ResetColor();
                    lettrePresente = true;

                    //Remplacer l'étoile par la lettre trouvée.
                    lettresADeviner[i] = input[0];
                }

            }

            //Le joueur perd une vie.
            if (lettrePresente == false)
            {
                vies--;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oups la lettre ne correspond pas, réessayez !");
                Console.ResetColor();

                Console.WriteLine(VisuelPendu.pendus[6-vies]);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Il vous reste "+vies+" vie(s)");
                Console.ResetColor();

            }
        }

        private static bool GameOver()
        {
            //Le joueur n'a plus de vie.
            if (vies <= 0)
            {
                victoire = false;
                return true;
            }

            //Le joueur n'a pas deviné toutes les lettres.
            foreach (char lettre in lettresADeviner)
            {
                if (lettre == '*')
                {
                    return false;
                }
            }

            //Le joueur a deviné toutes les lettres et a encore des vies.
            victoire = true;
            return true;
        }

        private static void AfficherResultat()
        {
            //teste la victoire ou non et préviens le joueur une fois que c'est fini.
            if (victoire == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Bien joué ! Tu as gagné la partie !");
                Console.ResetColor();
                Restart();
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oh non ! Tu as perdu... :( Recommence une partie ! :)");
                Console.ResetColor();
                Restart();
            }
        }

        private static void Restart()
        {
            ConsoleKey reponse;
            do
            {
                Console.WriteLine("Veux-tu recommencer ? [O/N]");
                reponse = Console.ReadKey(true).Key;
            } while (reponse != ConsoleKey.O && reponse != ConsoleKey.N);
            
            if(reponse == ConsoleKey.O)
            {
                var info = new System.Diagnostics.ProcessStartInfo(Environment.GetCommandLineArgs()[0]);
                System.Diagnostics.Process.Start(info);
                System.Environment.Exit(0);
            }
            else
            {
                System.Environment.Exit(0);
            }
        }
    }
}