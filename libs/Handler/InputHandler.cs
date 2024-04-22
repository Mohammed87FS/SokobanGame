namespace libs;

public sealed class InputHandler {
    private static InputHandler? _instance;
    private GameEngine engine;

    public static InputHandler Instance {
        get {
            if (_instance == null) {
                _instance = new InputHandler();
            }
            return _instance;
        }
    }

    private InputHandler() {
        engine = GameEngine.Instance;
    }

    public void Handle(ConsoleKeyInfo keyInfo) {
        // Check game state before processing inputs
        if (GameEngine.Instance.CurrentGameState == GameState.Won) {
            Console.WriteLine("The game is won and no input will be processed.");
            return;  // Early return if the game has already been won
        }

        // Retrieve the focused object only if the game state allows interaction
        GameObject focusedObject = engine.GetFocusedObject();

        if (focusedObject != null) {
            switch (keyInfo.Key) {
                case ConsoleKey.UpArrow:
                    focusedObject.Move(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    focusedObject.Move(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    focusedObject.Move(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    focusedObject.Move(1, 0);
                    break;
                default:
                    break;
            }
        }
    }
}
