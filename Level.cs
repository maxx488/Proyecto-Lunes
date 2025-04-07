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
        private Player player = new Player(new Vector2 (400, 650));
        private List<Enemy> enemyList = new List<Enemy>();
        private Random random = new Random();
        private int enemyCount;
        private List<Projectile> projectileList = new List<Projectile>();
        private List<Effect> effectList = new List<Effect>();
        private PowerUpStack powerUpStack = new PowerUpStack();
        private PowerUp powerUp;
        private float timer;
        private float cooldown = 0.25f;
        private float enemySpawnTimer;
        private float enemySpawnCooldown;
        private float powerUpSpawnTimer;
        private float powerUpSpawnCooldown;
        private float collisionTimer;
        private float collisionCooldown = 2;


        public Level()
        {
            enemyCount = random.Next(20,31); // Enemigos a derrotar para completar el nivel (sin utilizar todavia)
            enemySpawnCooldown = (float) random.Next(3); // Segundos que pasaran hasta spawnear un enemigo
            powerUpSpawnCooldown = (float) random.Next(15, 20); // Segundos que pasaran hasta spawnear un powerup
            powerUpStack.InitializeStack(); // Inicializar pila PowerUps
        }

        public int GetPlayerPower => player.GetPower;

        public void Input()
        {
            player.Input();
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
            Collisions();
            EffectUpdate();
        }

        public void Render()
        {
            Engine.Draw(background, 0, 0);
            EnemyRender();
            player.Render();
            ProjectileRender();
            PowerUpRender();
            EffectRender();
        }

        private void BulletSpawn()//Cambiar a propia clase
        {
            if (player.ShootState == true)
            {
                projectileList.Add(new Projectile(new Vector2(player.GetPlayerTransform.Position.X + 25, player.GetPlayerTransform.Position.Y - 20), 1, 500, player.GetPower));
                if (player.GetPower == 3)
                {
                    projectileList.Add(new Projectile(new Vector2(player.GetPlayerTransform.Position.X + 10, player.GetPlayerTransform.Position.Y - 20), 2, 500, player.GetPower));
                    projectileList.Add(new Projectile(new Vector2(player.GetPlayerTransform.Position.X + 40, player.GetPlayerTransform.Position.Y - 20), 3, 500, player.GetPower));
                }
                player.ShootState = false;
            }
            if (enemyList.Count > 0)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i].ShootState == true)
                    {
                        projectileList.Add(new Projectile(new Vector2(enemyList[i].GetEnemyTransform.Position.X + 27, enemyList[i].GetEnemyTransform.Position.Y + 65), -1, 500, 1));
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
                for (int i = 0; i < projectileList.Count; i++)
                {
                    projectileList[i].Render();
                }
            }
        }

        private void EnemySpawn()//Cambiar a propia clase
        {
            enemySpawnTimer += Program.deltaTime;
            if (enemySpawnTimer > enemySpawnCooldown)
            {
                enemyList.Add(new Enemy(new Vector2(random.Next(960), -64), false));
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
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i].Destroyed == true)
                    {
                        enemyList.RemoveAt(i);
                    }
                }
            }
        }

        private void EnemyRender()
        {
            if (enemyList.Count > 0)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    enemyList[i].Render();
                }
            }
        }

        private void PowerUpSpawn()//Cambiar a propia clase
        {
            powerUpSpawnTimer += Program.deltaTime;
            if (powerUpSpawnTimer > powerUpSpawnCooldown)
            {
                powerUp = new PowerUp(new Vector2(random.Next(1004), -10), random.Next(2, 4));
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

        private void EffectSpawn(Vector2 location, string path, int maxIndex, float animCooldown)//Cambiar a propia clase
        {
            effectList.Add(new Effect(location, path, maxIndex, animCooldown));
        }

        private void EffectUpdate()
        {
            if (effectList.Count > 0)
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    effectList[i].Update();
                    if (effectList[i].GetAnimationController.Finished == true)
                    {
                        effectList.RemoveAt(i);
                    }
                }
            }
        }

        private void EffectRender()
        {
            if (effectList.Count > 0)
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    effectList[i].Render();
                }
            }
        }

        private void DamagePlayer()
        {
            if (player.GetPower != 1)
            {
                if (powerUpStack.EmptyStack() == false)
                {
                    powerUpStack.Remove();
                    if (powerUpStack.EmptyStack() == false)
                    {
                        player.SetPower = powerUpStack.Top();
                        powerUpStack.ShowStack();
                    }
                    else
                    {
                        player.SetPower = 1;
                    }
                }
            }
            else
            {
                player.SetPower = 0;
            }
        }

        private void Collisions()
        {
            collisionTimer += Program.deltaTime;
            if (collisionTimer > collisionCooldown)
            {
                if (enemyList.Count > 0)
                {
                    for (int i = 0; i < enemyList.Count; i++) //Colision Enemigo / Player
                    {
                        var colision = new Collider(enemyList[i].GetEnemyTransform.Position, new Vector2(64, 64), player.GetPlayerTransform.Position, new Vector2(60, 66));
                        if (colision.IsBoxColliding() == true)
                        {
                            collisionTimer = 0;
                            DamagePlayer();
                            break;
                        }
                    }
                    if (projectileList.Count > 0)
                    {
                        for (int i = 0; i < projectileList.Count; i++) //Colision Proyectil Enemigo / Player
                        {
                            var colision = new Collider(new Vector2(projectileList[i].GetProjectileTransform.Position.X, projectileList[i].GetProjectileTransform.Position.Y), new Vector2(10, 20), player.GetPlayerTransform.Position, new Vector2(60, 66));
                            if (projectileList[i].Direction == -1)
                            {
                                if (colision.IsBoxColliding() == true)
                                {
                                    collisionTimer = 0;
                                    DamagePlayer();
                                    projectileList.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (projectileList.Count > 0)
            {
                for (int i = 0; i < enemyList.Count; i++) //Colision Enemigo / Proyectil Player
                {
                    for (int j = 0; j < projectileList.Count; j++)
                    {
                        var colision = new Collider(enemyList[i].GetEnemyTransform.Position, new Vector2(64, 64), new Vector2(projectileList[j].GetProjectileTransform.Position.X, projectileList[j].GetProjectileTransform.Position.Y), new Vector2(10, 20));
                        if (projectileList[j].Direction > 0)
                        {
                            if (colision.IsBoxColliding() == true)
                            {
                                enemyList[i].Destroyed = true;
                                EffectSpawn(new Vector2(enemyList[i].GetEnemyTransform.Position.X, enemyList[i].GetEnemyTransform.Position.Y + 32), "assets/animations/explosion/", 13, 0.077f);
                                projectileList.RemoveAt(j);
                            }
                        }
                    }
                }
            }
            if (powerUp != null)
            {
                var colision = new Collider(powerUp.GetPowerUpTransform.Position, new Vector2(20, 10), player.GetPlayerTransform.Position, new Vector2(60, 66));
                if (colision.IsBoxColliding() == true)
                {
                    if (powerUpStack.FullStack() == false)
                    {
                        powerUpStack.Stack(powerUp.Type);
                        powerUp = null;
                        player.SetPower = powerUpStack.Top();
                        powerUpStack.ShowStack();
                    }
                }
            }
        }
    }
}
