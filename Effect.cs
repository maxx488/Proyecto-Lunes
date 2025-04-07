using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Effect
    {
        private Transform transform;
        private AnimationController animationController;

        public Effect(Vector2 vector, string path, int maxIndex, float animCooldown)
        {
            transform = new Transform(vector);
            animationController = new AnimationController(transform, path, maxIndex, animCooldown);
        }

        public AnimationController GetAnimationController => animationController;

        public void Update()
        {
            animationController.Update();
        }

        public void Render()
        {
            animationController.Render();
        }
    }
}
