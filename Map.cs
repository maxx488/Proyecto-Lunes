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
        private Graph graph = new Graph();
        private Sorter sorter = new Sorter();
        private Font font = new Font("assets/fonts/PressStart2P.ttf", 38);
        private Font fontPath = new Font("assets/fonts/PressStart2P.ttf", 12);
        private Image background = Engine.LoadImage($"assets/map/backgroundGraph.png");
        private Random random = new Random();
        private Dictionary<int, int> caminosPeso = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 } };
        private int[][] caminos = new int[][]
        {
            new int[] { 1, 2, 4, 8 },
            new int[] { 3, 2, 4, 8 },
            new int[] { 5, 6, 4, 8 },
            new int[] { 7, 6, 4, 8 },
            new int[] { 9, 10, 12, 8 },
            new int[] { 11, 10, 12, 8 },
            new int[] { 13, 14, 12, 8 },
            new int[] { 15, 14, 12, 8 }
        };
        private int[] verticesIniciales = new int[] { 1, 3, 5, 7, 9, 11, 13, 15 };
        private int[] verticesInicialesPeso = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        private int caminoFacil;

        public Map()
        {
            CreateGalaxyMap();
        }

        public Dictionary<int, int> CaminosPeso => caminosPeso;

        public void Render()
        {
            Engine.Draw(background, 0, 0);
            RenderMap(tree.root);
            RenderText();
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
            AssignImagePlacements(tree.root, 0, 0);//se distribuyen los nodos visualmente.

            graph.InicializarGrafo();
            for (int i = 0; i < dataArray.Length; i++)
            {
                graph.AgregarVertice(dataArray[i]);
            }
            // vector de aristas - vertices origen
            int[] aristas_origen = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            // vector de aristas - vertices destino
            int[] aristas_destino = { 2, 4, 2, 8, 6, 4, 6, 8, 10, 12, 10, 8, 14, 12, 14 };
            // vector de aristas - pesos
            int[] aristas_pesos = new int[15]; // cantidad de enemigos en el camino

            //asigno pesos
            for (int i = 0; i < aristas_pesos.Length; i++)
            {
                aristas_pesos[i] = random.Next(10, 30);
            }
            // agrego las aristas

            for (int i = 0; i < aristas_pesos.Length; i++)
            {
                graph.AgregarArista(aristas_origen[i], aristas_destino[i], aristas_pesos[i]);
            }

            Console.WriteLine("\nListado de Etiquetas de los nodos");
            for (int i = 0; i < graph.Etiqs.Length; i++)
            {
                if (graph.Etiqs[i] != 0)
                {
                    Console.WriteLine("Nodo: " + graph.Etiqs[i].ToString());
                }
            }

            Console.WriteLine("\nListado de Aristas (Inicio, Fin, Peso)");
            for (int i = 0; i < graph.cantNodos; i++)
            {
                for (int j = 0; j < graph.cantNodos; j++)
                {
                    if (graph.MAdy[i, j] != 0)
                    {
                        // obtengo la etiqueta del nodo origen, que está en las filas (i)
                        int nodoIni = graph.Etiqs[i];
                        // obtengo la etiqueta del nodo destino, que está en las columnas (j)
                        int nodoFin = graph.Etiqs[j];
                        Console.WriteLine(nodoIni.ToString() + ", " + nodoFin.ToString() + ", " + graph.MAdy[i, j].ToString());
                    }
                }
            }
            int key = 0;
            foreach (var camino in caminos) //sumatoria de peso por camino
            {
                key++;
                int suma = 0;
                for (int i = 0; i < camino.Length - 1; i++)
                {
                    int desde = camino[i];
                    int hasta = camino[i + 1];

                    int peso = graph.PesoArista(desde, hasta);
                    suma += peso;
                }
                caminosPeso[key] += suma;
                Console.WriteLine($"Camino: {string.Join(" → ", camino)} | Suma de pesos: {suma}");
            }
            Console.WriteLine("");
            Console.WriteLine("DIJKSTRA");
            // al algoritmo le paso el TDA_Grafo estático con los datos cargados y el vértice origen
            AlgDijkstra.Dijkstra(graph, 1);
            // muestro resultados
            MuestroResultadosAlg(AlgDijkstra.distance, graph.cantNodos, graph.Etiqs, AlgDijkstra.nodos);

            caminoFacil = MenorPeso(verticesIniciales);
            Console.WriteLine("");
            Console.WriteLine($"{caminoFacil}");
        }

        public void RenderMap(Node node)
        {
            if (node != null)
            {
                Engine.Draw(node.image, node.placement.X, node.placement.Y);
                RenderMap(node.left);
                RenderMap(node.right);
            }
        }

        public void AssignImagePlacements(Node node, int depth, int index)
        {
            if (node != null)
            {
                int numNodesInLevel = (int)Math.Pow(2, depth); // cantidad de nodos por profundidad (depth^2)
                float totalWidth = Engine.ScreenSizeW - 60; // le resto tamaño planeta para que quede centrado
                float nodeX = (index + 1) * (totalWidth / (numNodesInLevel + 1));
                float nodeY = depth * 150f;       // espaciado vertical

                node.placement = new Vector2(nodeX, nodeY);

                AssignImagePlacements(node.left, depth + 1, index * 2);
                AssignImagePlacements(node.right, depth + 1, index * 2 + 1);
            }
        }

        private void RenderText()
        {
            Engine.DrawText("Choose Your Path...", 160, 700, 255, 10, 0, font);
            int x = 65;
            int y = 520;
            for (int i = 0; i < caminos.Length; i++)
            {
                Engine.DrawText($"{i + 1}: ", x, y, 255, 255, 255, fontPath);
                if (caminosPeso[i + 1] == caminoFacil)
                {
                    Engine.DrawText($"Enem:{caminosPeso[i + 1]}", x, y + 20, 0, 255, 10, fontPath);
                }
                else
                {
                    Engine.DrawText($"Enem:{caminosPeso[i + 1]}", x, y + 20, 255, 255, 255, fontPath);
                }
                x += 120;
            }
        }

        private void MuestroResultadosAlg(int[] distance, int verticesCount, int[] Etiqs, string[] caminos)
        {
            string distancia = "";

            Console.WriteLine("Vertice    Distancia desde origen    Nodos");

            for (int i = 0; i < verticesCount; ++i)
            {
                if (distance[i] == int.MaxValue)
                {
                    distancia = "---";
                }
                else
                {
                    distancia = distance[i].ToString();
                }
                Console.WriteLine("{0}\t  {1}\t\t\t   {2}", Etiqs[i], distancia, caminos[i]);
            }
        }

        private int MenorPeso(int[] iniciales)
        {
            for (int i = 0;i < iniciales.Length; ++i)
            {
                AlgDijkstra.Dijkstra(graph, iniciales[i]);
                VectorPesos(i, AlgDijkstra.distance, graph.cantNodos, graph.Etiqs, AlgDijkstra.nodos);
            }
            return verticesInicialesPeso.Min();
        }

        private void VectorPesos(int index, int[] distance, int verticesCount, int[] Etiqs, string[] caminos)
        {
            for (int i = 0; i < verticesCount; ++i)
            {
                if (caminos[i] != null)
                {
                    if (caminos[i].Contains("8"))
                    {
                        verticesInicialesPeso[index] = distance[i];
                        break;
                    }
                }
            }
        }
    }
}
