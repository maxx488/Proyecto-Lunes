using System;
using System.Collections.Generic;
using System.Dynamic;
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
        private Map map;
        private Level level;
        private int timesWon;
        private int factions = 2; //Cantidad de facciones enemigas.
        private bool bossLevel;
        private int enemyCount;
        private int[] enemies = new int[2];
        private int enemiesIndex = 0;
        private Font font = new Font("assets/fonts/PressStart2P.ttf", 38);
        private Font fontControls = new Font("assets/fonts/PressStart2P.ttf", 12);
        private string menuImage = "assets/gamestate/menu.png";
        private string winImage = "assets/gamestate/win.png";
        private string loseImage = "assets/gamestate/lose.png";
        private Renderer renderer = new Renderer(new Transform(new Vector2(0, 0), new Vector2(0, 0)), "assets/gamestate/menu.png");

        private GameManager() 
        {
            state = GameState.menu;
            soundManager.SetPlayBackground("assets/sounds/menu.wav");
            map = new Map();
            enemyCount = 0;
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
                        state = GameState.map;
                    }
                    if (Engine.GetKey(Engine.KEY_ESC))
                    {
                        Environment.Exit(0);
                    }
                    break;
                case GameState.map:
                    if (Engine.GetKey(Engine.KEY_1))
                    {
                        enemyCount = map.CaminosPeso[1];
                        EnemiesPerLevel();
                        StartGame();
                    }
                    if (Engine.GetKey(Engine.KEY_2))
                    {
                        enemyCount = map.CaminosPeso[2];
                        EnemiesPerLevel();
                        StartGame();
                    }
                    if (Engine.GetKey(Engine.KEY_3))
                    {
                        enemyCount = map.CaminosPeso[3];
                        EnemiesPerLevel();
                        StartGame();
                    }
                    if (Engine.GetKey(Engine.KEY_4))
                    {
                        enemyCount = map.CaminosPeso[4];
                        EnemiesPerLevel();
                        StartGame();
                    }
                    if (Engine.GetKey(Engine.KEY_5))
                    {
                        enemyCount = map.CaminosPeso[5];
                        EnemiesPerLevel();
                        StartGame();
                    }
                    if (Engine.GetKey(Engine.KEY_6))
                    {
                        enemyCount = map.CaminosPeso[6];
                        EnemiesPerLevel();
                        StartGame();
                    }
                    if (Engine.GetKey(Engine.KEY_7))
                    {
                        enemyCount = map.CaminosPeso[7];
                        EnemiesPerLevel();
                        StartGame();
                    }
                    if (Engine.GetKey(Engine.KEY_8))
                    {
                        enemyCount = map.CaminosPeso[8];
                        EnemiesPerLevel();
                        StartGame();
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
                            enemiesIndex++;
                        }
                        else
                        {
                            bossLevel = true;
                        }
                        if (timesWon == factions)
                        {
                            renderer.SetImage(menuImage);
                            soundManager.SetPlayBackground("assets/sounds/menu.wav");
                            state = GameState.menu;
                        }
                        else
                        {
                            level = new Level(timesWon + 1, bossLevel, enemies[enemiesIndex]);
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
                    Engine.DrawText("W", 35, 15, 255, 255, 255, fontControls);
                    Engine.DrawText("ASD", 23, 30, 255, 255, 255, fontControls);
                    Engine.DrawText("->Move", 58, 23, 255, 255, 255, fontControls);
                    Engine.DrawText("SPACE->Shoot", 10, 50, 255, 255, 255, fontControls);
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
                case GameState.map:
                    map.Render();
                    break;
            }
        }

        private void EnemiesPerLevel()
        {
            enemyCount -= 2;
            if (enemyCount % 2 == 1)
            {
                enemies[0] = enemyCount / 2;
                enemies[1] = (enemyCount / 2) + 1;
            }
            else
            {
                enemies[0] = enemyCount / 2;
                enemies[1] = enemyCount / 2;
            }
        }

        private void StartGame()
        {
            timesWon = 0;
            enemiesIndex = 0;
            bossLevel = false;
            stats = new GameStats();
            level = new Level(timesWon + 1, bossLevel, enemies[enemiesIndex]); // faccion enemiga definida por timeswon
            soundManager.StopBackground();
            state = GameState.game;
        }
    }
}
