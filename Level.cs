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
        private ProyectileSpawner proyectileSpawner;
        private ProyectileManager proyectileManager;
        private Random random = new Random();
        private int enemyCount;
        private List<Effect> effectList = new List<Effect>();
        private LevelHud levelHud;
        private PowerUpStack powerUpStack = new PowerUpStack();
        private PowerUp powerUp;
        private LevelCollider levelCollider;
        private float powerUpSpawnTimer;
        private float powerUpSpawnCooldown;
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
            proyectileSpawner = new ProyectileSpawner(player, enemySpawner.EnemyList); // spawner de proyectiles
            proyectileManager = new ProyectileManager(proyectileSpawner.ProjectileList);
            powerUpSpawnCooldown = (float) random.Next(15, 20); // Segundos que pasaran hasta spawnear un powerup
            powerUpStack.InitializeStack(); // Inicializar pila PowerUps
            levelHud = new LevelHud(powerUpStack); // Crear HUD
            levelCollider = new LevelCollider(player, enemySpawner.EnemyList, proyectileSpawner.ProjectileList, powerUp, powerUpStack, levelHud, effectList);
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
            ProyectileSpawn();
            ProjectileUpdate();
            PowerUpSpawn();
            PowerUpUpdate();
            Collisions();
            PowerUpPicked();
            DestroyedPlayer();
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
                    player = new Player(new Vector2(400, 650));
                    proyectileSpawner.SetPlayerToCheck = player;
                    levelCollider.SetPlayerToCheck = player;
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
                levelCollider.PowerUpToCheck = powerUp;
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

        private void DestroyedPlayer()
        {
            if (levelCollider.PlayerDestroyed == true)
            {
                Engine.Debug("El Jugador ha muerto.\n");
                player = null;
                tries--;
                Engine.Debug($"Intentos restantes: {tries}\n");
            }
        }

        private void PowerUpPicked()
        {
            if (levelCollider.PowerUpToCheck == null)
            {
                powerUp = null;
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
