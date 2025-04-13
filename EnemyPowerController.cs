using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class EnemyPowerController
    {
        private int power;
        private bool destroyed = false;

        public EnemyPowerController(int power)
        {
            this.power = power;
        }

        public bool Destroyed => destroyed;

        public int Power => power;

        public void DamageEnemy()
        {
            power--;
            if (power < 1)
            {
                destroyed = true;
            }
        }
    }
}
