namespace libs;

public class Box : GameObject
{

    private GameObjectFactory gameObjectFactory;
    private int targetsLeft;

    public Map map = GameEngine.Instance.GetMap();

    public Box() : base()
    {
        this.gameObjectFactory = GameEngine.Instance.gameObjectFactory as GameObjectFactory;
        this.targetsLeft = gameObjectFactory.AmountOfBoxes;
        Type = GameObjectType.Box;  // Assuming this was a mistake earlier setting it to Player
        CharRepresentation = '○';
        Color = ConsoleColor.DarkGreen;
    }

    public override void Move(int dx, int dy)
    {
        int goToX = PosX + dx;
        int goToY = PosY + dy;

        GameObject? potentialTarget = map.Get(goToY, goToX);


        // Check if the box is being moved onto a target
        if (potentialTarget != null && potentialTarget.Type == GameObjectType.Target)
        {

            for (int i = 0; i < 300; i++)
            {
                Console.WriteLine(potentialTarget.Type + "uooooooooooooooooooooo");
            }


            if (map.Get(PosY, PosX).Type != GameObjectType.Target)
            {
                gameObjectFactory.IncrementAmountOfBoxes();
          GameEngine.Instance.CheckWinCondition();

            }
        }
        else
        {

            if (map.Get(PosY, PosX).Type == GameObjectType.Target)
            {
                gameObjectFactory.DecrementAmountOfBoxes();


            }
        }


        base.Move(dx, dy);
    }
}
