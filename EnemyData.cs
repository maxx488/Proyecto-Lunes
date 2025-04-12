using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class EnemyData
    {
        private int sizeX;
        private int sizeY;
        private int[] shootPosX;
        private int shootPosY;

        public EnemyData(int typ)
        {
            if (typ == 1 || typ == 2)
            {
                sizeX = 64;
                sizeY = 64;
                shootPosX = new int[1] {27};
                shootPosY = 65;
            }
            if (typ == 3)
            {
                sizeX = 128;
                sizeY = 64;
                shootPosX = new int[2] {0, 118};
                shootPosY = 65;
            }

        }

        public int SizeX => sizeX;

        public int SizeY => sizeY;

        public int[] ShootPosX => shootPosX;

        public int ShootPosY => shootPosY;
    }
}
