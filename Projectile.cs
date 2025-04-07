using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Projectile
    {
        private Image bullet;
        private float timer;
        private float animCooldown = 0.2f;
        private int animIndex = 1;
        private string path;
        private int type;
        private int direction;
        private float speed;
        private float location;
        private float rute;
        private bool inBounds = true;

        public Projectile(float locX,float locY,int dir,float spd,int typ)
        {
            /* locX = posicion en X
               locY = posicion en Y
               dir = 1 o -1 dependiendo si es proyectil del jugador o del enemigo, 2 y 3 para diagonales en powerup
               spd = velocidad
               typ = tipo o variacion del proyectil */
            direction = dir;
            speed = spd;
            location = locX;
            rute = locY;
            type = typ;
            if (direction > 0)
            {
                path = "player";
            }
            else
            {
                path = "enemy";
            }
        }

        public bool InBounds => inBounds;

        public float Location => location;

        public float Rute => rute;

        public int Direction => direction;

        public void Update()
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
                    if (rute > 0)
                    {
                        rute -= (speed * 1.5f) * Program.deltaTime;
                    }
                    else
                    {
                        inBounds = false;
                    }
                }
                else if (type == 2 || type == 3)
                {
                    if (rute > 0)
                    {
                        rute -= (speed * 2f) * Program.deltaTime;
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
                    if (rute < 768)
                    {
                        rute += speed * Program.deltaTime;
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
                            if (rute > 0)
                            {
                                rute -= (speed * 2f) * Program.deltaTime;
                                location -= speed * Program.deltaTime;
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
                                if (rute > 0)
                                {
                                    rute -= (speed * 2f) * Program.deltaTime;
                                    location += speed * Program.deltaTime;
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
            timer += Program.deltaTime;
            if (timer > animCooldown)
            {
                timer = 0;
                animIndex++;
            }
            if (animIndex > 4)
            {
                animIndex = 1;
            }
        }

        public void Render()
        {
            bullet = Engine.LoadImage($"assets/animations/projectile/{path}/{animIndex}.png");
            Engine.Draw(bullet, location, rute);
        }
    }
}
