using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public interface IQueue
    {
        void InitializeQueue();
        // siempre que la cola este inicializada
        void Enqueue(int x);
        // siempre que la cola este inicializada y no este vacía
        void Dequeue();
        // siempre que la cola este inicializada
        bool EmptyQueue();
        // siempre que la cola este inicializada y no este vacía
        int First();
        // siempre que la cola este inicializada
        int[] ShowQueue(int[] y);
        // siempre que la cola este inicializada
        bool FullQueue();
    }
}
