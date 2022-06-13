//Author            : Mark Lovink
//Date              : 13.01.2022
//Company           : Etml, Lausanne
//Description       : Classe qui s'occupe du menu du jeu

using System;
using System.Collections.Generic;
using static System.Console;

namespace Spicy_Projet
{
    class StartSettings
    {

        private bool _soundON = true;  //Variable qui stoque l'état de l'option musique 
        private bool _difficutly;//Variable qui stoque l'état de l'option difficulté          
        static List<int> score = new List<int>(); //Liste qui stoque les scors          
        static List<string> playerName = new List<string>(); //Liste qui stoque les noms des joueurs

        
        /// <summary>
        /// Méthode qui affiche le menu du jeu, Titre, Jouer, Option, Score, About, Exit
        /// </summary>
        public void StartGameSettings()
        {
            Clear();
            //Ajuster la taille de la console
            SetWindowSize(120, 35);

            CursorVisible = false;

            int topPos = 16;
            int startLeftPos = 4;
            int userChoice =0;
            ConsoleKeyInfo keyInfo;
            

            GameSettings NewGame = new GameSettings(_difficutly);

            ForegroundColor = ConsoleColor.DarkYellow;
            //titre du jeu
            WriteLine(@" 
                  (                                  (                                                 
                   )\ )                               )\ )                           (                  
                  (()/(           (          (       (()/(             )        )    )\ )     (    (    
                  /(_))  `  )    )\    (    )\ )     /(_))   (       /((    ( /(   (()/(    ))\   )(   
                 (_))    /(/(   ((_)   )\  (()/(    (_))     )\ )   (_))\   )(_))   ((_))  /((_) (()\  
                 / __|  ((_)_\   (_)  ((_)  )(_))   |_ _|   _(_/(   _)((_) ((_)_    _| |  (_))    ((_) 
                 \__ \  | '_ \)  | | / _|  | || |    | |   | ' \))  \ V /  / _` | / _` |  / -_)  | '_| 
                 |___/  | .__/   |_| \__|   \_, |   |___|  |_||_|    \_/   \__,_| \__,_|  \___|  |_|   
                        |_|                 |__/                  ");


            SetCursorPosition(startLeftPos, topPos-3);
            WriteLine("Bienvenue dans le Spicy Invador");

            SetCursorPosition(1, topPos);
            ForegroundColor = ConsoleColor.Cyan;
            Write(">> ");
            ResetColor();
            SetCursorPosition(startLeftPos, 16 );
            WriteLine("Jouer");
            SetCursorPosition(startLeftPos, 17);
            WriteLine("Options");
            SetCursorPosition(startLeftPos, 18);
            WriteLine("Score");
            SetCursorPosition(startLeftPos, 19);
            WriteLine("About");
            SetCursorPosition(startLeftPos, 20);
            WriteLine("Exit");

            //Déplace la flèche dans le menu du jeu
            do
            {
                keyInfo = ReadKey(true);

                if (keyInfo.Key == ConsoleKey.DownArrow)
                {

                    MoveBufferArea(1, topPos, 2, 2, 1, topPos + 1);
                    topPos++;
                    if (topPos == 21)
                    {
                        topPos = 16;
                        MoveBufferArea(1, 21, 2, 2, 1, topPos);
                    }
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                   
                    MoveBufferArea(1, topPos, 2, 2, 1, topPos - 1);
                    topPos--;
                    if (topPos ==15)
                    {
                        topPos = 20;
                        MoveBufferArea(1, 15, 2, 2, 1, topPos );


                    }

                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    userChoice = topPos;
                }
                

            }
            while (keyInfo.Key != ConsoleKey.Enter);


            //Appel de méthode quand le joueur choisi une option dans le menu
            switch (userChoice)
            {
                //Lance le jeu
                case 16:
                    Clear();
                    Sound.GameMusic();
                    NewGame.GameStarted();
                    score.Add(NewGame.GetScore());
                    playerName.Add(NewGame.GetName());
                    Clear();
                    StartGameSettings();

                    break;
                    //Ouvre les options
                case 17:
                    Options();
                    break;
                //Ouvre le score
                case 18:
                    Score();
                    break;
                //Ouvre le à propos
                case 19:
                    About();
                    break;
                    //Quitte le programme
                case 20:
                    Environment.Exit(0);
                    break;



            }

        }

        /// <summary>
        /// Méthode qui s'occupe du choix de la difficulté et de la musique
        /// </summary>
        public void Options()
        {
            ConsoleKeyInfo keyInfo;
            string sound="" ;
            string difficulty = "";
            if (!_soundON)
            {
                sound = "Off";
            }
            else if (_soundON)
            {
                sound = " On";
            }

            if (!_difficutly)
            {
                difficulty = "    Facile";
            }
            else if (_difficutly)
            {
                difficulty = " Difficile";
            }



            int topPos = 3;
            int userChoice = 0;

            

            Clear();

            WriteLine("Option");
            WriteLine("--------------------");
            SetCursorPosition(1, topPos);
            ForegroundColor = ConsoleColor.Cyan;
            Write(">> ");
            ResetColor();
  
            SetCursorPosition(4, topPos);
            WriteLine("Son " +sound);
            SetCursorPosition(4, topPos + 2);
            WriteLine("Difficulté " + difficulty);
            SetCursorPosition(4, topPos + 4);
            WriteLine("Retour " );

            do
            {  //Déplace la flèche dans le menu des options
                do
                {
                    keyInfo = ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.DownArrow && topPos < 7)
                    {

                        MoveBufferArea(1, topPos, 2, 2, 1, topPos + 2);
                        topPos += 2;
                    }
                    else if (keyInfo.Key == ConsoleKey.UpArrow && topPos > 3)
                    {
                        MoveBufferArea(1, topPos, 2, 2, 1, topPos - 2);
                        topPos -= 2;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        userChoice = topPos - 3;
                    }

                }
                while (keyInfo.Key != ConsoleKey.Enter);

                //Change les options de son et de difficulté 
                switch (userChoice)
                {

                    case 0:
                        _soundON = !_soundON;
                        if (!_soundON)
                        {
                            sound = "Off";
                            Sound.MusicOver();
                        }
                        else if (_soundON)
                        {
                            sound = " On";
                            Sound.MenuMusic();
                        }
                        SetCursorPosition(4, topPos);
                        WriteLine("Son " + sound);
                        break;
                    case 2:
                        _difficutly = !_difficutly;
                        if (!_difficutly)
                        {
                            sound = "    Facile";
                        }
                        else if (_difficutly)
                        {
                            sound = " Difficile";
                        }
                        SetCursorPosition(4, topPos );
                        WriteLine("Difficulté " + sound);
                        break;
                    case 4:
                        StartGameSettings();
                        break;




                }
            } while (userChoice != 4);

        }


        /// <summary>
        /// Méthode qui s'occupe d'afficher les scores et le noms des joueurs
        /// </summary>
        public void Score()
        {
            Clear();
            WriteLine("Score");
            WriteLine("--------------------");
            SetCursorPosition(0, 3);
            for (int i = 0; i < playerName.Count; i++)
            {
                ForegroundColor = ConsoleColor.Cyan;      
                WriteLine("Score de "+playerName[i]+" : "+score[i]);
                ResetColor();
                WriteLine();
            }
            ReadKey();
            StartGameSettings();
        }

        /// <summary>
        /// Méthode qui s'occupe d'afficher les règles du jeu
        /// </summary>
        public void About()
        {
            Clear();
            WriteLine("A propos");
            WriteLine("--------------------");
            SetCursorPosition(3, 4);
            WriteLine("Le but du jeu est de détruire tout les robots sans se faire éliminer par ces derniers.");
            SetCursorPosition(3, 5);
            WriteLine("A chaque robot détruit vous gagné 10 point.");
            SetCursorPosition(3, 6);
            Write("Le déplacement s'effectue grâce aux touches directionnel");
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine(" ← →");
            ResetColor();
            SetCursorPosition(3, 7);
            Write("Pour détruire les robots vous pouvez tirer avec la ");
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("barre espace");
            ResetColor();
            SetCursorPosition(3, 8);
            WriteLine("Vous pouvez changer la difficulté du jeu dans les options.");
 

          


     

            ReadKey();
            StartGameSettings();

        }

            
        

    }
}
