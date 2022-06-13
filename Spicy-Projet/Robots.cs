//Author            : Mark Lovink
//Date              : 13.01.2022
//Company           : Etml, Lausanne
//Description       : Classe qui s'occupe de tous les paramètres des robot, du déplacement des robots et de la création des tirs du robot

using System;
using System.Collections.Generic;
using static System.Console;
using System.Timers;

namespace Spicy_Projet
{
    /// <summary>
    /// Classe avec la position des robots, la direction des robots, la création des tirs du robot, le déplacement des robots, la difficulté du jeu
    /// </summary>
    public class Robots
    {
   


        private Random _timeShoot = new Random(); // Variable qui stoque une intancie un compteur au hasard
        private List<Robots> _robots; //La liste des robots
        private Timer _robotsMovement = new Timer(200); //La valeur du déplacement des robots
        private Timer _robotsShooting = new Timer(250); //La du temps entre chaque tir des robots

        /// <summary>
        /// Getter de la form du robot
        /// </summary>
        /// 
        public string ShipForm { get; }

        /// <summary>
        /// Get setter de l'axe horizontal du robot
        /// </summary>
        public int EnemyX { get; set; }

        /// <summary>
        /// Get setter de l'axe vertical du robot
        /// </summary>
        public int EnemyY { get; set; }

        /// <summary>
        /// Get setter De la direction des robots
        /// </summary>
        public bool Direction { get; set; }

        /// <summary>
        /// Get setter de la possibilité de tir d'un robot
        /// </summary>
        public bool Shoot { get; set; }

        /// <summary>
        /// Get setter de la difficulté du mouvement des robots
        /// </summary>
        public bool Difficulty { get; set; }

        /// <summary>
        /// Get setter de la classe du missile
        /// </summary>
        public Shoot MissileRobots { get; set; }

        /// <summary>
        /// Constructeur des robots avec leurs position, forme, tir, difficulté
        /// </summary>
        /// <param name="EnemyX">Position de l'axe horizontal du robot</param>
        /// <param name="EnemyY">Position de l'axe vertical du robot</param>
        /// <param name="PosShields">La position des protections </param>
        /// <param name="player">Les paramètres du joueur</param>
        /// <param name="robots">La liste de tous les robots</param>
        /// <param name="direction">La direction de déplacement des robots</param>
        /// <param name="difficulty">La difficutlé du jeu</param>
        public Robots(int EnemyX, int EnemyY, int[,] PosShields, FriendlyShip player,List<Robots> robots, bool direction,bool difficulty)
        {
            ShipForm = "X╦X"; //Forme du robot
            this.EnemyX = EnemyX;
            this.EnemyY = EnemyY;
            this._robots = robots;
            this.Direction = direction;
            this.Difficulty = difficulty;
            this.MissileRobots = new Shoot(EnemyY, EnemyY, PosShields, player);

            //Associe le timer avec la méthode qui crée un missile pour les robots
            _robotsShooting.Elapsed += new ElapsedEventHandler(RobotsShoot);
            //Débute le timer
            _robotsShooting.Start();
        }
      

        /// <summary>
        /// Constructeur qui instancie le timer avec la méthode RobotsMouvement()
        /// </summary>
        /// <param name="robots">La liste contenant tous les robots</param>
        public Robots(List<Robots> robots)
        {
            this._robots = robots;
            //Si la difficulté est activé les robots se déplacent plus vite
            if (_robots[0].Difficulty == true)
            {
                _robotsMovement = new Timer(150);
             
            }
            //Associe le timer avec la méthode qui déplace les robots
            _robotsMovement.Elapsed += new ElapsedEventHandler(RobotsMouvement);
            //Débute le timer
            _robotsMovement.Start();
        }

