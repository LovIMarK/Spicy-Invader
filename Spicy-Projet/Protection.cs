//Author            : Mark Lovink
//Date              : 13.01.2022
//Company           : Etml, Lausanne
//Description       : Class qui s'occupe de la protection du joueur

using System;

namespace Spicy_Projet
{
    class Protection
    {
        /// <summary>
        /// Constructeur qui affiche la protection du joueur
        /// </summary>
        /// <param name="space">espace entre chque protection</param>
        public Protection(int space)
        {
            //Place le curseur à la bonne position
            Console.SetCursorPosition(Console.WindowWidth / 4 * space - 14, 25);
            //Affiche la protection du joueur
            Console.WriteLine("█████████████");
        }
    }
}
