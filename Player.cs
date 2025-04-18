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
        private int power = 1;
        private bool damaged = false;
        private float damagedTimer = 0;

        public Player(Vector2 position)
        {
            playerTransform = new Transform(position);
            playerController = new PlayerController(playerTransform);
            animationController = new AnimationController(playerTransform, $"assets/animations/player/{power}/", 4, 0.2f);
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
                        playerController.ShootCooldown = 0.1f;
                    }
                    else
                    {
                        playerController.ShootCooldown = 0.3f;
                    }
                    Engine.Debug($"\nPoder = {power}\n");
                }
            }
        }

        public bool Damaged
        {
            get
            {
                return damaged;
            }
            set
            {
                damaged = value;
            }
        }

        public bool ShootState
        {
            get
            {
                return playerController.GetShoot;
            }
        }

        public void Input() 
        {
            playerController.Input();
        }

        public void Update()
        {
            animationController.Update();
        }

        public void Render()
        {
            if (damaged == true)
            {
                damagedTimer += Time.DeltaTime;
                switch (damagedTimer)
                {
                    case float n when (n >= 0f && n <= 0.25f):
                        animationController.Render();
                        break;
                    case float n when (n >= 0.5f && n <= 0.75f):
                        animationController.Render();
                        break;
                    case float n when (n >= 1f && n <= 1.25f):
                        animationController.Render();
                        break;
                    case float n when (n >= 1.5f && n <= 1.75f):
                        animationController.Render();
                        break;
                }
                if (damagedTimer >= 2)
                {
                    damagedTimer = 0;
                    damaged = false;
                }
            }
            else
            {
                animationController.Render();
            }
        }
    }
}
