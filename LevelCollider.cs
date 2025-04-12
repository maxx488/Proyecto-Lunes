using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class LevelCollider
    {
        private float collisionTimer;
        private float collisionCooldown = 2;
        private Player playerToCheck;
        private List<Enemy> enemiesToCheck;
        private List<Projectile> projectilesToCheck = new List<Projectile>();
        private List<Effect> effectListRef;
        private PowerUp powerUpToCheck;
        private PowerUpStack powerUpStackRef;
        private LevelHud levelHudRef;
        private bool playerDestroyed = false;

        public LevelCollider(Player player, List<Enemy> enemies, List<Projectile> projectiles, PowerUp powerUp, PowerUpStack powerUpStack, LevelHud hud , List<Effect> effects)
        {
            this.playerToCheck = player;
            this.enemiesToCheck = enemies;
            this.projectilesToCheck = projectiles;
            this.powerUpToCheck = powerUp;
            this.powerUpStackRef = powerUpStack;
            this.levelHudRef = hud;
            this.effectListRef = effects;
        }

        public Player SetPlayerToCheck
        {
            set
            {
                playerToCheck = value;
            }
        }

        public PowerUp PowerUpToCheck
        {
            get
            {
                return powerUpToCheck;
            }
            set
            {
                powerUpToCheck = value;
            }
        }

        public bool PlayerDestroyed => playerDestroyed;



        public void Update()
        {
            if (playerDestroyed == true)
            {
                playerDestroyed = false;
            }
            if (playerToCheck == null)
            {
                collisionTimer = 0;
            }
            collisionTimer += Program.deltaTime;
            if (collisionTimer > collisionCooldown)
            {
                if (enemiesToCheck.Count > 0 && playerToCheck != null)
                {
                    for (int i = 0; i < enemiesToCheck.Count; i++) //Colision Enemigo / Player
                    {
                        var collision = new Collider(enemiesToCheck[i].GetEnemyTransform.Position, new Vector2(enemiesToCheck[i].GetEnemyData.SizeX, enemiesToCheck[i].GetEnemyData.SizeY), playerToCheck.GetPlayerTransform.Position, new Vector2(60, 66));
                        if (collision.IsBoxColliding() == true)
                        {
                            collisionTimer = 0;
                            DamagePlayer();
                            break;
                        }
                    }
                    if (projectilesToCheck.Count > 0 && playerToCheck != null)
                    {
                        for (int i = 0; i < projectilesToCheck.Count; i++) //Colision Proyectil Enemigo / Player
                        {
                            var collision = new Collider(new Vector2(projectilesToCheck[i].GetProjectileTransform.Position.X, projectilesToCheck[i].GetProjectileTransform.Position.Y), new Vector2(10, 20), playerToCheck.GetPlayerTransform.Position, new Vector2(60, 66));
                            if (projectilesToCheck[i].Direction == -1)
                            {
                                if (collision.IsBoxColliding() == true)
                                {
                                    collisionTimer = 0;
                                    DamagePlayer();
                                    projectilesToCheck.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (projectilesToCheck.Count > 0)
            {
                for (int i = 0; i < enemiesToCheck.Count; i++) //Colision Enemigo / Proyectil Player
                {
                    for (int j = 0; j < projectilesToCheck.Count; j++)
                    {
                        var collision = new Collider(enemiesToCheck[i].GetEnemyTransform.Position, new Vector2(enemiesToCheck[i].GetEnemyData.SizeX, enemiesToCheck[i].GetEnemyData.SizeY), new Vector2(projectilesToCheck[j].GetProjectileTransform.Position.X, projectilesToCheck[j].GetProjectileTransform.Position.Y), new Vector2(10, 20));
                        if (projectilesToCheck[j].Direction > 0)
                        {
                            if (collision.IsBoxColliding() == true)
                            {
                                if (enemiesToCheck[i].Power > 1)
                                {
                                    enemiesToCheck[i].Power--;
                                    effectListRef.Add(new Effect(new Vector2(projectilesToCheck[j].GetProjectileTransform.Position.X - 20, projectilesToCheck[j].GetProjectileTransform.Position.Y), "assets/animations/bullethits/", 6, 0.02f));
                                }
                                else
                                {
                                    enemiesToCheck[i].Destroyed = true;
                                    effectListRef.Add(new Effect(new Vector2(enemiesToCheck[i].GetEnemyTransform.Position.X, enemiesToCheck[i].GetEnemyTransform.Position.Y + 32), "assets/animations/explosion/", 13, 0.077f));
                                }
                                projectilesToCheck.RemoveAt(j);
                            }
                        }
                    }
                }
            }
            if (powerUpToCheck != null && playerToCheck != null)
            {
                var collision = new Collider(powerUpToCheck.GetPowerUpTransform.Position, new Vector2(20, 10), playerToCheck.GetPlayerTransform.Position, new Vector2(60, 66));
                if (collision.IsBoxColliding() == true)
                {
                    if (powerUpStackRef.FullStack() == false)
                    {
                        powerUpStackRef.Stack(powerUpToCheck.Type);
                        powerUpToCheck = null;
                        playerToCheck.SetPower = powerUpStackRef.Top();
                        levelHudRef.DisplayStackUpdate();
                    }
                }
            }
        }

        private void DamagePlayer()
        {
            if (playerToCheck.GetPower != 1)
            {
                if (powerUpStackRef.EmptyStack() == false)
                {
                    powerUpStackRef.Remove();
                    if (powerUpStackRef.EmptyStack() == false)
                    {
                        playerToCheck.SetPower = powerUpStackRef.Top();
                    }
                    else
                    {
                        playerToCheck.SetPower = 1;
                    }
                }
                levelHudRef.DisplayStackUpdate();
                effectListRef.Add(new Effect(new Vector2(playerToCheck.GetPlayerTransform.Position.X, playerToCheck.GetPlayerTransform.Position.Y), "assets/animations/powerdown/", 8, 0.077f));
            }
            else
            {
                playerDestroyed = true;
                effectListRef.Add(new Effect(new Vector2(playerToCheck.GetPlayerTransform.Position.X, playerToCheck.GetPlayerTransform.Position.Y + 32), "assets/animations/explosion/", 13, 0.077f));
                playerToCheck = null;
            }
        }
    }
}
