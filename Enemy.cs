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
        private Image enemigoImage;
        private EnemyController enemyController;
        private Transform enemyTransform;
        private float timer;
        private float cooldown = 1;
        private int animIndex = 1;
        private float timerAnim;
        private float animCooldown = 0.15f;
        private float shootTimer;
        private float shootCooldown = 1f;
        private bool shoot = false;
        private bool inBounds = true;
        private bool destroyed = false;
        private bool isBoss;

        public Enemy(Vector2 position, bool boss) 
        {
            isBoss = boss;
            enemyTransform = new Transform(position);
            enemyController = new EnemyController(enemyTransform, isBoss);
        }

        public Transform GetEnemyTransform => enemyTransform;

        public bool InBounds => inBounds;

        public bool IsBoss => isBoss;

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
            animationRender();
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

        private void animationRender()
        {
            enemigoImage = Engine.LoadImage($"assets/animations/enemies/1/{animIndex}.png");
            Engine.Draw(enemigoImage, enemyTransform.Position.X, enemyTransform.Position.Y);
        }

        private void AnimationUpdate()
        {
            timerAnim += Program.deltaTime;
            if (timerAnim > animCooldown)
            {
                timerAnim = 0;
                animIndex++;
            }
            if (animIndex > 5)
            {
                animIndex = 1;
            }
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
