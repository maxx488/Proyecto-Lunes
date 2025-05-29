using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class AnimationController
    {
        private Renderer renderer;
        private float timer;
        private float animCooldown;
        private string path;
        private int index = 1;
        private int maxIndex;

        public AnimationController(Transform transform, string path, int maxIndex, float animCooldown)
        {
            this.path = path;
            renderer = new Renderer(transform, this.path + $"{index}.png");
            this.maxIndex = maxIndex;
            this.animCooldown = animCooldown;
        }

        public Transform GetTransform => renderer.GetTransform;

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
                if (index > maxIndex)
                {
                    index = 1;
                }
                renderer.SetImage(path + $"{index}.png");
            }
        }

        public void ForceAnimationUpdate()
        {
            timer = animCooldown;
        }

        private void AnimationRender()
        {
            renderer.Render();
        }
    }
}
