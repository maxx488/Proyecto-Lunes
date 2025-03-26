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
        private float posX;
        private float posY;
        private float timer;
        private float cooldown = 1;
        private int speed = 500;
        private int animIndex = 1;
        private float timerAnim;
        private float animCooldown = 0.15f;

        public Enemy(float posicionX, float posicionY) 
        {
            posX = posicionX;
            posY = posicionY;
        }

        public void Update()
        {
            MovementUpdate();
            AnimationUpdate();
        }

        public void Render()
        {
            animationRender();
        }

        private void MovementUpdate()
        {
            timer += Program.deltaTime;
            if (timer > cooldown)
            {
                if (posX > 956 || posX < 1)
                {
                    timer = 0;
                    speed *= -1;
                }
            }
            posX += speed * Program.deltaTime;
        }

        private void animationRender()
        {
            enemigoImage = Engine.LoadImage($"assets/animations/enemies/1/{animIndex}.png");
            Engine.Draw(enemigoImage, posX, posY);
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
    }
}
