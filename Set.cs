using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Set: ISet
    {
        int[] a;
        int index;
        int maxIndex;

        public void InitializeSet(int x)
        {
            a = new int[x];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = 0;
            }
            index = 0;
            maxIndex = x - 1;
        }

        public void Add(int x)
        {
            bool result = Contains(x);
            if (result == false)
            {
                a[index] = x;
                index++;
            }
        }
        public void Remove(int x)
        {
            bool result = Contains(x);
            if (result == true)
            {
                for (int i = 0; i <= index; i++)
                {
                    if (a[i] == x)
                    {
                        for (int j = i; j <= index; j++)
                        {
                            if (j < maxIndex)
                            {
                                a[j] = a[j + 1];
                            }
                        }
                        break;
                    }
                }
                index--;
            }
        }

        public bool EmptySet()
        {
            return (index == 0);
        }

        public bool FullSet()
        {
            return (index == maxIndex + 1);
        }

        public int[] ReturnSet()
        {
            return a;
        }

        public bool Contains(int x)
        {
            bool result = false;
            for (int i = 0; i <= index; i++)
            {
                if (a[i] == x)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
