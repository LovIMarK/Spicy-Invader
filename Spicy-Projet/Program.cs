//Author            : Mark Lovink
//Date              : 13.01.2022
//Company           : Etml, Lausanne
//Description       : Classe instancie le menu du jeu
using static System.Console;

namespace Spicy_Projet
{
     class Program
    {
       
        static void Main()
        {
            

            StartSettings Options = new StartSettings();

            Sound.MenuMusic();//Allume la musique du menu
            Options.StartGameSettings();//Affiche le menu
            
          
           
            

        }
    }
}

