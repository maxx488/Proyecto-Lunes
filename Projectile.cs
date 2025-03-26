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
        private int direction;
        private float speed;
        private float location;
        private float rute;
        private bool inBounds = true;

        public Projectile(float locX,float locY,int dir,float spd)
        {
            direction = dir;
            speed = spd;
            location = locX;
            rute = locY;
        }

        public bool InBounds => inBounds;

        public void Update()
        {
            if (direction == 1)
            {
                if (rute > 0)
                {
                    rute -= speed * Program.deltaTime;
                }
                else
                {
                    inBounds = false;
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
            }
        }

        public void Render()
        {
            Engine.Draw(bullet, location, rute);
        }
    }
}
