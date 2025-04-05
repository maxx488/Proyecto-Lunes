using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Projectile
    {
        private Image bullet = Engine.LoadImage("assets/bullet.png");
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
        }

        public bool InBounds => inBounds;

        public void Update()
        {
            ProjectileBehavior();
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

        public void Render()
        {
            Engine.Draw(bullet, location, rute);
        }
    }
}
