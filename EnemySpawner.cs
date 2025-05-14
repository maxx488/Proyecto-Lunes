using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class EnemySpawner
    {
        private List<GameObject> objList;
        private EnemyQueue enemyQueueRef;
        private Random random = new Random();
        private float enemySpawnTimer;
        private float enemySpawnCooldown;
        private int faction;
        private bool bossLevel = false;
        private LevelHud levelHudRef;
        private EnemyData enemyData;

        public EnemySpawner(List<GameObject> objList, int fact, EnemyQueue queue, LevelHud hud, bool bossLevel)
        {
            this.objList = objList;
            faction = fact;
            this.enemyQueueRef = queue;
            this.levelHudRef = hud;
            enemySpawnCooldown = (float) random.Next(3); // Segundos que pasaran hasta spawnear un enemigo
            if (bossLevel == true)
            {
                this.bossLevel = true;
                objList.Add(new Enemy(new Vector2(200, 25), faction, 5, true));
            }
        }

        public void Update()
        {
            if (bossLevel == false)
            {
                for (int i = 0; enemyQueueRef.FullQueue() == false; i++) // Mientras que la cola no este llena
                {
                    enemyQueueRef.Enqueue(random.Next(1, 5)); // agregar enemigo a cola
                }
                levelHudRef.DisplayQueueUpdate();
                enemySpawnTimer += Time.DeltaTime;
                if (enemySpawnTimer > enemySpawnCooldown)
                {
                    var typ = enemyQueueRef.First();
                    enemyData = new EnemyData(typ);
                    objList.Add(new Enemy(new Vector2(random.Next(1024 - enemyData.SizeX), -enemyData.SizeY), faction, typ, false));
                    enemyQueueRef.Dequeue();
                    enemySpawnTimer = 0;
                    enemySpawnCooldown = (float)random.Next(3);
                }
            }
        }
    }
}
