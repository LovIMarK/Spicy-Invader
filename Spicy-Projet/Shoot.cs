//Author            : Mark Lovink
//Date              : 13.01.2022
//Company           : Etml, Lausanne
//Description       : Classe qui s'occupe des missiles

using System.Collections.Generic;
using static System.Console;
using System.Timers;

namespace Spicy_Projet
{
    /// <summary>
    /// Classe avec les paramètres des missiles des robots et du joueur ,la position des missiles, le statu des missiles , la création des missiles et le coportement des missiles
    /// </summary>
    public class Shoot
    {
        private Timer _shootPlayer = new Timer(40); // la vitesse du tire du vaisseau
        private Timer _shootRobot = new Timer(100); //La vitesse du tire des robots
        private string _missile = "|"; // la forme du missile 
        private int[,] _PosShields; // la postion des protections
        private List<Robots> _robots; // La liste de robot   
        private FriendlyShip _player; // Le vaisseau du joueur

        /// <summary>
        /// Get setter de la position de l'axe horizontal du missile 
        /// </summary>
        public int MissileX { get; set; }

        /// <summary>
        /// Get setter de la position de l'axe horizontal du missile 
        /// </summary>
        public int MissileY { get; set; }

        /// <summary>
        /// Get setter du statut du missile tiré ou non
        /// </summary>
        public bool MissileFired { get; set; }





        /// <summary>
        /// Constructeur pour le missile du joueur
        /// </summary>
        /// <param name="missileX">Position de l'axe horizontal du missile du joueur</param>
        /// <param name="missileY">Position de l'axe vertical du missile du joueur</param>
        /// <param name="missileLive">Etat du missile tiré ou non</param>
        /// <param name="PosShields">La position des protections</param>
        /// <param name="robots">La liste de tous les robots</param>
        /// <param name="player">Les paramètres du joueur</param>
        public Shoot(int missileX, int missileY, bool missileFired,  int[,] PosShields, List<Robots> robots, FriendlyShip player)
        {
            this.MissileX = missileX;
            this.MissileY = missileY;
            this.MissileFired = missileFired;
            this._PosShields = PosShields;
            this._robots = robots;
            this._player = player;
            _shootRobot = null;
            //Débute le timer
            SetTimerPlayer();
        }

        /// <summary>
        /// Constructeur pour le missile du robot
        /// </summary>
        /// <param name="missileX">Position de l'axe horizontal du missile du robot</param>
        /// <param name="missileY">Position de l'axe vertical du missile du robot</param>
        /// <param name="PosShields">La position des protections</param>
        /// <param name="player">Les paramètres du joueur</param>
        public Shoot(int missileX, int missileY, int[,] PosShields, FriendlyShip player)
        {
            this.MissileX = missileX;
            this.MissileY = missileY;
            this._PosShields = PosShields;
            this._player = player;
            _shootPlayer = null;
            //Débute le timer
            SetTimerRobot();
        }

        public void SetTimerPlayer()
        {
            //Associe le timer avec la méthode qui gère les paramètres du missile du joueur
            _shootPlayer.Elapsed += new ElapsedEventHandler(MissilePlayerMove);
        }

        public void SetTimerRobot()
        {
            //Associe le timer avec la méthode qui gère les paramètres du missile du robot
            _shootRobot.Elapsed += new ElapsedEventHandler(MissileRobotMove);
        }


