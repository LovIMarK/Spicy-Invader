//Author            : Mark Lovink
//Date              : 13.01.2022
//Company           : Etml, Lausanne
//Description       : Classe qui s'occupe de la création du jeu principal et de son affichage

using System;
using System.Collections.Generic;
using static System.Console;


namespace Spicy_Projet
{
    /// <summary>
    /// Classe qui instancie,crée et affiche les robots, le joueur, le score, les points de vie, les leves
    /// </summary>
    public class GameSettings
    {
        private FriendlyShip _player; //Le vaissceau du joueur
        private List<Robots> _robots = new List<Robots>(); //La liste de tous les robots
        private int[,] _PosShields = new int[70, 35]; //Un tableau avec la positon des protections
        private bool _difficutly; //Variable qui enregistre la difficulté du jeu
        private int _level = 1;//Le niveau du jeu


        /// <summary>
        /// Constructeur du jeu principal avec sa difficulté
        /// </summary>
        /// <param name="difficulty">difficulté du jeu</param>
        public GameSettings(bool difficulty)
        {
            this._difficutly = difficulty;
        }


        /// <summary>
        /// Méthode qui intancie et affiche les robots, le joueur, le score, le niveau et les vie, et du redémarrage du jeu
        /// </summary>
        public void GameStarted()
        {
            _level = 1;
            SetWindowSize(70, 35);
            CursorVisible = false;

            //Cération des 5 protections
            for (int i = 1; i < 5; i++)
            {
                new Protection(i); // Création des protections

                for (int j = 0; j < 13; j++) //Enregistre la psoitions des protections dans une liste
                {
                    _PosShields[(WindowWidth / 4 * i - 14) + j, 25] = 1;
                }
            }
            _player = new FriendlyShip(WindowWidth / 2 - 3, 30, _PosShields, _robots);//Crée le vaissceau du joueur
            SetCursorPosition(_player.ShipX, _player.ShipY);
            Write(_player.ShipForm);//Affiche le joueur
            SetCursorPosition(WindowWidth / 2 - 8, 34);
            Write("Score : " + _player.Score);//Affiche le score
            SetCursorPosition(WindowWidth - 14, 34);
            Write("Life : ");//Affiche les vies
            WriteLine("X X X");
            SetCursorPosition( 4, 34);
            Write("Level : "+_level);//Affiche le niveau

            do
            {
                System.Threading.Thread.Sleep(500);
                CreateRobots();//Crée les robots
                System.Threading.Thread.Sleep(500);
                Robots hord = new Robots(_robots); //Déplace tous les robots
                _player.shipMouvement();//Déplacement du joueur

                //Si le joueur n'a plus de vie
                if (_player.Life == 0)
                {
                    _player.GameOn = false;
                    Sound.GameOverSound();//Musique de perte
                    hord.RobotsDestruction();//Arrêt du déplacement des robots 
                    Clear();
                    System.Threading.Thread.Sleep(200);
                    Clear();
                    SetCursorPosition(WindowWidth / 2 - 8, WindowHeight / 2);
                    ForegroundColor = ConsoleColor.Magenta;
                    WriteLine("Game Over");//Affiche gam over
                    ResetColor();
                    ReadKey();
                    Clear();
                    Sound.MenuMusic();//Musique du menu
                    WriteLine("Score");
                    WriteLine("--------------------");
                    Write("Entrée votre nom : ");
                    _player.Name = ReadLine();//Enregistre le nom du joueur 
                }//S'il n'y a plus de robots en vie
                else
                {
                    hord.RobotsDestruction();//Arrêt du déplacement des robots 
                    _robots.Clear();//Vidé la liste des robots
                    _level++;//Augmente le niveau
                    SetCursorPosition(4, 34);
                    Write("Level :            ");
                    SetCursorPosition(4, 34);
                    Write("Level : " + _level);

                }
            }
            while (_player.Life > 0);
        }


        /// <summary>
        /// Méthode qui instancie les robots, les places, les ajoutes dans une liste
        /// </summary>
        public void CreateRobots()
        {
            //Crée 5 colonnes de robots
            for (int j = 0; j < 5; j++)
            {
                //Crée 4 lignes de robots
                for (int i = 0; i < 4; i++)
                {
                    //Instancie les robots
                    Robots first = new Robots(15 + 8 * j, 5 + 2 * i, _PosShields, _player, _robots, false, _difficutly);
                    //ajout des robots dans la liste
                    _robots.Add(first);
                    SetCursorPosition(first.EnemyX, first.EnemyY);
                    Write(first.ShipForm);//Affiche les robots
                }
            }
        }


        /// <summary>
        /// Méthode qui va récupérer le score du joueur
        /// </summary>
        /// <returns>Le score du joueur</returns>
        public int GetScore()
        {

            return _player.Score;

        }


        /// <summary>
        /// Méthode qui va récupérer le nom du joueur
        /// </summary>
        /// <returns>Le nom du joueur</returns>
        public string GetName()
        {

            return _player.Name;

        }






    }
}
