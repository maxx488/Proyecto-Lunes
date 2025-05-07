using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Projectile: GameObject
    {
        private string path;
        private int type;
        private int direction;
        private float speed;
        private bool inBounds = true;

        public Projectile(Vector2 position,int dir,float spd,int typ)
        {
            /* posiiton = vector posicion x y
               dir = 1 o -1 dependiendo si es proyectil del jugador o del enemigo, 2 y 3 para diagonales en powerup
               spd = velocidad
               typ = tipo o variacion del proyectil */
            transform = new Transform(position);
            direction = dir;
            speed = spd;
            type = typ;
            if (direction > 0)
            {
                path = "player";
            }
            else
            {
                path = "enemy";
            }
            animationController = new AnimationController(transform, $"assets/animations/projectile/{path}/", 4, 0.2f);
        }

        public bool InBounds => inBounds;

        public int Direction => direction;

        public override void Update()
        {
            ProjectileBehavior();
            AnimationUpdate();
        }

        private void ProjectileBehavior()
        {
            // Comportamiento del Proyectil
            if (direction == 1)
            {
                if (type == 1)
                {
                    if (transform.Position.Y > 0)
                    {
                        transform.Translate(new Vector2(0, -1), speed * 1.5f);
                    }
                    else
                    {
                        inBounds = false;
                    }
                }
                else if (type == 2 || type == 3)
                {
                    if (transform.Position.Y > 0)
                    {
                        transform.Translate(new Vector2(0, -1), speed * 2f);
                    }
                    else
                    {
                        inBounds = false;
                    }
                }
            }
            else
            {
                if (direction == -1)
                {
                    if (transform.Position.Y < 768)
                    {
                        transform.Translate(new Vector2(0, 1), speed);
                    }
                    else
                    {
                        inBounds = false;
                    }
                }
                else
                {
                    if (direction == 2)
                    {
                        if (type == 3)
                        {
                            if (transform.Position.Y > 0)
                            {
                                transform.Translate(new Vector2(0, -1), speed * 2f);
                                transform.Translate(new Vector2(-1, 0), speed);
                            }
                            else
                            {
                                inBounds = false;
                            }
                        }
                    }
                    else
                    {
                        if (direction == 3)
                        {
                            if (type == 3)
                            {
                                if (transform.Position.Y > 0)
                                {
                                    transform.Translate(new Vector2(0, -1), speed * 2f);
                                    transform.Translate(new Vector2(1, 0), speed);
                                }
                                else
                                {
                                    inBounds = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AnimationUpdate()
        {
            animationController.Update();
        }
    }
}
