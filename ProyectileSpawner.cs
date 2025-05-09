using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class ProyectileSpawner
    {
        private List<GameObject> objectsToCheck;

        public ProyectileSpawner(List<GameObject> objectsToCheck)
        {
            this.objectsToCheck = objectsToCheck;
        }

        public void Update()
        {
            if (objectsToCheck.OfType<Player>().Count() > 0)
            {
                for (int i = 0; i < objectsToCheck.Count; i++)
                {
                    if (objectsToCheck[i] is Player)
                    {
                        Player player = (Player) objectsToCheck[i];
                        if (player.ShootState == true)
                        {
                            objectsToCheck.Add(new Projectile(new Vector2(player.GetTransform.Position.X + 25, player.GetTransform.Position.Y - 20), 1, 500, player.GetPower));
                            if (player.GetPower == 3)
                            {
                                objectsToCheck.Add(new Projectile(new Vector2(player.GetTransform.Position.X + 10, player.GetTransform.Position.Y - 20), 2, 500, player.GetPower));
                                objectsToCheck.Add(new Projectile(new Vector2(player.GetTransform.Position.X + 40, player.GetTransform.Position.Y - 20), 3, 500, player.GetPower));
                            }
                        }
                        break;
                    }
                }
            }
            if (objectsToCheck.OfType<Enemy>().Count() > 0)
            {
                for (int i = 0; i < objectsToCheck.Count; i++)
                {
                    if (objectsToCheck[i] is Enemy)
                    {
                        Enemy enemy = (Enemy) objectsToCheck[i];
                        if (enemy.ShootState == true && enemy.GetEnemyData.ShootPosX != null)
                        {
                            for (int j = 0; j < enemy.GetEnemyData.ShootPosX.Length; j++)
                            {
                                objectsToCheck.Add(new Projectile(new Vector2(enemy.GetTransform.Position.X + enemy.GetEnemyData.ShootPosX[j], enemy.GetTransform.Position.Y + enemy.GetEnemyData.ShootPosY), -1, 500, 1));
                            }
                        }
                    }
                }
            }
        }
    }
}
