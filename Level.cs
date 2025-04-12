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
        private Player player;
        private EnemySpawner enemySpawner;
        private EnemyManager enemyManager;
        private Random random = new Random();
        private int enemyCount;
        private List<Projectile> projectileList = new List<Projectile>();
        private List<Effect> effectList = new List<Effect>();
        private LevelHud levelHud;
        private PowerUpStack powerUpStack = new PowerUpStack();
        private PowerUp powerUp;
        private float powerUpSpawnTimer;
        private float powerUpSpawnCooldown;
        private float collisionTimer;
        private float collisionCooldown = 2;
        private float respawnTimer;
        private float respawnCooldown = 1.5f;
        private int tries;


        public Level()
        {
            tries = 3; //Respawns del jugador (dependeria de la dificultad)
            player = new Player(new Vector2(400, 650));
            enemyCount = random.Next(20,31); // Enemigos a derrotar para completar el nivel (sin utilizar todavia)
            enemySpawner = new EnemySpawner(1); // faccion correspondiente (por ahora 1)
            enemyManager = new EnemyManager(enemySpawner.EnemyList);
            powerUpSpawnCooldown = (float) random.Next(15, 20); // Segundos que pasaran hasta spawnear un powerup
            powerUpStack.InitializeStack(); // Inicializar pila PowerUps
            levelHud = new LevelHud(powerUpStack); // Crear HUD
        }

        public int GetTries => tries;

        public int GetPlayerPower => player.GetPower;

        public void Input()
        {
            PlayerInput();
        }

        public void Update()
        {
            PlayerSpawn();
            EnemySpawn();
            EnemyUpdate();
            PlayerUpdate();
            BulletSpawn();
            ProjectileUpdate();
            PowerUpSpawn();
            PowerUpUpdate();
            Collisions();
            EffectUpdate();
            HudUpdate();
        }

        public void Render()
        {
            Engine.Draw(background, 0, 0);
            EnemyRender();
            PlayerRender();
            ProjectileRender();
            PowerUpRender();
            EffectRender();
            HudRender();
        }

        private void BulletSpawn()//Cambiar a propia clase
        {
            if (player != null)
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
            }
            if (enemySpawner.EnemyList.Count > 0)
            {
                for (int i = 0; i < enemySpawner.EnemyList.Count; i++)
                {
                    if (enemySpawner.EnemyList[i].ShootState == true)
                    {
                        for(int j = 0; j < enemySpawner.EnemyList[i].GetEnemyData.ShootPosX.Length; j++)
                        {
                            projectileList.Add(new Projectile(new Vector2(enemySpawner.EnemyList[i].GetEnemyTransform.Position.X + enemySpawner.EnemyList[i].GetEnemyData.ShootPosX[j], enemySpawner.EnemyList[i].GetEnemyTransform.Position.Y + enemySpawner.EnemyList[i].GetEnemyData.ShootPosY), -1, 500, 1));
                        }
                        enemySpawner.EnemyList[i].ShootState = false;
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

        private void EnemySpawn()
        {
            enemySpawner.Update();
        }

        private void EnemyUpdate()
        {
            enemyManager.Update();
        }

        private void EnemyRender()
        {
            enemyManager.Render();
        }

        private void PlayerSpawn()//Cambiar a propia clase
        {
            if (player == null && tries > 0)
            {
                respawnTimer += Program.deltaTime;
                if (respawnTimer > respawnCooldown)
                {
                    respawnTimer = 0;
                    collisionTimer = 0;
                    player = new Player(new Vector2(400, 650));
                }
            }
        }

        private void PlayerInput()//Cambiar a propia clase
        {
            if (player != null && tries > 0)
            {
                player.Input();
            }
        }

        private void PlayerUpdate()//Cambiar a propia clase
        {
            if (player != null && tries > 0)
            {
                player.Update();
            }
        }

        private void PlayerRender()//Cambiar a propia clase
        {
            if (player != null && tries > 0)
            {
                player.Render();
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
                    }
                    else
                    {
                        player.SetPower = 1;
                    }
                }
                levelHud.DisplayStackUpdate();
                EffectSpawn(new Vector2(player.GetPlayerTransform.Position.X, player.GetPlayerTransform.Position.Y), "assets/animations/powerdown/", 8, 0.077f);
            }
            else
            {
                Engine.Debug("El Jugador ha muerto.\n");
                EffectSpawn(new Vector2(player.GetPlayerTransform.Position.X, player.GetPlayerTransform.Position.Y + 32), "assets/animations/explosion/", 13, 0.077f);
                player = null;
                tries--;
                Engine.Debug($"Intentos restantes: {tries}\n");
            }
        }

        private void Collisions()
        {
            collisionTimer += Program.deltaTime;
            if (collisionTimer > collisionCooldown)
            {
                if (enemySpawner.EnemyList.Count > 0 && player != null)
                {
                    for (int i = 0; i < enemySpawner.EnemyList.Count; i++) //Colision Enemigo / Player
                    {
                        var collision = new Collider(enemySpawner.EnemyList[i].GetEnemyTransform.Position, new Vector2(enemySpawner.EnemyList[i].GetEnemyData.SizeX, enemySpawner.EnemyList[i].GetEnemyData.SizeY), player.GetPlayerTransform.Position, new Vector2(60, 66));
                        if (collision.IsBoxColliding() == true)
                        {
                            collisionTimer = 0;
                            DamagePlayer();
                            break;
                        }
                    }
                    if (projectileList.Count > 0 && player != null)
                    {
                        for (int i = 0; i < projectileList.Count; i++) //Colision Proyectil Enemigo / Player
                        {
                            var collision = new Collider(new Vector2(projectileList[i].GetProjectileTransform.Position.X, projectileList[i].GetProjectileTransform.Position.Y), new Vector2(10, 20), player.GetPlayerTransform.Position, new Vector2(60, 66));
                            if (projectileList[i].Direction == -1)
                            {
                                if (collision.IsBoxColliding() == true)
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
                for (int i = 0; i < enemySpawner.EnemyList.Count; i++) //Colision Enemigo / Proyectil Player
                {
                    for (int j = 0; j < projectileList.Count; j++)
                    {
                        var collision = new Collider(enemySpawner.EnemyList[i].GetEnemyTransform.Position, new Vector2(enemySpawner.EnemyList[i].GetEnemyData.SizeX, enemySpawner.EnemyList[i].GetEnemyData.SizeY), new Vector2(projectileList[j].GetProjectileTransform.Position.X, projectileList[j].GetProjectileTransform.Position.Y), new Vector2(10, 20));
                        if (projectileList[j].Direction > 0)
                        {
                            if (collision.IsBoxColliding() == true)
                            {
                                if (enemySpawner.EnemyList[i].Power  > 1)
                                {
                                    enemySpawner.EnemyList[i].Power--;
                                    EffectSpawn(new Vector2(projectileList[j].GetProjectileTransform.Position.X - 20, projectileList[j].GetProjectileTransform.Position.Y), "assets/animations/bullethits/", 6, 0.02f);
                                }
                                else
                                {
                                    enemySpawner.EnemyList[i].Destroyed = true;
                                    EffectSpawn(new Vector2(enemySpawner.EnemyList[i].GetEnemyTransform.Position.X, enemySpawner.EnemyList[i].GetEnemyTransform.Position.Y + 32), "assets/animations/explosion/", 13, 0.077f);
                                }
                                projectileList.RemoveAt(j);
                            }
                        }
                    }
                }
            }
            if (powerUp != null && player != null)
            {
                var collision = new Collider(powerUp.GetPowerUpTransform.Position, new Vector2(20, 10), player.GetPlayerTransform.Position, new Vector2(60, 66));
                if (collision.IsBoxColliding() == true)
                {
                    if (powerUpStack.FullStack() == false)
                    {
                        powerUpStack.Stack(powerUp.Type);
                        powerUp = null;
                        player.SetPower = powerUpStack.Top();
                        levelHud.DisplayStackUpdate();
                    }
                }
            }
        }

        private void HudUpdate()
        {
            levelHud.Update();
        }

        private void HudRender()
        {
            levelHud.Render();
        }
    }
}
