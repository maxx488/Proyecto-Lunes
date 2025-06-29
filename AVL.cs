using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class AVL
    {
        public Node root;
        public AVL()
        {
        }
        public void Add(int data, int id)
        {
            Node newItem = new Node(data, id);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }

            /*
             Insert(): después de insertar un nuevo nodo usando el procedimiento normal (recursivo o no recursivo), 
            es necesario verificar cada uno de los ancestros de los nodos en busca de un desequilibrio en el árbol, 
            por lo tanto llamando al método Balance(), básicamente, insert y luego un pequeño arreglar.
            Para entrar en más detalles, tenemos métodos de inserción públicos y privados. El método privado se 
            inserta recursivamente y toma un nuevo objeto de nodo y una referencia/puntero de nodo, y es aquí donde 
            llamamos a Balance_Tree(). En el método público, llamamos al método de inserción recursivo privado y 
            necesitamos establecer nuestro puntero/referencia raíz igual a la llamada al método porque el método 
            privado devuelve el tipo Nodo. También debido a la recursividad, cuando realizamos una rotación al 
            llamar al método Balance_Tree(), necesitamos recurrir a un nivel superior y realizar las reconexiones 
            necesarias del padre a los nodos pivotantes. La mejor manera de visualizar esta recursividad es dibujar 
            un marco de pila de llamadas para ver mejor el proceso.
            En resumen, nuestro caso base es que si nuestro nodo actual que usamos para atravesar el árbol 
            para insertar es nulo, actual = nuevo nodo y devuelve actual. Eso iría a nuestra siguiente declaración, 
            que se repite un nivel y establece actual->izquierda/derecha en el nodo recién agregado. 
            Luego equilibramos nuestro árbol llamando a Balance_Tree. Una vez realizadas las rotaciones, 
            regresamos a nuestro nodo pivote y volvemos a verificar el factor de equilibrio para asegurarnos de 
            que no tengamos desequilibrios. Recurra a un nivel una vez más y vuelva a conectar los nodos rotados 
            al nodo principal.

             */
        }
        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n.data < current.data)
            {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            }
            else if (n.data > current.data)
            {
                current.right = RecursiveInsert(current.right, n);
                current = balance_tree(current);
            }
            return current;
        }
        private Node balance_tree(Node current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;

            /* Balance_Tree(): Este método toma un puntero/referencia de nodo que se le pasa. 
Cuando equilibramos el árbol, el algoritmo es algo como esto:
Si el factor de equilibrio es 2, primero verificamos si tenemos un caso de izquierda a izquierda, 
si lo tenemos, realizamos esa rotación; de lo contrario, realizamos una rotación de izquierda a derecha.
Si el factor de equilibrio es -2, primero verificamos si tenemos un caso de derecha a derecha, 
si lo tenemos, realizamos una rotación de derecha a derecha; de lo contrario, realizamos una rotación de derecha a izquierda.
 */
        }
        public void Delete(int target)
        {
            root = Delete(root, target);

            /*
             Delete(): al igual que Insert(), después de que se produce la eliminación tenemos que llamar a 
            Balance() para verificar cada uno de los nodos en busca de cualquier desequilibrio en el árbol, 
            tenemos un Delete() público y un Delete() recursivo privado que hace el verdadero trabajo
             */
        }
        private Node Delete(Node current, int target)
        {
            Node parent;
            if (current == null)
            { return null; }
            else
            {
                //left subtree
                if (target < current.data)
                {
                    current.left = Delete(current.left, target);
                    if (balance_factor(current) == -2)//here
                    {
                        if (balance_factor(current.right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                //right subtree
                else if (target > current.data)
                {
                    current.right = Delete(current.right, target);
                    if (balance_factor(current) == 2)
                    {
                        if (balance_factor(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                //if target is found
                else
                {
                    if (current.right != null)
                    {
                        //delete its inorder successor
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.data = parent.data;
                        current.right = Delete(current.right, parent.data);
                        if (balance_factor(current) == 2)//rebalancing
                        {
                            if (balance_factor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {   //if current.left != null
                        return current.left;
                    }
                }
            }
            return current;
        }
        public void Find(int key)
        {
            if (Find(key, root).data == key)
            {
                Console.WriteLine("{0} was found!", key);
            }
            else
            {
                Console.WriteLine("Nothing found!");
            }

            /*
             Search(): Al estar los elementos equilibrados, 
             la implementación normal en esta función es suficiente.
             */
        }
        private Node Find(int target, Node current)
        {

            if (target < current.data)
            {
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.left);
            }
            else
            {
                if (target == current.data)
                {
                    return current;
                }
                else
                    return Find(target, current.right);
            }

        }
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            InOrderDisplayTree(root);
            Console.WriteLine();
        }
        private void InOrderDisplayTree(Node current)
        {
            if (current != null)
            {
                InOrderDisplayTree(current.left);
                Console.Write("({0}) ", current.data);
                InOrderDisplayTree(current.right);
            }
        }
        private int max(int l, int r)
        {
            return l > r ? l : r;
        }
        private int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;

            //GetHeight(): toma un argumento de puntero/referencia de nodo y devuelve la altura.
        }
        private int balance_factor(Node current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;

            // Balance_Factor(): toma una referencia de nodo como argumento, esto obtendrá recursivamente
            // las alturas de ambos lados y devolverá un número entero (altura izquierda – altura derecha)
        }
        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;

            //RotateRR(), RotateLL(), RotateLR(), and RotateRL():
            //toman un argumento de referencia/puntero de nodo y devuelven un nodo pivote con la rotación.
        }
        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }

        public void Render(Node a)
        {
            if (a != null)
            {
                Engine.Draw(a.image, a.placement.X, a.placement.Y);
                Render(a.left);
                Render(a.right);
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
    }
}
