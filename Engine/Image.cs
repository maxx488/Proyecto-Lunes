using System;
using Tao.Sdl;

public class Image: IDisposable // interfaz para liberar memoria
{
    private bool disposed = false;
    public IntPtr Pointer { get; private set; }

    public Image(string imagePath)
    {
        LoadImage(imagePath);
    }

    private void LoadImage(string imagePath)
    {
        Pointer = SdlImage.IMG_Load(imagePath);
        if (Pointer == IntPtr.Zero)
        {
            Console.WriteLine("Imagen inexistente: {0}", imagePath);
            Environment.Exit(4);
        }
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
            if (Pointer != IntPtr.Zero)
            {
                Sdl.SDL_FreeSurface(Pointer); 
                Pointer = IntPtr.Zero;
            }
            disposed = true;
        }
    }

    ~Image()
    {
        Dispose(false);
    }
}