        /// <summary>
        /// Méthode qui gère le déplacement des missile du joueur ainsi que ses actions
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void MissilePlayerMove(object source, ElapsedEventArgs e)
        {


            //Check si le joueur touche un bouclier
            if (_PosShields[MissileX, MissileY] == 1)
            {
                MoveBufferArea(MissileX, MissileY + 1, 1, 1, MissileX, MissileY); //Efface le bouclier
                _PosShields[MissileX, MissileY] = 0; //Détruit le bouclier
                MissileFired = false;
            }
            //Parcours la liste de robots
            for (int i = 0; i < _robots.Count; i++)
            {
                if (_robots[i] != null && MissileY == _robots[i].EnemyY)
                {


                    //Check si le missile du joueur touche un des 3 pixel du robot si le robot est en vie
                    if (MissileX >= _robots[i].EnemyX && MissileX <= _robots[i].EnemyX + _robots[i].ShipForm.Length - 1)
                    {
                        Sound.RobotExplosionSound(); //Sound d'explosion d'un robot
                        //MoveBufferArea(0, 0, 1, 1, this.MissileX, this.MissileY);
                        MoveBufferArea(0, 0, 3, 1, _robots[i].EnemyX, _robots[i].EnemyY);
                        _robots[i].MissileRobots._shootRobot.Stop();//Arrête le robot de tirer
                        _robots[i] = null; //Détruit le robot touché
                        MissileY = 0;//Détruit le missile du joueur
                        _player.Score += 10; // Ajoute le score au joueur
                        SetCursorPosition(WindowWidth / 2, 34);
                        Write(_player.Score);


                    }
                }
            }
            //Tant que le missile n'a pas touché le toit il continue de monter
            if (MissileY > 2) 
            {
                MissileY--;
                MoveBufferArea(MissileX, MissileY + 1, 1, 1, MissileX, MissileY);
            }
            //Sinon le missile s'efface 
            else
            {
                MoveBufferArea(MissileX - 1, MissileY, 1, 1, MissileX, MissileY);
                MissileFired = false;
            }

        }
        /// <summary>
        ///  Méthode qui gère le déplacement des missile des robtos ainsi que ses actions
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void MissileRobotMove(object source, ElapsedEventArgs e)
        {
          

            if (_player.GameOn)
            {

                if (MissileY == 25 && _PosShields[MissileX, 25] != 0)
                {

                    _PosShields[MissileX, 25] = 0;
                    MoveBufferArea(1, 1, 1, 1, MissileX, MissileY);
                    _shootRobot.Stop();
                    MissileFired = false;
                    MissileY = 1;

                }
                //Si le missile du robot est à la même hauteur que celui du ship 
                if (MissileY == _player.ShipY-1) //-1 pour un meilleur affichage
                {
                    //Parcours les pixels du joueur
                    for (int i = 0; i < _player.ShipForm.Length; i++)
                    {

                        //Si le missile d'un robot touche un des pixels du joueur
                        if (MissileX == _player.ShipX + i)
                        {
                            Sound.PlayerDamagedSound();//Bruit de joueur touché
                            _shootRobot.Stop();
                            MoveBufferArea(1, 1, 1, 1, MissileX, MissileY);//Efface le missile
                            MissileY = 2;//Détruit le missile
                            _player.Life--; //Joueur perd une vie
                            SetCursorPosition(_player.ShipX, _player.ShipY);
                            Write(_player.ShipForm);
                            SetCursorPosition(WindowWidth / 2, 34);
                            Write(_player.Score);
                          
                            //Affiche la vie du joueur
                            if (_player.Life == 2)
                            {

                                SetCursorPosition(WindowWidth - 8, 34);
                                Write("          ");
                                SetCursorPosition(WindowWidth - 8, 34);
                                WriteLine(" X X");
                            }
                            else if (_player.Life == 1)
                            {
                                SetCursorPosition(WindowWidth - 8, 34);
                                Write("          ");
                                SetCursorPosition(WindowWidth - 8, 34);
                                WriteLine(" X");

                            }
                           
                            break;
                        }
                    }
                }


             

                //Tant que le missile n'est pas à la hauteur du joueur il descend
                if (MissileY < _player.ShipY)
                {
                    MissileY++;
                    MoveBufferArea(MissileX, MissileY - 1, 1, 1, MissileX, MissileY);
                }
                else//Sinon il se fait détruire
                {
                    MoveBufferArea(1, 1, 1, 1, MissileX, MissileY);
                    MissileFired = false;
                    _shootRobot.Stop();
                }


            }
            else
            {
                _shootRobot.Stop();
            }

        }

        /// <summary>
        /// Méthode qui place et crée le missile du joueur
        /// </summary>
        public void MissilePlayerCreate()
        {
            SetCursorPosition(MissileX, MissileY);  // Place le curseur à la position du missile du joueur
            Write(_missile); // Affiche le missile
            _shootPlayer.Start(); //Démarre le timer de la vitesse du missile
        }

        /// <summary>
        /// Méthode qui place et crée les missiles des robots
        /// </summary>
        public void MissileRobotCreate()
        {
            SetCursorPosition(MissileX, MissileY);    // Place le curseur à la position du missile du robot
            Write(_missile); // Affiche le missile
            _shootRobot.Start();//Démarre le timer de la vitesse du missile
        }

        
      

    }
}
