using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Tao.Sdl;



namespace MyGame
{

    class Program
    {
        private static Time time;
        private static GameManager gameManager;

        static void Main(string[] args)
        {
            Engine.Initialize();
            time = new Time();
            gameManager = GameManager.GetInstance();

            while (true)
            {
                Input();
                Update();
                Render();
            }
        }

        static void Input()
        {
            gameManager.Input();
        }

        static void Update()
        {
            time.Update();
            gameManager.Update();
        }

        static void Render()
        {
            Engine.Clear();
            gameManager.Render();
            Engine.Show();
            
        }
    }
}