using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Enemy
    {
        private EnemyController enemyController;
        private Transform enemyTransform;
        private AnimationController animationController;
        private int power;
        private int type;
        private float shootTimer;
        private float shootCooldown = 1f;
        private bool shoot = false;
        private bool inBounds = true;
        private bool destroyed = false;
        private bool isBoss;

        public Enemy(Vector2 position, bool boss) 
        {
            isBoss = boss;
            power = 3;
            enemyTransform = new Transform(position);
            enemyController = new EnemyController(enemyTransform, isBoss);
            animationController = new AnimationController(enemyTransform, $"assets/animations/enemies/1/", 5, 0.15f);
        }

        public Transform GetEnemyTransform => enemyTransform;

        public bool InBounds => inBounds;

        public bool IsBoss => isBoss;

        public int Power
        {
            get
            {
                return power;
            }
            set
            {
                power = value;
            }
        }

        public bool ShootState
        {
            get
            {
                return shoot;
            }
            set
            {
                shoot = value;
            }
        }

        public bool Destroyed
        {
            get
            {
                return destroyed;
            }
            set
            {
                destroyed = value;
            }
        }

        public void Update()
        {
            MovementUpdate();
            AnimationUpdate();
            Shoot();
        }

        public void Render()
        {
            AnimationRender();
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
            enemyController.Update();
            if (enemyTransform.Position.Y > 768)
            {
                inBounds = false;
            }
        }

        private void AnimationRender()
        {
            animationController.Render();
        }

        private void AnimationUpdate()
        {
            animationController.Update();
        }

        private void Shoot()
        {
            shootTimer += Program.deltaTime;
            if (shootTimer > shootCooldown)
            {
                shootTimer = 0;
                shoot = true;
            }
        }
    }
}
