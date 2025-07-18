﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Level
    {
        private Renderer renderer;
        private List<GameObject> objectsList = new List<GameObject>();
        private PlayerSpawner playerSpawner;
        private EnemySpawner enemySpawner;
        private EnemyManager enemyManager;
        private EnemyQueue enemyQueue = new EnemyQueue();
        private ProjectileSpawner projectileSpawner;
        private ProjectileManager projectileManager;
        private Random random = new Random();
        private int enemyCount;
        private LevelHud levelHud;
        private PowerUpStack powerUpStack = new PowerUpStack();
        private LevelCollider levelCollider;
        private float powerUpSpawnTimer;
        private float powerUpSpawnCooldown;
        private float respawnTimer;
        private float respawnCooldown = 1.5f;
        private int tries;
        private int enemiesDestroyed = 0;

        public Level(int faction, bool bossLevel, int enemies)
        {
            enemyCount = enemies; // Enemigos a derrotar para completar el nivel
            renderer = new Renderer(new Transform(new Vector2(0, 0), new Vector2(0, 0)), $"assets/level/{faction}.png");
            tries = 3; //Intentos del jugador (dependeria de la dificultad?)
            objectsList.Add(new Player(new Vector2(400, 650))); // Se agrega jugador a lista (se podrian agregar mas a futuro con powerup)
            powerUpStack.InitializeStack(); // Inicializar pila PowerUps
            enemyQueue.InitializeQueue(); // Inicializar cola enemigos
            playerSpawner = new PlayerSpawner(objectsList);
            levelHud = new LevelHud(powerUpStack, enemyQueue, faction, tries, enemiesDestroyed, enemyCount); // Crear HUD (tomar faccion enemiga)
            enemySpawner = new EnemySpawner(objectsList, faction, enemyQueue, levelHud, bossLevel); // faccion correspondiente
            enemyManager = new EnemyManager(objectsList); // Manager de enemigos
            projectileSpawner = new ProjectileSpawner(objectsList); // spawner de proyectiles
            projectileManager = new ProjectileManager(objectsList); // Manager de proyectiles
            powerUpSpawnCooldown = (float) random.Next(15, 20); // Segundos que pasaran hasta spawnear un powerup
            levelCollider = new LevelCollider(objectsList, powerUpStack, levelHud); // manejo de colisiones del nivel
        }

        public int GetTries => tries;

        public bool EnemiesDestroyed
        {
            get
            {
                if (enemiesDestroyed == enemyCount)
                {
                    GameManager.Stats.UnSubStats(enemyManager);
                }
                return enemiesDestroyed == enemyCount;
            }
        }

        public void Input()
        {
            PlayerInput();
        }

        public void Update()
        {
            PlayerSpawn();
            PlayerSpawnerUpdate();
            EnemySpawn();
            EnemyUpdate();
            PlayerUpdate();
            ProyectileSpawn();
            ProjectileUpdate();
            PowerUpSpawn();
            PowerUpUpdate();
            Collisions();
            EffectUpdate();
            HudUpdate();
        }

        public void Render()
        {
            renderer.Render();
            EnemyRender();
            PlayerRender();
            PlayerSpawnerRender();
            ProjectileRender();
            PowerUpRender();
            EffectRender();
            HudRender();
        }

        private void ProyectileSpawn()
        {
            projectileSpawner.Update();
        }

        private void ProjectileUpdate()
        {
            projectileManager.Update();
        }

        private void ProjectileRender()
        {
            projectileManager.Render();
        }

        private void EnemySpawn()
        {
            enemySpawner.Update();
        }

        private void EnemyUpdate()
        {
            enemyManager.Update();
            enemiesDestroyed = enemyManager.EnemiesDestroyed;
            levelHud.Destroyed = enemiesDestroyed;
        }

        private void EnemyRender()
        {
            enemyManager.Render();
        }

        private void PlayerSpawn()
        {
            if (objectsList.OfType<Player>().Count() < 1 && tries > 0)
            {
                respawnTimer += Time.DeltaTime;
                if (respawnTimer > respawnCooldown)
                {
                    Engine.Debug("El Jugador ha muerto.\n");
                    tries--;
                    Engine.Debug($"Intentos restantes: {tries}\n");
                    respawnTimer = 0;
                    if (tries > 0)
                    {
                        levelHud.Tries--;
                        playerSpawner.Spawn();
                    }
                }
            }
        }

        private void PlayerInput()
        {
            if (objectsList.OfType<Player>().Count() > 0 && tries > 0)
            {
                for (int i = 0; i < objectsList.Count; i++)
                {
                    if (objectsList[i] is Player)
                    {
                        Player player = (Player) objectsList[i];
                        player.Input();
                        break;
                    }
                }
            }
        }

        private void PlayerUpdate()
        {
            if (objectsList.OfType<Player>().Count() > 0 && tries > 0)
            {
                for (int i = 0; i < objectsList.Count; i++)
                {
                    if (objectsList[i] is Player)
                    {
                        Player player = (Player) objectsList[i];
                        player.Update();
                        break;
                    }
                }
            }
        }

        private void PlayerRender()
        {
            if (objectsList.OfType<Player>().Count() > 0 && tries > 0)
            {
                for (int i = 0; i < objectsList.Count; i++)
                {
                    if (objectsList[i] is Player)
                    {
                        Player player = (Player) objectsList[i];
                        player.Render();
                        break;
                    }
                }
            }
        }

        private void PlayerSpawnerUpdate()
        {
            playerSpawner.Update();
        }

        private void PlayerSpawnerRender()
        {
            playerSpawner.Render();
        }

        private void PowerUpSpawn()//Cambiar a propia clase
        {
            powerUpSpawnTimer += Time.DeltaTime;
            if (powerUpSpawnTimer > powerUpSpawnCooldown)
            {
                objectsList.Add(new PowerUp(new Vector2(random.Next(1004), -10), random.Next(2, 4)));
                powerUpSpawnTimer = 0;
                powerUpSpawnCooldown = (float) random.Next(15, 20);
            }

        }

        private void PowerUpUpdate()
        {
            if (objectsList.OfType<PowerUp>().Count() > 0)
            {
                for (int i = 0; i < objectsList.Count; i++)
                {
                    if (objectsList[i] is PowerUp)
                    {
                        PowerUp powerUp = (PowerUp)objectsList[i];
                        powerUp.Update();
                    }
                }
                for (int i = 0; i < objectsList.Count; i++)
                {
                    if (objectsList[i] is PowerUp)
                    {
                        PowerUp powerUp = (PowerUp)objectsList[i];
                        if (powerUp.InBounds == false)
                        {
                            objectsList.RemoveAt(i);
                        }
                    }
                }
            }
        }

        private void PowerUpRender()
        {
            if (objectsList.OfType<PowerUp>().Count() > 0)
            {
                for (int i = 0; i < objectsList.Count; i++)
                {
                    if (objectsList[i] is PowerUp)
                    {
                        PowerUp powerUp = (PowerUp) objectsList[i];
                        powerUp.Render();
                    }
                }
            }
        }

        private void EffectUpdate()
        {
            if (objectsList.OfType<Effect>().Count() > 0)
            {
                for (int i = 0; i < objectsList.Count; i++)
                {
                    if (objectsList[i] is Effect)
                    {
                        Effect effect = (Effect) objectsList[i];
                        effect.Update();
                        if (effect.GetAnimationController.Finished == true)
                        {
                            effect.Disable();
                            objectsList.RemoveAt(i);
                        }
                    }
                }
            }
        }

        private void EffectRender()
        {
            if (objectsList.OfType<Effect>().Count() > 0)
            {
                for (int i = 0; i < objectsList.Count; i++)
                {
                    if (objectsList[i] is Effect)
                    {
                        Effect effect = (Effect) objectsList[i];
                        effect.Render();
                    }
                }
            }
        }

        private void Collisions()
        {
            levelCollider.Update();
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
