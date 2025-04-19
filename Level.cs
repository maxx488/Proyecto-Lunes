using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Level
    {
        private Image background;
        private List<Player> playerList = new List<Player>();
        private EnemySpawner enemySpawner;
        private EnemyManager enemyManager;
        private EnemyQueue enemyQueue = new EnemyQueue();
        private ProyectileSpawner proyectileSpawner;
        private ProyectileManager proyectileManager;
        private Random random = new Random();
        private int enemyCount;
        private List<Effect> effectList = new List<Effect>();
        private LevelHud levelHud;
        private PowerUpStack powerUpStack = new PowerUpStack();
        private List<PowerUp> powerUpList = new List<PowerUp>();
        private LevelCollider levelCollider;
        private float powerUpSpawnTimer;
        private float powerUpSpawnCooldown;
        private float respawnTimer;
        private float respawnCooldown = 1.5f;
        private int tries;
        private int enemiesDestroyed = 0;

        public Level(int faction)
        {
            tries = 3; //Intentos del jugador (dependeria de la dificultad?)
            background = Engine.LoadImage($"assets/level/{faction}.png"); // cambiar forma de obtener fondo y/o datos de nivel en el futuro
            playerList.Add(new Player(new Vector2(400, 650))); // Se agrega jugador a lista (se podrian agregar mas a futuro con powerup)
            enemyCount = random.Next(20,51); // Enemigos a derrotar para completar el nivel (mas adelante atarlo a dificultad en vez de random)
            powerUpStack.InitializeStack(); // Inicializar pila PowerUps
            enemyQueue.InitializeQueue(); // Inicializar cola enemigos
            levelHud = new LevelHud(powerUpStack, enemyQueue, faction, tries); // Crear HUD (tomar faccion enemiga)
            enemySpawner = new EnemySpawner(faction, enemyQueue, levelHud); // faccion correspondiente
            enemyManager = new EnemyManager(enemySpawner.EnemyList); // Manager de enemigos
            proyectileSpawner = new ProyectileSpawner(playerList, enemySpawner.EnemyList); // spawner de proyectiles
            proyectileManager = new ProyectileManager(proyectileSpawner.ProjectileList); // Manager de proyectiles
            powerUpSpawnCooldown = (float) random.Next(15, 20); // Segundos que pasaran hasta spawnear un powerup
            levelCollider = new LevelCollider(playerList, enemySpawner.EnemyList, proyectileSpawner.ProjectileList, powerUpList, powerUpStack, levelHud, effectList); // manejo de colisiones del nivel
        }

        public int GetTries => tries;

        public bool EnemiesDestroyed
        {
            get
            {
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
            Engine.Draw(background, 0, 0);
            EnemyRender();
            PlayerRender();
            ProjectileRender();
            PowerUpRender();
            EffectRender();
            HudRender();
        }

        private void ProyectileSpawn()
        {
            proyectileSpawner.Update();
        }

        private void ProjectileUpdate()
        {
            proyectileManager.Update();
        }

        private void ProjectileRender()
        {
            proyectileManager.Render();
        }

        private void EnemySpawn()
        {
            enemySpawner.Update();
        }

        private void EnemyUpdate()
        {
            enemyManager.Update();
            enemiesDestroyed = enemyManager.EnemiesDestroyed;
        }

        private void EnemyRender()
        {
            enemyManager.Render();
        }

        private void PlayerSpawn()//Cambiar a propia clase
        {
            if (playerList.Count < 1 && tries > 0)
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
                        playerList.Add(new Player(new Vector2(400, 650)));
                    }
                }
            }
        }

        private void PlayerInput()
        {
            if (playerList.Count > 0 && tries > 0)
            {
                playerList[0].Input();
            }
        }

        private void PlayerUpdate()
        {
            if (playerList.Count > 0 && tries > 0)
            {
                playerList[0].Update();
            }
        }

        private void PlayerRender()
        {
            if (playerList.Count > 0 && tries > 0)
            {
                playerList[0].Render();
            }
        }

        private void PowerUpSpawn()//Cambiar a propia clase
        {
            powerUpSpawnTimer += Time.DeltaTime;
            if (powerUpSpawnTimer > powerUpSpawnCooldown)
            {
                powerUpList.Add(new PowerUp(new Vector2(random.Next(1004), -10), random.Next(2, 4)));
                powerUpSpawnTimer = 0;
                powerUpSpawnCooldown = (float) random.Next(15, 20);
            }

        }

        private void PowerUpUpdate()
        {
            if (powerUpList.Count > 0)
            {
                for (int i = 0; i < powerUpList.Count; i++)
                {
                    powerUpList[i].Update();
                }
                for (int i = 0; i < powerUpList.Count; i++)
                {
                    if (powerUpList[i].InBounds == false)
                    {
                        powerUpList.RemoveAt(i);
                    }
                }
            }
        }

        private void PowerUpRender()
        {
            if (powerUpList.Count > 0)
            {
                for (int i = 0; i < powerUpList.Count; i++)
                {
                    powerUpList[i].Render();
                }
            }
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
