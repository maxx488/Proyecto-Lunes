using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Time
    {
        static private float deltaTime;
        private DateTime startTime;
        private float lastFrameTime;

        public Time()
        {
            startTime = DateTime.Now;
        }

        static public float DeltaTime => deltaTime;

        public void Update()
        {
            var currentTime = (float)(DateTime.Now - startTime).TotalSeconds;
            deltaTime = currentTime - lastFrameTime;
            lastFrameTime = currentTime;
        }
    }
}
