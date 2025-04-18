using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PlayerController
    {
        private Transform transform;
        private float speed = 500;
        private bool shoot = false;
        private float shootTimer;
        private float shootCooldown = 0.3f;
        private Vector2 up = new Vector2(0, -1);
        private Vector2 down = new Vector2(0 ,1);
        private Vector2 left = new Vector2(-1 ,0);
        private Vector2 right = new Vector2(1 ,0);

        public PlayerController(Transform transform)
        {
            this.transform = transform;
        }

        public bool GetShoot => shoot;

        public float ShootCooldown
        {
            get
            {
                return shootCooldown;
            }
            set
            {
                shootCooldown = value;
            }
        }

        public void Input()
        {
            Movement();
            Shoot();
        }

        private void Movement()
        {
            if (Engine.GetKey(Engine.KEY_A) && transform.Position.X > 0)
            {
                transform.Translate(left, speed);
            }
            if (Engine.GetKey(Engine.KEY_D) && transform.Position.X < 964)
            {
                transform.Translate(right, speed);
            }
            if (Engine.GetKey(Engine.KEY_W) && transform.Position.Y > 0)
            {
                transform.Translate(up, speed);
            }
            if (Engine.GetKey(Engine.KEY_S) && transform.Position.Y < 702)
            {
                transform.Translate(down, speed);
            }
        }

        private void Shoot()
        {
            shootTimer += Time.DeltaTime;
            if (shootTimer > shootCooldown)
            {
                if (Engine.GetKey(Engine.KEY_ESP))
                {
                    shoot = true;
                    shootTimer = 0;
                }
            }
            else
            {
                shoot = false;
            }
        }
    }
}
