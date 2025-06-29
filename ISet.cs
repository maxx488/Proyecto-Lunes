using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public interface ISet
    {
        void InitializeSet(int x);
        void Add(int x);
        void Remove(int x);
        bool EmptySet();
        bool FullSet();
        int[] ReturnSet();
        bool Contains(int x);
    }
}
