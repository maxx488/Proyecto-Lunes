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
        Player player = new Player(new Vector2 (400, 650));
        private List<Enemy> enemyList = new List<Enemy>();
        Random random = new Random();
        private int enemyCount;
        private List<Projectile> projectileList = new List<Projectile>();
        PowerUpStack powerUpStack = new PowerUpStack();
        private PowerUp powerUp;
        private float timer;
        private float cooldown = 0.25f;
        private float enemySpawnTimer;
        private float enemySpawnCooldown;
        private float powerUpSpawnTimer;
        private float powerUpSpawnCooldown;


        public Level()
        {
            enemyCount = random.Next(20,31); // Enemigos a derrotar para completar el nivel
            enemySpawnCooldown = (float) random.Next(3); // Segundos que pasaran hasta spawnear un enemigo
            powerUpSpawnCooldown = (float) random.Next(15, 20); // Segundos que pasaran hasta spawnear un powerup
            powerUpStack.InitializeStack(); // Inicializar pila PowerUps
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
            PowerUpSpawn();
            PowerUpUpdate();
        }

        public void Render()
        {
            Engine.Draw(background, 0, 0);
            EnemyRender();
            player.Render();
            ProjectileRender();
            PowerUpRender();
        }

        private void ModifyHealth()//cambiar a colisiones
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

        private void ModifyPower()//Cambiar a colisiones
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

        private void BulletSpawn()//Cambiar a propia clase
        {
            if (player.ShootState == true)
            {
                projectileList.Add(new Projectile(player.GetPlayerTransform.Position.X + 25, player.GetPlayerTransform.Position.Y - 20, 1, 500, player.GetPower));
                if (player.GetPower == 3)
                {
                    projectileList.Add(new Projectile(player.GetPlayerTransform.Position.X + 10, player.GetPlayerTransform.Position.Y - 20, 2, 500, player.GetPower));
                    projectileList.Add(new Projectile(player.GetPlayerTransform.Position.X + 40, player.GetPlayerTransform.Position.Y - 20, 3, 500, player.GetPower));
                }
                player.ShootState = false;
            }
            if (enemyList.Count > 0)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i].ShootState == true)
                    {
                        projectileList.Add(new Projectile(enemyList[i].GetEnemyTransform.Position.X + 27, enemyList[i].GetEnemyTransform.Position.Y + 65, -1, 500, 1));
                        enemyList[i].ShootState = false;
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

        private void EnemySpawn()//Cambiar a propia clase
        {
            enemySpawnTimer += Program.deltaTime;
            if (enemySpawnTimer > enemySpawnCooldown)
            {
                enemyList.Add(new Enemy(new Vector2 (random.Next(960), -64), false));
                enemySpawnTimer = 0;
                enemySpawnCooldown = (float)random.Next(3);
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

        private void PowerUpSpawn()//Cambiar a propia clase
        {
            powerUpSpawnTimer += Program.deltaTime;
            if (powerUpSpawnTimer > powerUpSpawnCooldown)
            {
                powerUp = new PowerUp(new Vector2(random.Next(1004), -10), 2);
                powerUpSpawnTimer = 0;
                powerUpSpawnCooldown = (float) random.Next(15, 20);
            }

        }

        private void PowerUpUpdate()
        {
            if (powerUp != null)
            {
                powerUp.Update();
                if (powerUp.InBounds == false)
                {
                    powerUp = null;
                }
            }
        }

        private void PowerUpRender()
        {
            if (powerUp != null)
            {
                powerUp.Render();
            }
        }
    }
}
