using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PowerUp
    {
        private Image powerUpImage = Engine.LoadImage("assets/powerup.png");
        private int type;
        private float speed = 150;
        private Transform transform;
        private Vector2 down = new Vector2(0, 1);
        private bool inBounds = true;

        public PowerUp(Vector2 vector, int typ)
        {
            transform = new Transform(vector);
            type = typ;
        }

        public Transform GetPowerUpTransform => transform;

        public bool InBounds => inBounds;

        public int Type => type;

        public void Update()
        {
            transform.Translate(down, speed);
        }

        public void Render()
        {
            Engine.Draw(powerUpImage, transform.Position.X, transform.Position.Y);
        }
    }
}
