using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class ProyectileManager
    {
        private List<Projectile> proyectilesToManage;

        public ProyectileManager(List<Projectile> proyectiles)
        {
            this.proyectilesToManage = proyectiles;
        }

        public void Update()
        {
            if (proyectilesToManage.Count > 0)
            {
                for (int i = 0; i < proyectilesToManage.Count; i++)
                {
                    proyectilesToManage[i].Update();
                }
                for (int i = 0; i < proyectilesToManage.Count; i++)
                {
                    if (proyectilesToManage[i].InBounds == false)
                    {
                        proyectilesToManage.RemoveAt(i);
                    }
                }
            }
        }

        public void Render()
        {
            if (proyectilesToManage.Count > 0)
            {
                for (int i = 0; i < proyectilesToManage.Count; i++)
                {
                    proyectilesToManage[i].Render();
                }
            }
        }
    }
}
