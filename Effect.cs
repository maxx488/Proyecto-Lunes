using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Effect: GameObject
    {

        public Effect(Vector2 vector, string path, int maxIndex, float animCooldown)
        {
            transform = new Transform(vector, new Vector2(0, 0));
            animationController = new AnimationController(transform, path, maxIndex, animCooldown);
        }

        public AnimationController GetAnimationController => animationController;

        public override void Update()
        {
            animationController.Update();
        }
    }
}
