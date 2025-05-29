using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public interface IPoolable
    {
        event Action OnDisable;
        void Reset();
    }
}
