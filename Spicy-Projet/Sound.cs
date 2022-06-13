//Author            : Mark Lovink
//Date              : 13.01.2022
//Company           : Etml, Lausanne
//Description       : Class qui s'occupe de toutes les musiques du jeu

using WMPLib;

namespace Spicy_Projet
{

    public class Sound
    {
        static WindowsMediaPlayer gameSound = new WindowsMediaPlayer(); //Variable de musique pour le jeu
        static WindowsMediaPlayer menuSound = new WindowsMediaPlayer(); //Variable de musique pour le menu
        static WindowsMediaPlayer deadSound = new WindowsMediaPlayer(); //Variable de musique pour la mort d'un robot
        static WindowsMediaPlayer damaged = new WindowsMediaPlayer(); //Variable de musique pour la perte de vie du joueur
        static WindowsMediaPlayer fired = new WindowsMediaPlayer(); //Variable de musique pour le tire du joueur
        static WindowsMediaPlayer playerKilled = new WindowsMediaPlayer(); //Variable de musique pour la mort du joueur
        static private bool _soundOn = true; //Variable qui stoque l'état de l'option musique 


        /// <summary>
        /// Méthode qui allume la musique du menu
        /// </summary>
        public static void MenuMusic()
        {
            gameSound.controls.stop();//Eteint la musique du jeu
            _soundOn = true;
            menuSound.URL = "menuMusic.mp3"; //Allume la musique


        }

        /// <summary>
        /// Méthode qui éteint toutes les musiques
        /// </summary>
        public static void MusicOver( )
        {
          
                menuSound.controls.stop();//Eteint la musique du menu
                _soundOn = false;//Empeche les autres musique de se lancer
            
        }

        /// <summary>
        /// Méthode qui allume la musique du jeu
        /// </summary>
        public static void GameMusic()
        {
            if (_soundOn)
            {
                menuSound.controls.stop();//Eteint la musique du menu
                gameSound.URL = "gameMusic.mp3"; //Allume la musique 
            }
        }

        /// <summary>
        /// Méthode qui allume la musique d'explosion d'un robot
        /// </summary>
        public static void RobotExplosionSound()
        {
            if (_soundOn)//Si l'option musique est active
                deadSound.URL = "robotExplosion.wav"; //Allume la musique 
        }

        /// <summary>
        /// Méthode qui allume la musique quand le joueur se fait toucher
        /// </summary>
        public static void PlayerDamagedSound()
        {
            if (_soundOn)//Si l'option musique est active
                damaged.URL = "playerTouch.wav"; //Allume la muusique
        }

        /// <summary>
        /// Méthode qui allume la musique quand le joueur tir un missile
        /// </summary>
        public static void MissileFiredSound()
        {
            if (_soundOn) //Si l'option musique est active
                fired.URL = "missileFired.wav";//Allume la musique
        }

        /// <summary>
        /// Méthode qui allume la musique quand le joueur perd toutes ses vies
        /// </summary>
        public static void GameOverSound( )
        {
            if (_soundOn)//Si l'option musique est active
                playerKilled.URL = "gameOver.wav";//Allume la musique
        }


    }
}
