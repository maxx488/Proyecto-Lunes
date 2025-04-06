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
        private PlayerController playerController;
        private Transform playerTransform;
        private int animIndex = 1;
        private float timer;
        private float shootTimer;
        private float shootCooldown = 0.3f;
        private float animCooldown = 0.2f;
        private bool shoot = false;
        private int power = 1;

        public Player(Vector2 position)
        {
            playerTransform = new Transform(position);
            playerController = new PlayerController(playerTransform);
        }

        public Transform GetPlayerTransform => playerTransform;

        public int GetPower => power;

        public int SetPower
        {
            get
            {
                return power;
            }
            set
            {
                if (value <= 3)
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
                    if (value == 0)
                    {
                        Kill();
                    }
                }
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

        public void Input() 
        {
            playerController.Input();
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

        private void Shoot()
        {
            shootTimer += Program.deltaTime;
            if (shootTimer > shootCooldown)
            {
                if (playerController.GetShoot == true)
                {
                    shoot = true;
                    shootTimer = 0;
                }
            }
        }

        private void animationRender()
        {
            playerImage = Engine.LoadImage($"assets/animations/player/{animIndex}.png");
            Engine.Draw(playerImage, playerTransform.Position.X, playerTransform.Position.Y);
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
