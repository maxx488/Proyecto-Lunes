using System;
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
        private PowerUpStack activePowerUps;
        private AnimationController powerAnimation1;
        private AnimationController powerAnimation2;
        private AnimationController powerAnimation3;
        private Image powerStack;
        private int[] powerArray;
        private Transform powerUpLocation1 = new Transform(new Vector2(15, 689));
        private Transform powerUpLocation2 = new Transform(new Vector2(15, 597));
        private Transform powerUpLocation3 = new Transform(new Vector2(15, 505));

        public LevelHud(PowerUpStack powerups)
        {
            powerStack = Engine.LoadImage("assets/powerstack.png");
            this.activePowerUps = powerups;
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

        public void Render()
        {
            Engine.Draw(powerStack, 5, 495);
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
