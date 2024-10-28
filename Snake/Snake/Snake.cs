namespace Snake;

public static class SnakeGame
{
    private static Coords[] _snakeCoords;
    private static EDirection _direction;

    private static Int32 T;

    private const Int32 _groundHeight = 20;
    private const Int32 _groundWidth = 40;

    static SnakeGame()
    {
        _snakeCoords = [ new Coords(7, 0) ];
        _direction = EDirection.Right;

        T = 600;
    }

    public static void StartGame()
    {
        var readingThread = new Thread(Read);
        var drawingThread = new Thread(Draw);

        readingThread.Start();
        drawingThread.Start();
    }

    private static void Draw()
    {
        while (true)
        {
            Int32 horizontally = 0;
            Int32 vertically = 0;

            if (_direction is EDirection.Left or EDirection.Right)
                vertically = (_direction is EDirection.Right ? 1 : -1);

            if (_direction is EDirection.Up or EDirection.Down)
                horizontally = (_direction is EDirection.Up ? 1 : -1);

            _snakeCoords[0] += new Coords(horizontally, vertically);

            Print();

            Thread.Sleep(T);
        }
    }

    private static void Read()
    {
        while (true)
        {

        }
    }

    public static void Print()
    {
        Console.SetCursorPosition(0, 0);

        Console.Write("╔");
        for (int i = 0; i < _groundWidth; i++)
        {
            Console.Write("═");
        }
        Console.Write("╗\n");

        for (int i = 0; i < _groundHeight; i++)
        {
            Console.Write("║");
            for (int j  = 0; j < _groundWidth; j++)
            {
                Console.Write( _snakeCoords.Contains( new Coords(i, j) ) ? 'o' : ' ' );
            }
            Console.Write("║\n");
        }

        Console.Write("╚");
        for (int i = 0; i < _groundWidth; i++)
        {
            Console.Write("═");
        }
        Console.Write("╝");
    }
}
