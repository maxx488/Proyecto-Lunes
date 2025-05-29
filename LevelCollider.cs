using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class LevelCollider
    {
        public delegate void soundDelegate(string type, string state);
        private event soundDelegate onCollisionSound;
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
            GameManager.GetSoundManager.SubCollisions(this);
        }

        public event soundDelegate OnCollisionSound
        {
            add { onCollisionSound += value; }
            remove { onCollisionSound -= value; }
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
                                    Player player = (Player) objectsListToCheck[j];
                                    if (CheckCollisions(enemy, player) == true)
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
                        int previousSize = objectsListToCheck.Count;
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
                                            Player player = (Player) objectsListToCheck[j];
                                            if (CheckCollisions(projectile, player) == true)
                                            {
                                                collisionTimer = 0;
                                                projectile.Disable();
                                                DamagePlayer(j);
                                                int currentSize = objectsListToCheck.Count;
                                                if (currentSize == previousSize) //quiere decir que el jugador fue removido y se añadió un objeto efecto.
                                                {
                                                    if (j < i)
                                                    {
                                                        objectsListToCheck.RemoveAt(i - 1); //si el proyectil se encontraba mas adelante en la lista, entonces se corrió una posición hacia la "izquierda".
                                                    }
                                                    else
                                                    {
                                                        objectsListToCheck.RemoveAt(i);
                                                    }
                                                }
                                                else
                                                {
                                                    objectsListToCheck.RemoveAt(i);
                                                }
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
                                    if (CheckCollisions(enemy, projectile) == true)
                                    {
                                        enemy.GetDamage();
                                        if (enemy.PowerController.Power > 0)
                                        {
                                            objectsListToCheck.Add(new Effect(new Vector2(projectile.GetTransform.Position.X - 20, projectile.GetTransform.Position.Y), "assets/animations/bullethits/", 6, 0.02f));
                                            onCollisionSound.Invoke($"{enemy.GetType().Name}", "hit");
                                        }
                                        else
                                        {
                                            objectsListToCheck.Add(new Effect(new Vector2(enemy.GetTransform.Position.X, enemy.GetTransform.Position.Y + 32), "assets/animations/explosion/", 13, 0.077f));
                                            onCollisionSound.Invoke($"{enemy.GetType().Name}", "");
                                        }
                                        projectile.Disable();
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
                                if (CheckCollisions(powerUp, player) == true)
                                {
                                    if (powerUpStackRef.FullStack() == false)
                                    {
                                        powerUpStackRef.Stack(powerUp.Type);
                                        player.SetPower = powerUpStackRef.Top();
                                        objectsListToCheck.RemoveAt(i);
                                        levelHudRef.DisplayStackUpdate();
                                        onCollisionSound.Invoke($"{powerUp.GetType().Name}", "");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool CheckCollisions(GameObject object1, GameObject object2)
        {
            var collision = new Collider(object1.GetTransform.Position, object1.GetTransform.Scale, object2.GetTransform.Position, object2.GetTransform.Scale);
            return collision.IsBoxColliding();
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
                player.GetDamage();
                objectsListToCheck.Add(new Effect(new Vector2(player.GetTransform.Position.X, player.GetTransform.Position.Y), "assets/animations/powerdown/", 8, 0.077f));
                onCollisionSound.Invoke($"{player.GetType().Name}", "hit");
            }
            else
            {
                objectsListToCheck.Add(new Effect(new Vector2(player.GetTransform.Position.X, player.GetTransform.Position.Y + 32), "assets/animations/explosion/", 13, 0.077f));
                onCollisionSound.Invoke($"{player.GetType().Name}", "");
                objectsListToCheck.RemoveAt(x);
            }
        }
    }
}
