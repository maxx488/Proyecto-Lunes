using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Map
    {
        private Set set = new Set();
        private AVL tree = new AVL();
        private Sorter sorter = new Sorter();

        public Map()
        {
            CreateGalaxyMap();
        }

        public void Render()
        {
            tree.Render(tree.root);
        }

        private void CreateGalaxyMap()
        {
            set.InitializeSet(15);
            while (set.FullSet() != true)
            {
                Random random = new Random();
                set.Add(random.Next(1, 16));
            }
            int[] dataArray = set.ReturnSet();
            sorter.QuickSort(dataArray, 0, dataArray.Length -1); //se ordena el conjunto para que el arbol quede visualmente balanceado.
            set.InitializeSet(15);
            while (set.FullSet() != true)
            {
                Random random = new Random();
                set.Add(random.Next(1, 16));
            }
            int[] planetArray = set.ReturnSet(); //conjunto que no se ordena para darle aleatoridad a las imagenes de los nodos.
            for (int i = 0; i < dataArray.Length; i++)
            {
                tree.Add(dataArray[i], planetArray[i]);
            }
            tree.AssignImagePlacements(tree.root, 0, 0);//se distribuyen los nodos visualmente.
        }
    }
}
