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
        private EnemyQueue enemyQueueRef;
        private Random random = new Random();
        private float enemySpawnTimer;
        private float enemySpawnCooldown;
        private int faction;
        private LevelHud levelHudRef;

        public EnemySpawner(int fact, EnemyQueue queue, LevelHud hud)
        {
            faction = fact;
            this.enemyQueueRef = queue;
            this.levelHudRef = hud;
            enemySpawnCooldown = (float) random.Next(3); // Segundos que pasaran hasta spawnear un enemigo
        }

        public List<Enemy> EnemyList => enemyList;

        public void Update()
        {
            for (int i = 0; enemyQueueRef.FullQueue() == false; i++) // Mientras que la cola no este llena
            {
                enemyQueueRef.Enqueue(random.Next(1, 4)); // agregar enemigo a cola
            }
            levelHudRef.DisplayQueueUpdate();
            enemySpawnTimer += Time.DeltaTime;
            if (enemySpawnTimer > enemySpawnCooldown)
            {
                var typ = enemyQueueRef.First();
                if (typ == 1 || typ == 2)
                {
                    var spawnX = random.Next(960);
                    enemyList.Add(new Enemy(new Vector2(spawnX, -64), faction, typ, false));
                }
                else
                {
                    var spawnX = random.Next(896);
                    enemyList.Add(new Enemy(new Vector2(spawnX, -64), faction, typ, false));
                }
                enemyQueueRef.Dequeue();
                enemySpawnTimer = 0;
                enemySpawnCooldown = (float) random.Next(3);
            }
        }
    }
}
