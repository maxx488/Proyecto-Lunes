using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Tao.Sdl;



namespace MyGame
{

    class Program
    {

        public static float deltaTime;
        public static DateTime startTime;
        private static float lastFrameTime;
        private static GameManager gameManager;

        static void Main(string[] args)
        {
            Engine.Initialize();
            gameManager = GameManager.GetInstance();
            startTime = DateTime.Now;

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
            DeltaTime();
            gameManager.Update();
        }

        static void Render()
        {
            Engine.Clear();
            gameManager.Render();
            Engine.Show();
            
        }

        static void DeltaTime()
        {
            var currentTime = (float)(DateTime.Now - startTime).TotalSeconds;
            deltaTime = currentTime - lastFrameTime;
            lastFrameTime = currentTime;
        }
    }
}