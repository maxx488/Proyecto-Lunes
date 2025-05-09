using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class ProyectileManager
    {
        private List<GameObject> objectsToCheck;

        public ProyectileManager(List<GameObject> objectsToCheck)
        {
            this.objectsToCheck = objectsToCheck;
        }

        public void Update()
        {
            if (objectsToCheck.OfType<Projectile>().Count() > 0)
            {
                for (int i = 0; i < objectsToCheck.Count; i++)
                {
                    if (objectsToCheck[i] is Projectile)
                    {
                        Projectile projectile = (Projectile) objectsToCheck[i];
                        projectile.Update();
                    }
                }
                for (int i = 0; i < objectsToCheck.Count; i++)
                {
                    if (objectsToCheck[i] is Projectile)
                    {
                        Projectile projectile = (Projectile) objectsToCheck[i];
                        if (projectile.InBounds == false)
                        {
                            objectsToCheck.RemoveAt(i);
                        }
                    }
                }
            }
        }

        public void Render()
        {
            if (objectsToCheck.OfType<Projectile>().Count() > 0)
            {
                for (int i = 0; i < objectsToCheck.Count; i++)
                {
                    if (objectsToCheck[i] is Projectile)
                    {
                        Projectile projectile = (Projectile) objectsToCheck[i];
                        projectile.Render();
                    }
                }
            }
        }
    }
}
