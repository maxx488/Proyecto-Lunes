using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class ProyectileSpawner
    {
        private List<Player> playerToCheck;
        private List<Enemy> enemiesToCheck;
        private List<Projectile> projectileList = new List<Projectile>();

        public ProyectileSpawner(List<Player> playerToCheck, List<Enemy> enemiesToCheck)
        {
            this.playerToCheck = playerToCheck;
            this.enemiesToCheck = enemiesToCheck;
        }

        public List<Projectile> ProjectileList => projectileList;

        public void Update()
        {
            if (playerToCheck.Count > 0)
            {
                if (playerToCheck[0].ShootState == true)
                {
                    projectileList.Add(new Projectile(new Vector2(playerToCheck[0].GetPlayerTransform.Position.X + 25, playerToCheck[0].GetPlayerTransform.Position.Y - 20), 1, 500, playerToCheck[0].GetPower));
                    if (playerToCheck[0].GetPower == 3)
                    {
                        projectileList.Add(new Projectile(new Vector2(playerToCheck[0].GetPlayerTransform.Position.X + 10, playerToCheck[0].GetPlayerTransform.Position.Y - 20), 2, 500, playerToCheck[0].GetPower));
                        projectileList.Add(new Projectile(new Vector2(playerToCheck[0].GetPlayerTransform.Position.X + 40, playerToCheck[0].GetPlayerTransform.Position.Y - 20), 3, 500, playerToCheck[0].GetPower));
                    }
                }
            }
            if (enemiesToCheck.Count > 0)
            {
                for (int i = 0; i < enemiesToCheck.Count; i++)
                {
                    if (enemiesToCheck[i].ShootState == true && enemiesToCheck[i].GetEnemyData.ShootPosX != null)
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
