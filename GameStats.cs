using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class GameStats
    {
        private Font font = new Font("assets/fonts/PressStart2P.ttf", 32);
        private int[][] arrayCount = new int[][] { new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 }, new int[2] { 0, 0 } };
        private Dictionary<int, int> destroyedCount = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 } };
        private Sorter sorter = new Sorter();

        public void SubStats(EnemyManager currentManager)
        {
            currentManager.OnEnemyDestroyed += AddStats;
        }

        public void UnSubStats(EnemyManager currentManager)
        {
            currentManager.OnEnemyDestroyed -= AddStats;
        }

        public void AddStats(int x)
        {
            destroyedCount[x] += 1;
        }

        public void DisplayStats()
        {
            foreach (var ele in destroyedCount)
            {
                Console.WriteLine($"Key: {ele.Key}, Value: {ele.Value}");
                arrayCount[ele.Key-1][0] = ele.Key;
                arrayCount[ele.Key - 1][1] = ele.Value;
            }
            QuickSortArray(arrayCount);
        }

        public void QuickSortArray(int[][] array)
        {
            sorter.QuickSort(array, 0, arrayCount.Length - 1 );
            for (int i = 0; i < arrayCount.Length; i++)
            {
                Console.WriteLine($"Type: {arrayCount[i][0]}, Killed: {arrayCount[i][1]}");
            }
        }

        public void Render()
        {
            int x = 350;
            int y = 350;
            Engine.DrawText($"Enemies Destroyed", 250, 275, 255, 255, 255, font);
            for (int i = arrayCount.Length - 1; i > -1; i--)
            {
                switch (arrayCount[i][0])
                {
                    case 1:
                        Engine.DrawText($"Fighter: {arrayCount[i][1]}", x, y, 255, 255, 255, font);
                        break;
                    case 2:
                        Engine.DrawText($"Torpedo: {arrayCount[i][1]}", x, y, 255, 255, 255, font);
                        break;
                    case 3:
                        Engine.DrawText($"Bomber: {arrayCount[i][1]}", x, y, 255, 255, 255, font);
                        break;
                    case 4:
                        Engine.DrawText($"Kamikaze: {arrayCount[i][1]}", x, y, 255, 255, 255, font);
                        break;
                    case 5:
                        Engine.DrawText($"Boss: {arrayCount[i][1]}", x, y, 255, 255, 255, font);
                        break;
                }
                y += 50;
            }
        }
    }
}
