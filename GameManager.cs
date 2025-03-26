using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class GameManager
    {
        private Level level;

        private GameManager() 
        {
            level = new Level();
        }

        private static GameManager _instance;

        public static GameManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }

        public void Input()
        {
            level.Input();
        }

        public void Update()
        {
            level.Update();
            if (level.GetPlayerHealth == 0)
            {
                Console.WriteLine("Pulsa Enter para salir.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public void Render()
        {
            level.Render();
        }
    }
}
