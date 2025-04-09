using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public interface IStack
    {
        void InitializeStack();
        // siempre que la pila esté inicializada
        void Stack(int x);
        // siempre que la pila esté inicializada y no esté vacía
        void Remove();
        // siempre que la pila esté inicializada
        bool EmptyStack();
        // siempre que la pila esté inicializada y no esté vacía
        bool FullStack();
        // siempre que la pila esté inicializada y no esté vacía
        int Top();
        // Devolver tope
        int[] ShowStack(int[] b);
        // Devolver copia Pila
    }
}
