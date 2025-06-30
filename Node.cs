using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Node
    {
        public int data;
        public Vector2 placement;
        public int enemyCount;
        public Image image;
        public Node left;
        public Node right;
        public Node(int data, int planetId)
        {
            this.data = data;
            image = Engine.LoadImage($"assets/map/planets/{planetId}.png");
        }
    }
}
