using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public  class EnemyManager
    {
        private List<Enemy> enemiesToManage;
        private int enemiesDestroyed;

        public EnemyManager(List<Enemy> enemies)
        {
            this.enemiesToManage = enemies;
            enemiesDestroyed = 0;
        }

        public int EnemiesDestroyed => enemiesDestroyed;

        public void Update()
        {
            if (enemiesToManage.Count > 0)
            {
                for (int i = 0; i < enemiesToManage.Count; i++)
                {
                    enemiesToManage[i].Update();
                }
                for (int i = 0; i < enemiesToManage.Count; i++)
                {
                    if (enemiesToManage[i].InBounds == false)
                    {
                        enemiesToManage.RemoveAt(i);
                    }
                }
                for (int i = 0; i < enemiesToManage.Count; i++)
                {
                    if (enemiesToManage[i].PowerController.Destroyed == true)
                    {
                        enemiesToManage.RemoveAt(i);
                        enemiesDestroyed++;
                    }
                }
            }
        }

        public void Render()
        {
            if (enemiesToManage.Count > 0)
            {
                for (int i = 0; i < enemiesToManage.Count; i++)
                {
                    enemiesToManage[i].Render();
                }
            }
        }
    }
}
