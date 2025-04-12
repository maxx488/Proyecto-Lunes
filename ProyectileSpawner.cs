using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class ProyectileSpawner
    {
        private Player playerToCheck;
        private List<Enemy> enemiesToCheck;
        private List<Projectile> projectileList = new List<Projectile>();

        public ProyectileSpawner(Player player, List<Enemy> enemies)
        {
            this.playerToCheck = player;
            this.enemiesToCheck = enemies;
        }

        public List<Projectile> ProjectileList => projectileList;

        public Player SetPlayerToCheck
        {
            set
            {
                playerToCheck = value;
            }
        }

        public void Update()
        {
            if (playerToCheck != null)
            {
                if (playerToCheck.ShootState == true)
                {
                    projectileList.Add(new Projectile(new Vector2(playerToCheck.GetPlayerTransform.Position.X + 25, playerToCheck.GetPlayerTransform.Position.Y - 20), 1, 500, playerToCheck.GetPower));
                    if (playerToCheck.GetPower == 3)
                    {
                        projectileList.Add(new Projectile(new Vector2(playerToCheck.GetPlayerTransform.Position.X + 10, playerToCheck.GetPlayerTransform.Position.Y - 20), 2, 500, playerToCheck.GetPower));
                        projectileList.Add(new Projectile(new Vector2(playerToCheck.GetPlayerTransform.Position.X + 40, playerToCheck.GetPlayerTransform.Position.Y - 20), 3, 500, playerToCheck.GetPower));
                    }
                }
            }
            if (enemiesToCheck.Count > 0)
            {
                for (int i = 0; i < enemiesToCheck.Count; i++)
                {
                    if (enemiesToCheck[i].ShootState == true)
                    {
                        for (int j = 0; j < enemiesToCheck[i].GetEnemyData.ShootPosX.Length; j++)
                        {
                            projectileList.Add(new Projectile(new Vector2(enemiesToCheck[i].GetEnemyTransform.Position.X + enemiesToCheck[i].GetEnemyData.ShootPosX[j], enemiesToCheck[i].GetEnemyTransform.Position.Y + enemiesToCheck[i].GetEnemyData.ShootPosY), -1, 500, 1));
                        }
                    }
                }
            }
        }
    }
}
