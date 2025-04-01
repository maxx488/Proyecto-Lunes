using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PowerUpStack: IStack
    {
        int []a;
        int index;

        public void InitializeStack()
        {
            a = new int[3];
            index = 0;
        }

        public void Stack(int x)
        {
            a[index] = x;
            index++;
        }

        public void Remove()
        {
            index--;
        }

        public bool EmptyStack()
        {
            return (index == 0);
        }

        public bool FullStack()
        {
            return (index == 3);
        }

        public int Top()
        {
            return a[index - 1];
        }
        public void ShowStack()
        {
            for (int i = index - 1; i >= 0; i--)
            {
                Console.WriteLine($"Elemento {i}: " + a[i]);
            }
        }
    }
}
