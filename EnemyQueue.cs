using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class EnemyQueue: IQueue
    {
        int[] a; // array donde se guarda la informacion
        int index; // variable que guarda la cantidad de elementos que se tienen guardados

        public void InitializeQueue()
        {
            a = new int[10];
            index = 0;
        }

        public void Enqueue(int x)
        {
            for (int i = index - 1; i >= 0; i--)
            {
                a[i + 1] = a[i];
            }
            a[0] = x;

            index++;
        }

        public void Dequeue()
        {
            index--;
        }

        public bool EmptyQueue()
        {
            return (index == 0);
        }

        public bool FullQueue()
        {
            return (index == 10);
        }

        public int First()
        {
            return a[index - 1];
        }

        public int[] ShowQueue(int[] b)
        {
            b = new int[index];
            for (int i = index - 1; i >= 0; i--)
            {
                b[i] = a[i];
            }
            return b;
        }
    }
}
