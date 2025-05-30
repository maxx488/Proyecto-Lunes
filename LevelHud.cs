﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class LevelHud
    {
        private Font font = new Font("assets/fonts/PressStart2P.ttf", 12);
        private int destroyed;
        private int enemyCount;
        private int tries;
        private Image triesImage;
        private PowerUpStack activePowerUps;
        private AnimationController powerAnimation1;
        private AnimationController powerAnimation2;
        private AnimationController powerAnimation3;
        private Image powerStack;
        private int[] powerArray;
        private Transform powerUpLocation1 = new Transform(new Vector2(15, 689), new Vector2(0, 0));
        private Transform powerUpLocation2 = new Transform(new Vector2(15, 597), new Vector2(0, 0));
        private Transform powerUpLocation3 = new Transform(new Vector2(15, 505), new Vector2(0, 0));
        private EnemyQueue currentQueue;
        private int faction;
        private Image nextEnemy1;
        private Image nextEnemy2;
        private Image nextEnemy3;
        private Image nextEnemy4;
        private Image nextEnemy5;
        private Image enemyQueue;
        private int[] queueArray;
        private Vector2 nextEnemyLocation1 = new Vector2(785, 702);
        private Vector2 nextEnemyLocation2 = new Vector2(825, 702);
        private Vector2 nextEnemyLocation3 = new Vector2(865, 702);
        private Vector2 nextEnemyLocation4 = new Vector2(905, 702);
        private Vector2 nextEnemyLocation5 = new Vector2(945, 702);

        public LevelHud(PowerUpStack powerups, EnemyQueue queue, int faction, int tries, int destroyed, int enemyCount)
        {
            powerStack = Engine.LoadImage("assets/hud/powerstack.png");
            enemyQueue = Engine.LoadImage("assets/hud/enemyqueue.png");
            this.activePowerUps = powerups;
            this.currentQueue = queue;
            this.faction = faction;
            this.tries = tries;
            this.enemyCount = enemyCount;
            this.destroyed = destroyed;
        }

        public int Tries
        {
            get
            {
                return tries;
            }
            set
            {
                tries = value;
            }
        }

        public int Destroyed
        {
            get
            {
                return destroyed;
            }
            set
            {
                destroyed = value;
            }
        }

        public void Update()
        {
            if (powerAnimation1 != null)
            {
                powerAnimation1.Update();
            }
            if (powerAnimation2 != null)
            {
                powerAnimation2.Update();
            }
            if (powerAnimation3 != null)
            {
                powerAnimation3.Update();
            }
            triesImage = Engine.LoadImage($"assets/hud/tries/{tries}.png");
        }

        public void DisplayStackUpdate()
        {
            powerArray = activePowerUps.ShowStack(powerArray);
            if (powerArray != null)
            {
                for (int i = 0; i < powerArray.Length; i++)
                {
                    if (i == 0)
                    {
                        if (powerArray[i] != 0)
                        {
                            powerAnimation1 = new AnimationController(powerUpLocation1, $"assets/animations/hud/powerup/{powerArray[i]}/", 15, 0.12f);
                        }
                    }
                    if (i == 1)
                    {
                        if (powerArray[i] != 0)
                        {
                            powerAnimation2 = new AnimationController(powerUpLocation2, $"assets/animations/hud/powerup/{powerArray[i]}/", 15, 0.12f);
                        }
                    }
                    if (i == 2)
                    {
                        if (powerArray[i] != 0)
                        {
                            powerAnimation3 = new AnimationController(powerUpLocation3, $"assets/animations/hud/powerup/{powerArray[i]}/", 15, 0.12f);
                        }
                    }
                }
                if (powerArray.Length == 1)
                {
                    powerAnimation2 = null;
                    powerAnimation3 = null;
                }
                if (powerArray.Length == 0)
                {
                    powerAnimation1 = null;
                    powerAnimation2 = null;
                    powerAnimation3 = null;
                }
                if (powerArray.Length == 2)
                {
                    powerAnimation3 = null;
                }
            }
        }

        public void DisplayQueueUpdate()
        {
            queueArray = currentQueue.ShowQueue(queueArray);
            if (queueArray != null)
            {
                for (int i = 9; i >= queueArray.Length/2; i--)
                {
                    switch (i)
                    {
                        case 5:
                            nextEnemy5 = Engine.LoadImage($"assets/hud/enemyqueue/{faction}/{queueArray[i]}.png");
                            break;
                        case 6:
                            nextEnemy4 = Engine.LoadImage($"assets/hud/enemyqueue/{faction}/{queueArray[i]}.png");
                            break;
                        case 7:
                            nextEnemy3 = Engine.LoadImage($"assets/hud/enemyqueue/{faction}/{queueArray[i]}.png");
                            break;
                        case 8:
                            nextEnemy2 = Engine.LoadImage($"assets/hud/enemyqueue/{faction}/{queueArray[i]}.png");
                            break;
                        case 9:
                            nextEnemy1 = Engine.LoadImage($"assets/hud/enemyqueue/{faction}/{queueArray[i]}.png");
                            break;
                    }

                }
            }
        }

        public void Render()
        {
            Engine.Draw(powerStack, 5, 485);
            Engine.Draw(triesImage, 97, 731);
            if (enemyCount != 1)
            {
                Engine.Draw(enemyQueue, 697, 676);
                Engine.DrawText($"Enemies Destroyed: {destroyed}/{enemyCount}", 0, 5, 255, 255, 255, font);
                if (nextEnemy1 != null)
                {
                    Engine.Draw(nextEnemy1, nextEnemyLocation1.X, nextEnemyLocation1.Y);
                }
                if (nextEnemy2 != null)
                {
                    Engine.Draw(nextEnemy2, nextEnemyLocation2.X, nextEnemyLocation2.Y);
                }
                if (nextEnemy3 != null)
                {
                    Engine.Draw(nextEnemy3, nextEnemyLocation3.X, nextEnemyLocation3.Y);
                }
                if (nextEnemy4 != null)
                {
                    Engine.Draw(nextEnemy4, nextEnemyLocation4.X, nextEnemyLocation4.Y);
                }
                if (nextEnemy5 != null)
                {
                    Engine.Draw(nextEnemy5, nextEnemyLocation5.X, nextEnemyLocation5.Y);
                }
            }
            if (powerAnimation1 != null)
            {
                powerAnimation1.Render();
            }
            if (powerAnimation2 != null)
            {
                powerAnimation2.Render();
            }
            if (powerAnimation3 != null)
            {
                powerAnimation3.Render();
            }
        }
    }
}
