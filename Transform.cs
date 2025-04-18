using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Transform
    {
        private Vector2 position;

        public Transform(Vector2 position)
        {
            this.position = position;
        }

        public Vector2 Position => position;

        public void Translate(Vector2 dir,float spd)
        {
            position.X += dir.X * spd * Time.DeltaTime;
            position.Y += dir.Y * spd * Time.DeltaTime;
        }
    }
}
