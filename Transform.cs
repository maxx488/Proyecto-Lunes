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
        private Vector2 scale;

        public Transform(Vector2 position, Vector2 scale)
        {
            this.position = position;
            this.scale = scale;
        }

        public Vector2 Position => position;
        public Vector2 Scale => scale;

        public void Translate(Vector2 dir,float spd)
        {
            position.X += dir.X * spd * Time.DeltaTime;
            position.Y += dir.Y * spd * Time.DeltaTime;
        }

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void SetScale(Vector2 newScale)
        {
            scale = newScale;
        }
    }
}
