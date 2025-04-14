using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PowerUp
    {
        private AnimationController animationController;
        private int type;
        private float speed = 150;
        private Transform transform;
        private Vector2 down = new Vector2(0, 1);
        private bool inBounds = true;

        public PowerUp(Vector2 vector, int typ)
        {
            transform = new Transform(vector);
            animationController = new AnimationController(transform, "assets/animations/powerup/", 4, 0.2f);
            type = typ;
        }

        public Transform GetPowerUpTransform => transform;

        public bool InBounds => inBounds;

        public int Type => type;

        public void Update()
        {
            transform.Translate(down, speed);
            animationController.Update();
            if (transform.Position.Y > 768)
            {
                inBounds = false;
            }
        }

        public void Render()
        {
            animationController.Render();
        }
    }
}
