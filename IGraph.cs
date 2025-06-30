using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public interface IGraph
    {
        void InicializarGrafo();
        void AgregarVertice(int v);
        void EliminarVertice(int v);
        void AgregarArista(int v1, int v2, int peso);
        void EliminarArista(int v1, int v2);
        bool ExisteArista(int v1, int v2);
        int PesoArista(int v1, int v2);
    }
}
