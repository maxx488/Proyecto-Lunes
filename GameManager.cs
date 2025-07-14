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
        private Font font = new Font("assets/fonts/PressStart2P.ttf", 38);
        private Font fontControls = new Font("assets/fonts/PressStart2P.ttf", 12);
        private string menuImage = "assets/gamestate/menu.png";
        private string winImage = "assets/gamestate/win.png";
        private string loseImage = "assets/gamestate/lose.png";
        private Renderer renderer = new Renderer(new Transform(new Vector2(0, 0), new Vector2(0, 0)), "assets/gamestate/menu.png");
        private Renderer pauseRenderer = new Renderer(new Transform(new Vector2(0, 0), new Vector2(0, 0)), "assets/gamestate/pause.png");
        private float pauseTimer = 0;
        private Random random = new Random();
        private int node = 8;

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
                        StartGame();
                    }
                    if (Engine.GetKey(Engine.KEY_ESC))
                    {
                        Environment.Exit(0);
                    }
                    break;
                case GameState.map:
                    if (Engine.GetKey(Engine.KEY_1))
                    {
                        switch (node)
                        {
                            case 8:
                                ContinueGame(4, 1);
                                break;
                            case 4:
                                ContinueGame(2, 3);
                                break;
                            case 12:
                                ContinueGame(10, 5);
                                break;
                            case 2:
                                ContinueGame(1, 7);
                                break;
                            case 6:
                                ContinueGame(5, 9);
                                break;
                            case 10:
                                ContinueGame(9, 11);
                                break;
                            case 14:
                                ContinueGame(13, 13);
                                break;
                        }
                    }
                    if (Engine.GetKey(Engine.KEY_2))
                    {
                        switch (node)
                        {
                            case 8:
                                ContinueGame(12, 2);
                                break;
                            case 4:
                                ContinueGame(6, 4);
                                break;
                            case 12:
                                ContinueGame(14, 6);
                                break;
                            case 2:
                                ContinueGame(3, 8);
                                break;
                            case 6:
                                ContinueGame(7, 10);
                                break;
                            case 10:
                                ContinueGame(11, 12);
                                break;
                            case 14:
                                ContinueGame(15, 14);
                                break;
                        }
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
                    if (pauseTimer > 0.5f)
                    {
                        if (Engine.GetKey(Engine.KEY_P))
                        {
                            state = GameState.pause;
                            pauseTimer = 0;
                        }
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
                        if (timesWon == factions)
                        {
                            renderer.SetImage(menuImage);
                            soundManager.SetPlayBackground("assets/sounds/menu.wav");
                            state = GameState.menu;
                            node = 8;
                            map.NodoActual = node;
                            map.Dijkstra();
                        }
                        else
                        {
                            soundManager.SetPlayBackground("assets/sounds/menu.wav");
                            state = GameState.map;
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
                        node = 8;
                        map.NodoActual = node;
                        map.Dijkstra();
                    }
                    if (Engine.GetKey(Engine.KEY_ESC))
                    {
                        Environment.Exit(0);
                    }
                    break;
                case GameState.pause:
                    if (Engine.GetKey(Engine.KEY_ESC))
                    {
                        Environment.Exit(0);
                    }
                    if (pauseTimer > 0.5f)
                    {
                        if (Engine.GetKey(Engine.KEY_P))
                        {
                            state = GameState.game;
                            pauseTimer = 0;
                        }
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
                    pauseTimer += Time.DeltaTime;
                    break;
                case GameState.win:
                    break;
                case GameState.lose:
                    break;
                case GameState.pause:
                    pauseTimer += Time.DeltaTime;
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
                case GameState.pause:
                    level.Render();
                    pauseRenderer.Render();
                    Engine.DrawText("Paused", 475, 370, 255, 255, 255, fontControls);
                    break;
            }
        }

        private void StartGame()
        {
            timesWon = 0;
            bossLevel = false;
            stats = new GameStats();
            level = new Level(timesWon + 1, bossLevel, random.Next(10, 30)); // faccion enemiga definida por timeswon
            soundManager.StopBackground();
            state = GameState.game;
        }

        private void ContinueGame(int nextNode, int arista)
        {
            level = new Level(timesWon + 1, bossLevel, map.AristasPeso[arista]);
            soundManager.StopBackground();
            state = GameState.game;
            node = nextNode;
            map.NodoActual = node;
            map.Dijkstra();
        }
    }
}
