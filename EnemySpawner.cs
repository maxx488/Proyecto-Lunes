using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class EnemySpawner
    {
        private List<Enemy> enemyList = new List<Enemy>();
        private Random random = new Random();
        private float enemySpawnTimer;
        private float enemySpawnCooldown;

        public EnemySpawner()
        {
            enemySpawnCooldown = (float) random.Next(3); // Segundos que pasaran hasta spawnear un enemigo
        }

        public List<Enemy> EnemyList => enemyList;

        public void Update()
        {
            enemySpawnTimer += Program.deltaTime;
            if (enemySpawnTimer > enemySpawnCooldown)
            {
                enemyList.Add(new Enemy(new Vector2(random.Next(960), -64), false));
                enemySpawnTimer = 0;
                enemySpawnCooldown = (float) random.Next(3);
            }
        }
    }
}
