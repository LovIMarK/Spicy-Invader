using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spicy_Projet.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Collision_entre_2_missiles()
        {
            // Arrange
            List<Robots> listRobots = new List<Robots>();
            int[,] posShield = new int[0,0];
            FriendlyShip player = new FriendlyShip(1, 20, posShield, listRobots);
            Robots robots = new Robots(1, 10,posShield,player,listRobots, true,false);


            // Act
            robots.MissileRobots.MissileFired = true;
            player.MissilePlayer.MissileFired = true;
            while (player.MissilePlayer.MissileY != 0)
            {
                // Check if the missile touch a another player's missile
                if (robots.MissileRobots.MissileX == player.MissilePlayer.MissileX && robots.MissileRobots.MissileY >= player.MissilePlayer.MissileY)
                {
                    robots.MissileRobots.MissileFired = false;
                    player.MissilePlayer.MissileFired = false;
                    break;
                }
                player.MissilePlayer.MissileY--;
            }


            //Assert
            Assert.AreEqual(false, robots.MissileRobots.MissileFired, "le missile doit être mort");
            Assert.AreEqual(false, player.MissilePlayer.MissileFired, "le missile doit être mort");
        }

        [TestMethod]
        public void Collision_entre_1_missile_et_1_joueur()
        {
            // Arrange
            List<Robots> listRobots = new List<Robots>();
            int[,] posShield = new int[0, 0];
            FriendlyShip player = new FriendlyShip(1, 20, posShield, listRobots);
            Robots robots = new Robots(1, 20, posShield, player, listRobots, true, false);
            player.Life = 3;

            // Act
            while (robots.MissileRobots.MissileY < 28)
            {
                
                // Check if the missile touch a pixel of the player
                if (robots.MissileRobots.MissileY == player.ShipY - 1)
                {
                    if (robots.MissileRobots.MissileX >= player.ShipX && robots.MissileRobots.MissileX <= player.ShipX + player.ShipForm.Length - 1)
                    {
                        robots.MissileRobots.MissileY = 28;
                        player.Life = 2;
                    }
                }
                robots.MissileRobots.MissileY++;
            }


            //Assert
            Assert.AreEqual(2, player.Life, "La vie du joueur doit être 2");
        }
    }
}
