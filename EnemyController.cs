using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class EnemyController
    {

        private Transform transform;
        private bool isBoss;
        private float speed = 250;
        private Vector2 up = new Vector2(0, -1);
        private Vector2 down = new Vector2(0, 1);
        private Vector2 left = new Vector2(-1, 0);
        private Vector2 right = new Vector2(1, 0);

        public EnemyController(Transform transform, bool isBoss)
        {
            this.transform = transform;
            this.isBoss = isBoss;
        }

        public void Update()
        {
            MovementUpdate();
        }

        private void MovementUpdate()
        {
            // MOVIMIENTO PARA JEFE
            /*timer += Program.deltaTime;
            if (timer > cooldown)
            {
                if (posX > 956 || posX < 1)
                {
                    timer = 0;
                    speed *= -1;
                }
            }
            posX += speed * Program.deltaTime;*/
            if (isBoss != true)
            {
                transform.Translate(down, speed);
            }
        }
    }
}
