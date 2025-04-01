using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player
    {
        private Image playerImage;
        private static float posX;
        private static float posY;
        private float speed = 500;
        private int health = 1;
        private int animIndex = 1;
        private float timer;
        private float shootTimer;
        private float shootCooldown = 0.3f;
        private float animCooldown = 0.2f;
        private bool shooting = false;
        private int power = 1;

        public Player(float posicionX, float posicionY)
        {
            posX = posicionX;
            posY = posicionY;
        }

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                if (health != 0)
                {
                    if (value >= 0 && value <= 5)
                    {
                        health = value;
                        Engine.Debug($"Vida = {health}");
                    }
                    if (health == 0)
                    {
                        Kill();
                    }
                }
            }
        }

        public float GetPosX => posX;
        
        public float GetPosY => posY;

        public int GetPower => power;

        public int SetPower
        {
            get
            {
                return power;
            }
            set
            {
                if (value >= 1 && value <= 3)
                {
                    power = value;
                    if (value == 2 || value == 3)
                    {
                        shootCooldown = 0.1f;
                    }
                    else
                    {
                        shootCooldown = 0.3f;
                    }
                    Engine.Debug($"Poder = {power}");
                }
            }
        }

        public bool Shooting
        {
            get
            {
                return shooting;
            }
            set 
            {
                shooting = value;
            }
        }

        public void Input() 
        {
            Movement();
            Shoot();
        }

        public void Update()
        {
            AnimationUpdate();
        }

        public void Render()
        {
            animationRender();
        }

        private void Movement()
        {
            if (Engine.GetKey(Engine.KEY_A) && posX > 0)
            {
                posX += (speed * -1) * Program.deltaTime;
            }
            if (Engine.GetKey(Engine.KEY_D) && posX < 964)
            {
                posX += speed * Program.deltaTime;
            }
            if (Engine.GetKey(Engine.KEY_W) && posY > 0)
            {
                posY += (speed * -1) * Program.deltaTime;
            }
            if (Engine.GetKey(Engine.KEY_S) && posY < 702)
            {
                posY += speed * Program.deltaTime;
            }
        }

        private void Shoot()
        {
            shootTimer += Program.deltaTime;
            if (shootTimer > shootCooldown)
            {
                if (Engine.GetKey(Engine.KEY_ESP))
                {
                    shooting = true;
                    shootTimer = 0;
                }
            }
        }

        private void animationRender()
        {
            playerImage = Engine.LoadImage($"assets/animations/player/{animIndex}.png");
            Engine.Draw(playerImage, posX, posY);
        }

        private void AnimationUpdate()
        {
            timer += Program.deltaTime;
            if (timer > animCooldown)
            {
                timer = 0;
                animIndex++;
            }
            if (animIndex > 4)
            {
                animIndex = 1;
            }
        }

        private void Kill()
        {
            Engine.Debug("El Jugador ha muerto.");
        }
    }
}
