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
        private List<GameObject> objectsListToCheck;
        private PowerUpStack powerUpStackRef;
        private LevelHud levelHudRef;

        public LevelCollider(List<GameObject> objects, PowerUpStack powerUpStack, LevelHud hud)
        {
            this.objectsListToCheck = objects;
            this.powerUpStackRef = powerUpStack;
            this.levelHudRef = hud;
        }

        public void Update()
        {
            if (objectsListToCheck.OfType<Player>().Count() < 1)
            {
                collisionTimer = 0;
            }
            collisionTimer += Time.DeltaTime;
            if (collisionTimer > collisionCooldown)
            {
                if (objectsListToCheck.OfType<Enemy>().Count() > 0 && objectsListToCheck.OfType<Player>().Count() > 0)
                {
                    for (int i = 0; i < objectsListToCheck.Count; i++) //Colision Enemigo / Player
                    {
                        if (objectsListToCheck[i] is Enemy)
                        {
                            Enemy enemy = (Enemy) objectsListToCheck[i];
                            for (int j = 0; j < objectsListToCheck.Count; j++)
                            {
                                if (objectsListToCheck[j] is Player)
                                {
                                    var collision = new Collider(enemy.GetTransform.Position, new Vector2(enemy.GetEnemyData.SizeX, enemy.GetEnemyData.SizeY), objectsListToCheck[j].GetTransform.Position, new Vector2(60, 66));
                                    if (collision.IsBoxColliding() == true)
                                    {
                                        collisionTimer = 0;
                                        DamagePlayer(j);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (objectsListToCheck.OfType<Projectile>().Count() > 0 && objectsListToCheck.OfType<Player>().Count() > 0)
                    {
                        for (int i = 0; i < objectsListToCheck.Count; i++) //Colision Proyectil Enemigo / Player
                        {
                            if (objectsListToCheck[i] is Projectile)
                            {
                                Projectile projectile = (Projectile) objectsListToCheck[i];
                                if (projectile.Direction == -1)
                                {
                                    for (int j = 0; j < objectsListToCheck.Count; j++)
                                    {
                                        if (objectsListToCheck[j] is Player)
                                        {
                                            var collision = new Collider(projectile.GetTransform.Position, new Vector2(10, 20), objectsListToCheck[j].GetTransform.Position, new Vector2(60, 66));
                                            if (collision.IsBoxColliding() == true)
                                            {
                                                collisionTimer = 0;
                                                DamagePlayer(j);
                                                objectsListToCheck.RemoveAt(i);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (objectsListToCheck.OfType<Projectile>().Count() > 0)
            {
                for (int i = 0; i < objectsListToCheck.Count; i++) //Colision Enemigo / Proyectil Player
                {
                    if (objectsListToCheck[i] is Enemy)
                    {
                        Enemy enemy = (Enemy) objectsListToCheck[i];
                        for (int j = 0; j < objectsListToCheck.Count; j++)
                        {
                            if (objectsListToCheck[j] is Projectile)
                            {
                                Projectile projectile = (Projectile) objectsListToCheck[j];
                                if (projectile.Direction > 0)
                                {
                                    var collision = new Collider(enemy.GetTransform.Position, new Vector2(enemy.GetEnemyData.SizeX, enemy.GetEnemyData.SizeY), projectile.GetTransform.Position, new Vector2(10, 20));
                                    if (collision.IsBoxColliding() == true)
                                    {
                                        enemy.PowerController.DamageEnemy();
                                        if (enemy.PowerController.Power > 0)
                                        {
                                            objectsListToCheck.Add(new Effect(new Vector2(projectile.GetTransform.Position.X - 20, projectile.GetTransform.Position.Y), "assets/animations/bullethits/", 6, 0.02f));
                                        }
                                        else
                                        {
                                            objectsListToCheck.Add(new Effect(new Vector2(enemy.GetTransform.Position.X, enemy.GetTransform.Position.Y + 32), "assets/animations/explosion/", 13, 0.077f));
                                        }
                                        objectsListToCheck.RemoveAt(j);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (objectsListToCheck.OfType<PowerUp>().Count() > 0 && objectsListToCheck.OfType<Player>().Count() > 0)
            {
                for (int i = 0; i < objectsListToCheck.Count; i++)
                {
                    if (objectsListToCheck[i] is PowerUp)
                    {
                        PowerUp powerUp = (PowerUp) objectsListToCheck[i];
                        for (int j = 0; j < objectsListToCheck.Count; j++)
                        {
                            if (objectsListToCheck[j] is Player)
                            {
                                Player player = (Player) objectsListToCheck[j];
                                var collision = new Collider(powerUp.GetTransform.Position, new Vector2(20, 10), player.GetTransform.Position, new Vector2(60, 66));
                                if (collision.IsBoxColliding() == true)
                                {
                                    if (powerUpStackRef.FullStack() == false)
                                    {
                                        powerUpStackRef.Stack(powerUp.Type);
                                        player.SetPower = powerUpStackRef.Top();
                                        objectsListToCheck.RemoveAt(i);
                                        levelHudRef.DisplayStackUpdate();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DamagePlayer(int x)
        {
            Player player = (Player) objectsListToCheck[x];
            if (player.GetPower != 1)
            {
                if (powerUpStackRef.EmptyStack() == false)
                {
                    powerUpStackRef.Remove();
                    if (powerUpStackRef.EmptyStack() == false)
                    {
                        player.SetPower = powerUpStackRef.Top();
                    }
                    else
                    {
                        player.SetPower = 1;
                    }
                }
                levelHudRef.DisplayStackUpdate();
                player.Damaged = true;
                objectsListToCheck.Add(new Effect(new Vector2(player.GetTransform.Position.X, player.GetTransform.Position.Y), "assets/animations/powerdown/", 8, 0.077f));
            }
            else
            {
                objectsListToCheck.Add(new Effect(new Vector2(player.GetTransform.Position.X, player.GetTransform.Position.Y + 32), "assets/animations/explosion/", 13, 0.077f));
                objectsListToCheck.RemoveAt(x);
            }
        }
    }
}
