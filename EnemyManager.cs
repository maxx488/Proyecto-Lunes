using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public  class EnemyManager
    {
        public delegate void statsDelegate(int x);
        private event statsDelegate onEnemyDestroyed;
        private List<GameObject> objList;
        private int enemiesDestroyed;

        public EnemyManager(List<GameObject> objList)
        {
            GameManager.Stats.SubStats(this);
            this.objList = objList;
            enemiesDestroyed = 0;
        }

        public event statsDelegate OnEnemyDestroyed
        {
            add { onEnemyDestroyed += value; }
            remove { onEnemyDestroyed -= value; }
        }

        public int EnemiesDestroyed => enemiesDestroyed;

        public void Update()
        {
            if (objList.OfType<Enemy>().Count() > 0)
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    if (objList[i] is Enemy)
                    {
                        Enemy enemy = (Enemy) objList[i];
                        enemy.Update();
                    }
                }
                for (int i = 0; i < objList.Count; i++)
                {
                    if (objList[i] is Enemy)
                    {
                        Enemy enemy = (Enemy)objList[i];
                        if (enemy.InBounds == false)
                        {
                            objList.RemoveAt(i);
                        }
                    }
                }
                for (int i = 0; i < objList.Count; i++)
                {
                    if (objList[i] is Enemy)
                    {
                        Enemy enemy = (Enemy)objList[i];
                        if (enemy.PowerController.Destroyed == true)
                        {
                            onEnemyDestroyed.Invoke(enemy.Type);
                            objList.RemoveAt(i);
                            enemiesDestroyed++;
                        }
                    }
                }
            }
        }

        public void Render()
        {
            if (objList.OfType<Enemy>().Count() > 0)
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    if (objList[i] is Enemy)
                    {
                        Enemy enemy = (Enemy)objList[i];
                        enemy.Render();
                    }
                }
            }
        }
    }
}
