﻿using System;
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
        private float speed;
        private float shootTimer;
        private float shootCooldown = 1f;
        private bool shoot = false;
        private int type;
        private Vector2 up = new Vector2(0, -1);
        private Vector2 down = new Vector2(0, 1);
        private Vector2 left = new Vector2(-1, 0);
        private Vector2 right = new Vector2(1, 0);

        public EnemyController(Transform transform, bool isBoss, int typ, float spd)
        {
            this.transform = transform;
            this.isBoss = isBoss;
            this.type = typ;
            speed = spd;
        }

        public Transform GetTransform => transform;

        public bool GetShoot => shoot;

        public void Update()
        {
            MovementUpdate();
            Shoot();
        }

        private void MovementUpdate()
        {
            if (isBoss != true)
            {
                transform.Translate(down, speed);
            }
            else
            {
                if (transform.Position.X > (Engine.ScreenSizeW - transform.Scale.X) || transform.Position.X < 0)
                {
                    speed *= -1;
                }
                transform.Translate(right, speed);
            }
        }

        private void Shoot()
        {
            shootTimer += Time.DeltaTime;
            if (shootTimer > shootCooldown)
            {
                if (type == 2)
                {
                    if (shootCooldown == 1f)
                    {
                        shootCooldown = 0.5f;
                    }
                    else
                    {
                        shootCooldown = 1f;
                    }
                }
                shootTimer = 0;
                shoot = true;
            }
            else
            {
                shoot = false;
            }
        }

        public void ResetData(bool isBoss, int typ, float spd)
        {
            shootTimer = 0;
            this.isBoss = isBoss;
            this.type = typ;
            speed = spd;
        }
    }
}
