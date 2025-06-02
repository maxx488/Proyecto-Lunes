using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Effect: GameObject, IPoolable
    {
        public event Action OnDisable;

        public Effect()
        {
            
        }

        public AnimationController GetAnimationController => animationController;

        public void Initialize(Vector2 vector, string path, int maxIndex, float animCooldown)
        {
            if (transform == null)
            {
                transform = new Transform(vector, new Vector2(0, 0));
                animationController = new AnimationController(transform, path, maxIndex, animCooldown);
            }
            else
            {
                transform.SetPosition(vector);
                animationController.GetTransform.SetPosition(vector);
                animationController.Path = path;
                animationController.MaxIndex = maxIndex;
                animationController.AnimCooldown = animCooldown;
                animationController.ForceAnimationUpdate();
            }
        }

        public override void Update()
        {
            animationController.Update();
        }

        public void Disable()
        {
            OnDisable.Invoke();
        }

        public void Reset()
        {
            
        }
    }
}
