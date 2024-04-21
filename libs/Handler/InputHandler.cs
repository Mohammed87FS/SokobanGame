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
       
        engine = GameEngine.Instance;
    }

    public void Handle(ConsoleKeyInfo keyInfo)
{
    GameObject focusedObject = engine.GetFocusedObject();
  
    if (focusedObject == null) return;

 
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

    GameObject? objectAtNewPosition = engine.GetMap().GetObjectAt(newX, newY);

  
    if (objectAtNewPosition != null && objectAtNewPosition.Type == GameObjectType.Box)
    {
      
        int boxNewX = objectAtNewPosition.PosX + (newX - focusedObject.PosX);
        int boxNewY = objectAtNewPosition.PosY + (newY - focusedObject.PosY);

        
        if (engine.GetMap().IsPositionWalkable(boxNewX, boxNewY))
        {
        
            objectAtNewPosition.Move(boxNewX - objectAtNewPosition.PosX, boxNewY - objectAtNewPosition.PosY);
            UpdateGameMap(objectAtNewPosition);

            
            focusedObject.Move(newX - focusedObject.PosX, newY - focusedObject.PosY);
            UpdateGameMap(focusedObject);
        }
    }
    else if (engine.GetMap().IsPositionWalkable(newX, newY))
    {
      
        focusedObject.Move(newX - focusedObject.PosX, newY - focusedObject.PosY);
        UpdateGameMap(focusedObject);
    }
}

private void UpdateGameMap(GameObject gameObject)
{

    engine.GetMap().Set(gameObject);
    engine.Render();
}



}

