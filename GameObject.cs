using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public abstract class GameObject
    {
        protected Transform transform;
        protected AnimationController animationController;

        public Transform GetTransform => transform;

        public abstract void Update();

        public virtual void Render()
        {
            animationController.Render();
        }
    }
}
