//Author            : Mark Lovink
//Date              : 13.01.2022
//Company           : Etml, Lausanne
//Description       : Classe qui s'occupe du déplacement du vaissceau du joueur

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static System.Console;

namespace Spicy_Projet
{
   
    public class FriendlyShip
    {
    
        private List<Robots> _robots; //La liste de tous les robots


        /// <summary>
        /// Getter de la forme du vaissceau
        /// </summary>
        public string ShipForm { get; }

        /// <summary>
        /// Get setter de la position vertical du vaissceau
        /// </summary>
        public int ShipX { get; set; }

        /// <summary>
        ///  Get setter de la position horizontal du vaissceau
        /// </summary>
        public int ShipY { get; set; }

        /// <summary>
        /// Get setter de la classe du missile
        /// </summary>
        public Shoot MissilePlayer { get; set; }


        /// <summary>
        /// Get setter du score du joueur
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Get setter de la vie du joueur
        /// </summary>
        public int Life { get; set; }


        /// <summary>
        /// Get setter du statu du jeu
        /// </summary>
        public bool GameOn { get; set; }

        /// <summary>
        /// Get setter du nom du joueur
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Constructeur du vaissceau
        /// </summary>
        /// <param name="shipX">position horizontal du vaissceau</param>
        /// <param name="shipY">position vertical du vaissceau</param>
        /// <param name="PosShields">La position des protections</param>
        /// <param name="robots">La liste des robots</param>
        public FriendlyShip(int shipX, int shipY, int[,] PosShields, List<Robots> robots)
        {
            ShipForm = @"╔═╩═╗";  //Forme du robot
            this.ShipX = shipX;
            this.ShipY = shipY;
            this._robots = robots;
            this.Score = 0;
            this.Life = 3;
            this.GameOn = true;
            this.Name = "";
            this.MissilePlayer = new Shoot(ShipX, ShipY,false,PosShields,_robots,this);
        }

        [DllImport("User32.dll")]                       // Import the User32.dll
        static extern short GetAsyncKeyState( int key);  // Touche appuyé par le joueur

        /// <summary>
        /// Méthode qui s'occupe du déplacement du vaissceau 
        /// </summary>
        public void shipMouvement()
        {

            byte allRobot = 0; //Variable qui stoque le nombre de robots en vie
            do
            {
                allRobot = 0;

                // Si la flèche gauche est appuyé
                if (GetAsyncKeyState(37) < 0 && ShipX > 0)
                {
                    ShipX--;
                    MoveBufferArea(ShipX + 1, ShipY, ShipForm.Length, 1, ShipX, ShipY); //déplace le vaissceau à gauche
                    System.Threading.Thread.Sleep(10);

                }
                // Si la flèche droite est appuyé
                else if (GetAsyncKeyState(39) < 0 && ShipX < 65)
                {
                    ShipX++;
                    MoveBufferArea(ShipX - 1, ShipY, ShipForm.Length, 1, ShipX, ShipY); //déplace le vaissceau à droite   
                    System.Threading.Thread.Sleep(10);
                }
                if (KeyAvailable)
                {//Si la tocueh enter est appuyeé
                    ConsoleKeyInfo key = ReadKey(true);
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        
                        // Si le missile n'est pas tiré
                        if (MissilePlayer.MissileFired == false)
                        {
                            Sound.MissileFiredSound();//Allume le son de tir
                            MissilePlayer.MissileFired = true;
                            MissilePlayer.MissileY = ShipY - 1;
                            MissilePlayer.MissileX = ShipX + (ShipForm.Length / 2);
                            MissilePlayer.MissilePlayerCreate();//Crée le missile du joueur
                        }
                    }

                }
                //Vérifie le nombre de robots en vie
                foreach (Robots x in _robots)
                {
                    if (x != null)
                    {
                        allRobot++;
                    }
                    
                }
               

            } while (Life>0 && allRobot != 0); //Effectue la méthode tant que le joueur n'est pas mort ou tant que les robots ne sont pas tous mort

            
        }
     

    }
}
