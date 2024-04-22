namespace libs;

using System;
using System.Collections.Generic;
using System.Linq;

public sealed class GameEngine {
    private static GameEngine? _instance;
    public GameObjectFactory gameObjectFactory;  

    public GameState CurrentGameState { get; private set; } = GameState.Running;

    public static GameEngine Instance {
        get {
            if (_instance == null) {
                _instance = new GameEngine();
            }
            return _instance;
        }
    }

    private GameEngine() {
        gameObjectFactory = new GameObjectFactory();
    }

    private GameObject? _focusedObject;
    private List<GameObject> gameObjects = new List<GameObject>();
    private Map map = new Map();

    public Map GetMap() {
        return map;
    }

    public GameObject GetFocusedObject() {
        return _focusedObject;
    }

    public void Setup() {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        dynamic gameData = FileHandler.ReadJson();
        map.MapWidth = gameData.map.width;
        map.MapHeight = gameData.map.height;

        foreach (var gameObject in gameData.First.gameObjects) {
            AddGameObject(CreateGameObject(gameObject));
        }
        _focusedObject = gameObjects.OfType<Player>().First();
    }

    public void Render() {
        Console.Clear();
        map.Initialize();
        PlaceGameObjects();
        for (int i = 0; i < map.MapHeight; i++) {
            for (int j = 0; j < map.MapWidth; j++) {
                DrawObject(map.Get(i, j));
            }
            Console.WriteLine();
        }
    }

    public GameObject CreateGameObject(dynamic obj) {
        return gameObjectFactory.CreateGameObject(obj);
    }

    public void AddGameObject(GameObject gameObject) {
        gameObjects.Add(gameObject);
        if (gameObject.Type == GameObjectType.Box) {
            gameObjectFactory.IncrementAmountOfBoxes();
        }
    }

    private void PlaceGameObjects() {
        foreach (var obj in gameObjects) {
            map.Set(obj);
            if (obj is Box && map.Get(obj.PosY, obj.PosX).Type == GameObjectType.Target) {
                gameObjectFactory.DecrementAmountOfBoxes();
                 for(int i = 0; i < 3000; i++) 
{							
  Console.WriteLine("Hi from place");	
}	
            }
        }
    }

    public void CheckWinCondition() {
        if (gameObjectFactory.AmountOfBoxes == 5) {
            CurrentGameState = GameState.Won;
         for(int i = 0; i < 30; i++) 
{							
  Console.WriteLine("u won !!");	
}	
        }
    }

    private void DrawObject(GameObject gameObject) {
        Console.ResetColor();
        if (gameObject != null) {
            Console.ForegroundColor = gameObject.Color;
            Console.Write(gameObject.CharRepresentation);
        } else {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(' ');
        }
    }
}