        /// <summary>
        /// Méthode qui gére le déplacement des robots
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void RobotsMouvement(object source, ElapsedEventArgs e)
        {


            //Passe dans toutes la liste de robots
            for (int i = _robots.Count - 1; i >= 0; i--)
            {
                // Si le robot est en vie
                if (_robots[i] != null)
                {
                    //Si la direction est fausse déplace tous les robots à droite
                    if (Direction == false && _robots[i].EnemyX + 4 != WindowWidth )
                    {
                        //Déplace tous les robots à droite
                        MoveBufferArea(_robots[i].EnemyX, _robots[i].EnemyY, 3, 1, _robots[i].EnemyX + 1, _robots[i].EnemyY);
                        _robots[i].EnemyX++;

                    }
                    //Si le robot tout à droite touche la limite du jeu descend les robots d'un niveau
                    else if (Direction == false && _robots[i].EnemyX + 4 == WindowWidth )
                    {
                        //Descend les robots d'un niveau
                        foreach (Robots robots in _robots)
                        {
                            if (robots != null)
                            {
                                MoveBufferArea(robots.EnemyX, robots.EnemyY, 3, 1, robots.EnemyX - 1, robots.EnemyY + 1);
                                robots.EnemyY++;
                                robots.EnemyX--;
                            }
                        }
                        //Change les robots de direction
                        Direction = true;

                    }
                }

            }
            //Passe dans toutes la liste de robots
            for (int i = 0; i < _robots.Count; i++)
            {
                // Si le robot est en vie
                if (_robots[i] != null)
                {
                    //Si la direction est fausse déplace tous les robots à gauche
                    if (Direction == true && _robots[i].EnemyX >= 4 )
                    {
                        //Déplace tous les robots à gauche
                        MoveBufferArea(_robots[i].EnemyX, _robots[i].EnemyY, 3, 1, _robots[i].EnemyX - 1, _robots[i].EnemyY);
                        _robots[i].EnemyX--;
                    }
                    //Si le robot tout à gauche touche la limite du jeu descend les robots d'un niveau
                    else if (Direction == true && _robots[i].EnemyX <= 4 )
                    {
                        //Descend les robots d'un niveau si le robot est à la limite du jeu
                        foreach (Robots robots in _robots)
                        {
                            if (robots != null)
                            {
                                MoveBufferArea(robots.EnemyX, robots.EnemyY, 3, 1, robots.EnemyX + 1, robots.EnemyY + 1);
                                robots.EnemyY++;
                                robots.EnemyX++;
                            }
                        }
                        //Change les robots de direction
                        Direction = false;
                    }
                }
            }

            //Passe dans toutes la liste de robots
            for (int i = 0; i != _robots.Count; i++)
            {


                // Si le robot est en vie
                if (_robots[i] != null)
                {
                    // Check la première rangée de robots et attribut la valeur shoot true si le robot et en vie
                    if (i == 3 || i == 7 || i == 11 || i == 15 || i == 19)
                    {
                        _robots[i].Shoot = true;
                    }
                    // Check la deuxième rangée de robots et attribut la valeur shoot true si le robot et en vie et qu'aucun autre robots est devant ce dernier
                    else if (i == 2 || i == 6 || i == 10 || i == 14 || i == 18)
                    {
                        if (_robots[i + 1] == null)
                        {
                            _robots[i].Shoot = true;
                        }
                        else
                        {
                            _robots[i].Shoot = false;
                        }
                    }  // Check la troisième rangée de robots et attribut la valeur shoot true si le robot et en vie et qu'aucun autre robots est devant ce dernier
                    else if (i == 1 || i == 5 || i == 9 || i == 13 || i == 17)
                    {
                        if (_robots[i + 1] == null && _robots[i + 2] == null)
                        {
                            _robots[i].Shoot = true;
                        }
                        else
                        {
                            _robots[i].Shoot = false;
                        }
                    }  // Check la quatrième rangée de robots et attribut la valeur shoot true si le robot et en vie et qu'aucun autre robots est devant ce dernier
                    else if (i == 0 || i == 4 || i == 8 || i == 12 || i == 16)
                    {
                        if (_robots[i + 1] == null && _robots[i + 2] == null && _robots[i + 3] == null)
                        {
                            _robots[i].Shoot = true;
                        }
                        else
                        {
                            _robots[i].Shoot = false;
                        }
                    }
                }
            }



            }

        /// <summary>
        /// Méthode qui arrête le déplacement des robots à la fin du jeu
        /// </summary>
        public void RobotsDestruction()
        {
            //Arrêt du mouvement des robots
            _robotsMovement.Stop();
            _robotsShooting.Stop();


        }

        

        /// <summary>
        /// Méthode qui crée un missile selon la difficulté 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void RobotsShoot(object source, ElapsedEventArgs e)
        {
            for (int i = 0; i != _robots.Count; i++)
            {
                //Si la difficulté est en mode facile
                if (!Difficulty)
                {
                    //Choisi un nombre entre 0 et 14 si égal 0 le robot tire
                    if (_timeShoot.Next(0, 15) == 0 && Shoot == true && MissileRobots.MissileFired == false)
                    {
                        Sound.MissileFiredSound();//Allume le son de tir
                        MissileRobots.MissileY = EnemyY + 1;
                        MissileRobots.MissileX = EnemyX+1;
                        MissileRobots.MissileFired = true;
                        MissileRobots.MissileRobotCreate();

                    }
                }
                //Si la difficulté est en mode difficile
                if (Difficulty)
                {
                    //Choisi un nombre entre 0 et 5 si égal 0 le robot tire
                    if (_timeShoot.Next(0, 6) == 0 && Shoot == true && MissileRobots.MissileFired == false)
                    {
                        Sound.MissileFiredSound();//Allume le son de tir
                        MissileRobots.MissileY = EnemyY + 1;
                        MissileRobots.MissileX = EnemyX+1;
                        MissileRobots.MissileFired = true;
                        MissileRobots.MissileRobotCreate();

                    }

                }


            }
            





        }
    }
}
