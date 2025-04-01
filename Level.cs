using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Level
    {
        private Image background = Engine.LoadImage("assets/background.png");
        Player player = new Player(400, 650);
        private List<Enemy> enemyList = new List<Enemy>();
        Random random = new Random();
        private int enemyCount;
        private List<Projectile> projectileList = new List<Projectile>();
        PowerUpStack powerUpStack = new PowerUpStack();
        private float timer;
        private float cooldown = 0.25f;
        private float spawnTimer;
        private float spawnCooldown;


        public Level()
        {
            enemyCount = random.Next(20,31);
            spawnCooldown = (float) random.Next(3);
            powerUpStack.InitializeStack();
        }

        public int GetPlayerHealth => player.Health;

        public void Input()
        {
            player.Input();
            ModifyHealth();
            ModifyPower();
        }

        public void Update()
        {
            EnemySpawn();
            EnemyUpdate();
            player.Update();
            BulletSpawn();
            ProjectileUpdate();
        }

        public void Render()
        {
            Engine.Draw(background, 0, 0);
            EnemyRender();
            player.Render();
            ProjectileRender();
        }

        private void ModifyHealth()
        {
            timer += Program.deltaTime;
            if (timer > cooldown)
            {
                if (Engine.GetKey(Engine.KEY_UP))
                {
                    timer = 0;
                    player.Health += 1;
                }
                if (Engine.GetKey(Engine.KEY_DOWN))
                {
                    timer = 0;
                    player.Health -= 1;
                }
            }
        }

        private void ModifyPower()
        {
            timer += Program.deltaTime;
            if (timer > cooldown)
            {
                if (Engine.GetKey(Engine.KEY_RIGHT))
                {
                    timer = 0;
                    if (powerUpStack.FullStack() == false)
                    {
                        powerUpStack.Stack(random.Next(2, 4));
                        player.SetPower = powerUpStack.Top();
                    }
                }
                if (Engine.GetKey(Engine.KEY_LEFT))
                {
                    timer = 0;
                    if (powerUpStack.EmptyStack() == false)
                    {
                        powerUpStack.Remove();
                        if (powerUpStack.EmptyStack() == false)
                        {
                            player.SetPower = powerUpStack.Top();
                        }
                        else
                        {
                            player.SetPower = 1;
                        }
                    }
                }
            }
        }

        private void BulletSpawn()
        {
            if (player.Shooting == true)
            {
                projectileList.Add(new Projectile(player.GetPosX + 25, player.GetPosY - 20, 1, 500, player.GetPower));
                if (player.GetPower == 3)
                {
                    projectileList.Add(new Projectile(player.GetPosX + 10, player.GetPosY - 20, 2, 500, player.GetPower));
                    projectileList.Add(new Projectile(player.GetPosX + 40, player.GetPosY - 20, 3, 500, player.GetPower));
                }
                player.Shooting = false;
            }
            if (enemyList.Count > 0)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i].Shooting == true)
                    {
                        projectileList.Add(new Projectile(enemyList[i].GetPosX + 27, enemyList[i].GetPosY + 65, -1, 500, 1));
                        enemyList[i].Shooting = false;
                    }
                }
            }
        }

        private void ProjectileUpdate()
        {
            if (projectileList.Count > 0)
            {
                for (int i = 0; i < projectileList.Count; i++)
                {
                    projectileList[i].Update();
                }
                for (int i = 0; i < projectileList.Count; i++)
                {
                    if (projectileList[i].InBounds == false)
                    {
                        projectileList.RemoveAt(i);
                    }
                }
            }
        }

        private void ProjectileRender()
        {
            if (projectileList.Count > 0)
            {
                foreach (Projectile projectile in projectileList)
                {
                    projectile.Render();
                }
            }
        }

        private void EnemySpawn()
        {
            spawnTimer += Program.deltaTime;
            if (spawnTimer > spawnCooldown)
            {
                enemyList.Add(new Enemy(random.Next(960), -64));
                spawnTimer = 0;
                spawnCooldown = (float)random.Next(3);
            }

        }

        private void EnemyUpdate()
        {
            if (enemyList.Count > 0)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    enemyList[i].Update();
                }
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i].InBounds == false)
                    {
                        enemyList.RemoveAt(i);
                    }
                }
            }
        }

        private void EnemyRender()
        {
            foreach (Enemy enemy in enemyList)
            {
                enemy.Render();
            }
        }
    }
}
