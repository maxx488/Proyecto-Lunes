using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class GenericDynamicPool<T> where T : IPoolable, new()
    {
        private List<T> inUse = new List<T>();
        private List<T> available = new List<T>();

        public T Get()
        {
            T item;

            if (available.Count > 0)
            {
                item = available[0];
                available.RemoveAt(0);
            }
            else
            {
                item = new T();
                item.OnDisable += () => Reuse(item);
            }
            item.Reset();
            inUse.Add(item);
            PrintStatus();
            return item;
        }

        private void Reuse(T item)
        {
            inUse.Remove(item);
            available.Add(item);
        }

        private void PrintStatus()
        {
            //Engine.Debug("Objetos disponibles: " + available.Count);
            //Engine.Debug("Objetos en uso: " + inUse.Count);
        }
    }
}
