using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Renderer : IDisposable // interfaz para liberar memoria
    {
        private bool disposed = false;
        private Transform transform;
        private Image image;

        public Renderer(Transform transform, string imagePath)
        {
            this.transform = transform;
            image = Engine.LoadImage(imagePath);
        }

        public Transform GetTransform => transform;

        public void SetImage(string imagePath)
        {
            if (image != null) // Liberar memoria
            {
                image.Dispose();
                image = null;
            }
            image = Engine.LoadImage(imagePath);
        }

        public void Render()
        {
            Engine.Draw(image, transform.Position.X, transform.Position.Y);
        }

        public void Dispose() // Algoritmo para liberar memoria.
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Liberar recursos administrados
                    if (image != null)
                    {
                        image.Dispose();
                        image = null;
                    }
                }

                disposed = true;
            }
        }

        ~Renderer()
        {
            Dispose(false);
        }
    }
}
