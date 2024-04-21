namespace libs;

public sealed class InputHandler{

    private static InputHandler? _instance;
    private GameEngine engine;

    public static InputHandler Instance {
        get{
            if(_instance == null)
            {
                _instance = new InputHandler();
            }
            return _instance;
        }
    }

    private InputHandler() {
        //INIT PROPS HERE IF NEEDED
        engine = GameEngine.Instance;
    }

    public void Handle(ConsoleKeyInfo keyInfo)
    {
        GameObject focusedObject = engine.GetFocusedObject();
        if (focusedObject == null) return;

        // Proposed new positions based on key press
        int newX = focusedObject.PosX;
        int newY = focusedObject.PosY;

        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                newY--;
                break;
            case ConsoleKey.DownArrow:
                newY++;
                break;
            case ConsoleKey.LeftArrow:
                newX--;
                break;
            case ConsoleKey.RightArrow:
                newX++;
                break;
        }

        // Check if the new position is walkable before moving
        if (engine.GetMap().IsPositionWalkable(newX, newY))
        {
            focusedObject.Move(newX - focusedObject.PosX, newY - focusedObject.PosY);
        }
    }
}