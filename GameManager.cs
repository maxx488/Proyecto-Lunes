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
        private static GameState state;
        private static GameStats stats;
        private static SoundManager soundManager = new SoundManager();
        private Level level;
        private int timesWon;
        private bool bossLevel;
        private Font font = new Font("assets/fonts/PressStart2P.ttf", 38);
        private string menuImage = "assets/gamestate/menu.png";
        private string winImage = "assets/gamestate/win.png";
        private string loseImage = "assets/gamestate/lose.png";
        private Renderer renderer = new Renderer(new Transform(new Vector2(0, 0), new Vector2(0, 0)), "assets/gamestate/menu.png");

        private GameManager() 
        {
            state = GameState.menu;
            soundManager.SetPlayBackground("assets/sounds/menu.wav");
        }

        private static GameManager _instance;

        public static GameStats Stats => stats;

        public static SoundManager GetSoundManager => soundManager;

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
            switch (state)
            {
                case GameState.menu:
                    if (Engine.GetKey(Engine.KEY_ESP))
                    {
                        timesWon = 0;
                        bossLevel = false;
                        stats = new GameStats();
                        level = new Level(timesWon + 1, bossLevel); // faccion enemiga
                        soundManager.StopBackground();
                        state = GameState.game;
                    }
                    if (Engine.GetKey(Engine.KEY_ESC))
                    {
                        Environment.Exit(0);
                    }
                    break;
                case GameState.game:
                    level.Input();
                    if (Engine.GetKey(Engine.KEY_ESC))
                    {
                        Environment.Exit(0);
                    }
                    break;
                case GameState.win:
                    if (Engine.GetKey(Engine.KEY_ENT))
                    {
                        if (bossLevel == true)
                        {
                            timesWon++;
                            bossLevel = false;
                        }
                        else
                        {
                            bossLevel = true;
                        }
                        if (timesWon > 1) //depende de cantidad de facciones por ahora, mas adelante cambiara la logica de elegir nivel
                        {
                            renderer.SetImage(menuImage);
                            soundManager.SetPlayBackground("assets/sounds/menu.wav");
                            state = GameState.menu;
                        }
                        else
                        {
                            level = new Level(timesWon + 1, bossLevel); // faccion enemiga
                            soundManager.StopBackground();
                            state = GameState.game;
                        }
                    }
                    if (Engine.GetKey(Engine.KEY_ESC))
                    {
                        Environment.Exit(0);
                    }
                    break;
                case GameState.lose:
                    if (Engine.GetKey(Engine.KEY_ENT))
                    {
                        renderer.SetImage(menuImage);
                        soundManager.SetPlayBackground("assets/sounds/menu.wav");
                        state = GameState.menu;
                    }
                    if (Engine.GetKey(Engine.KEY_ESC))
                    {
                        Environment.Exit(0);
                    }
                    break;
            }
        }

        public void Update()
        {
            switch (state)
            {
                case GameState.menu:
                    break;
                case GameState.game:
                    level.Update();
                    if (level.GetTries == 0)
                    {
                        renderer.SetImage(loseImage);
                        soundManager.SetPlayBackground("assets/sounds/lose.wav");
                        state = GameState.lose;
                    }
                    if (level.EnemiesDestroyed == true)
                    {
                        stats.DisplayStats();
                        renderer.SetImage(winImage);
                        soundManager.SetPlayBackground("assets/sounds/win.wav");
                        state = GameState.win;
                    }
                    break;
                case GameState.win:
                    break;
                case GameState.lose:
                    break;
            }
        }

        public void Render()
        {
            switch (state)
            {
                case GameState.menu:
                    renderer.Render();
                    Engine.DrawText("Press SPACE To Start", 120, 675, 255, 10, 0, font);
                    break;
                case GameState.game:
                    level.Render();
                    break;
                case GameState.win:
                    renderer.Render();
                    stats.Render();
                    Engine.DrawText("Press ENTER To Continue", 75, 675, 255, 10, 0, font);
                    break;
                case GameState.lose:
                    renderer.Render();
                    Engine.DrawText("Press ENTER To Continue", 75, 675, 255, 10, 0, font);
                    break;
            }
        }
    }
}
