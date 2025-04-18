using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class AnimationController
    {
        private Image image;
        private Transform transform;
        private float timer;
        private float animCooldown;
        private string path;
        private int index = 1;
        private int maxIndex;

        public AnimationController(Transform transform, string path, int maxIndex, float animCooldown)
        {
            this.transform = transform;
            this.path = path;
            this.maxIndex = maxIndex;
            this.animCooldown = animCooldown;
        }

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }

        public bool Finished
        {
            get
            {
                return index == maxIndex;
            }
        }

        public void Update()
        {
            AnimationUpdate();
        }

        public void Render()
        {
            AnimationRender();
        }

        private void AnimationUpdate()
        {
            timer += Time.DeltaTime;
            if (timer > animCooldown)
            {
                timer = 0;
                index++;
            }
            if (index > maxIndex)
            {
                index = 1;
            }
        }

        private void AnimationRender()
        {
            image = Engine.LoadImage(path + $"{index}.png");
            Engine.Draw(image, transform.Position.X, transform.Position.Y);
        }
    }
}
