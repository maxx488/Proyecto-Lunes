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
        private int power;
        private float speed;

        public EnemyData(int typ)
        {
            switch (typ)
            {
                case 1:
                    sizeX = 64;
                    sizeY = 64;
                    shootPosX = new int[1] { 27 };
                    shootPosY = 65;
                    power = 2;
                    speed = 250;
                    break;
                case 2:
                    sizeX = 64;
                    sizeY = 64;
                    shootPosX = new int[1] { 27 };
                    shootPosY = 65;
                    power = 3;
                    speed = 225;
                    break;
                case 3:
                    sizeX = 128;
                    sizeY = 64;
                    shootPosX = new int[2] { 0, 118 };
                    shootPosY = 65;
                    power = 5;
                    speed = 200;
                    break;
                case 4:
                    sizeX = 44;
                    sizeY = 52;
                    shootPosX = null;
                    shootPosY = 0;
                    power = 1;
                    speed = 500;
                    break;
            }

        }

        public int SizeX => sizeX;

        public int SizeY => sizeY;

        public int[] ShootPosX => shootPosX;

        public int ShootPosY => shootPosY;

        public int Power => power;

        public float Speed => speed;
    }
}
