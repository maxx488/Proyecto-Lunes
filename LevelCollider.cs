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
        private List<Player> playerToCheck;
        private List<Enemy> enemiesToCheck;
        private List<Projectile> projectilesToCheck = new List<Projectile>();
        private List<Effect> effectListRef;
        private List<PowerUp> powerUpToCheck;
        private PowerUpStack powerUpStackRef;
        private LevelHud levelHudRef;

        public LevelCollider(List<Player> player, List<Enemy> enemies, List<Projectile> projectiles, List<PowerUp> powerUp, PowerUpStack powerUpStack, LevelHud hud , List<Effect> effects)
        {
            this.playerToCheck = player;
            this.enemiesToCheck = enemies;
            this.projectilesToCheck = projectiles;
            this.powerUpToCheck = powerUp;
            this.powerUpStackRef = powerUpStack;
            this.levelHudRef = hud;
            this.effectListRef = effects;
        }

        public void Update()
        {
            if (playerToCheck.Count < 1)
            {
                collisionTimer = 0;
            }
            collisionTimer += Program.deltaTime;
            if (collisionTimer > collisionCooldown)
            {
                if (enemiesToCheck.Count > 0 && playerToCheck.Count > 0)
                {
                    for (int i = 0; i < enemiesToCheck.Count; i++) //Colision Enemigo / Player
                    {
                        var collision = new Collider(enemiesToCheck[i].GetEnemyTransform.Position, new Vector2(enemiesToCheck[i].GetEnemyData.SizeX, enemiesToCheck[i].GetEnemyData.SizeY), playerToCheck[0].GetPlayerTransform.Position, new Vector2(60, 66));
                        if (collision.IsBoxColliding() == true)
                        {
                            collisionTimer = 0;
                            DamagePlayer();
                            break;
                        }
                    }
                    if (projectilesToCheck.Count > 0 && playerToCheck.Count > 0)
                    {
                        for (int i = 0; i < projectilesToCheck.Count; i++) //Colision Proyectil Enemigo / Player
                        {
                            var collision = new Collider(new Vector2(projectilesToCheck[i].GetProjectileTransform.Position.X, projectilesToCheck[i].GetProjectileTransform.Position.Y), new Vector2(10, 20), playerToCheck[0].GetPlayerTransform.Position, new Vector2(60, 66));
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
                                enemiesToCheck[i].PowerController.DamageEnemy();
                                if (enemiesToCheck[i].PowerController.Power > 0)
                                {
                                    effectListRef.Add(new Effect(new Vector2(projectilesToCheck[j].GetProjectileTransform.Position.X - 20, projectilesToCheck[j].GetProjectileTransform.Position.Y), "assets/animations/bullethits/", 6, 0.02f));
                                }
                                else
                                {
                                    effectListRef.Add(new Effect(new Vector2(enemiesToCheck[i].GetEnemyTransform.Position.X, enemiesToCheck[i].GetEnemyTransform.Position.Y + 32), "assets/animations/explosion/", 13, 0.077f));
                                }
                                projectilesToCheck.RemoveAt(j);
                            }
                        }
                    }
                }
            }
            if (powerUpToCheck.Count > 0 && playerToCheck.Count > 0)
            {
                var collision = new Collider(powerUpToCheck[0].GetPowerUpTransform.Position, new Vector2(20, 10), playerToCheck[0].GetPlayerTransform.Position, new Vector2(60, 66));
                if (collision.IsBoxColliding() == true)
                {
                    if (powerUpStackRef.FullStack() == false)
                    {
                        powerUpStackRef.Stack(powerUpToCheck[0].Type);
                        powerUpToCheck.RemoveAt(0);
                        playerToCheck[0].SetPower = powerUpStackRef.Top();
                        levelHudRef.DisplayStackUpdate();
                    }
                }
            }
        }

        private void DamagePlayer()
        {
            if (playerToCheck[0].GetPower != 1)
            {
                if (powerUpStackRef.EmptyStack() == false)
                {
                    powerUpStackRef.Remove();
                    if (powerUpStackRef.EmptyStack() == false)
                    {
                        playerToCheck[0].SetPower = powerUpStackRef.Top();
                    }
                    else
                    {
                        playerToCheck[0].SetPower = 1;
                    }
                }
                levelHudRef.DisplayStackUpdate();
                effectListRef.Add(new Effect(new Vector2(playerToCheck[0].GetPlayerTransform.Position.X, playerToCheck[0].GetPlayerTransform.Position.Y), "assets/animations/powerdown/", 8, 0.077f));
            }
            else
            {
                effectListRef.Add(new Effect(new Vector2(playerToCheck[0].GetPlayerTransform.Position.X, playerToCheck[0].GetPlayerTransform.Position.Y + 32), "assets/animations/explosion/", 13, 0.077f));
                playerToCheck.RemoveAt(0);
            }
        }
    }
}
