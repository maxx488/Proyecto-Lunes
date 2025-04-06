using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public struct Collider
    {
        private Vector2 positionA;
        private Vector2 sizeA;
        private Vector2 positionB;
        private Vector2 sizeB;

        public Collider(Vector2 positionA, Vector2 sizeA, Vector2 positionB, Vector2 sizeB)
        {
            this.positionA = positionA;
            this.positionB = positionB;
            this.sizeA = sizeA;
            this.sizeB = sizeB;
        }

        public bool IsBoxColliding()
        {
            if (positionA.X + sizeA.X > positionB.X && positionA.Y + sizeA.Y > positionB.Y && positionA.Y < positionB.Y + sizeB.Y && positionB.X + sizeB.X > positionA.X)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
