using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player
    {
        private PlayerController playerController;
        private Transform playerTransform;
        private AnimationController animationController;
        private float shootTimer;
        private float shootCooldown = 0.3f;
        private bool shoot = false;
        private int power = 1;

        public Player(Vector2 position)
        {
            playerTransform = new Transform(position);
            playerController = new PlayerController(playerTransform);
            animationController = new AnimationController(playerTransform, $"assets/animations/player/{power}/", 4);
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
                    animationController.Path = $"assets/animations/player/{power}/";
                    if (value == 2 || value == 3)
                    {
                        shootCooldown = 0.1f;
                    }
                    else
                    {
                        shootCooldown = 0.3f;
                    }
                    Engine.Debug($"\nPoder = {power}\n");
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
            animationController.Update();
        }

        public void Render()
        {
            animationController.Render();
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

        private void Kill()
        {
            Engine.Debug("El Jugador ha muerto.");
        }
    }
}
