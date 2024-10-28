namespace Snake;

public static class SnakeGame
{
    private static Coords[] _snakeCoords;
    private static EDirection _direction;

    private static Int32 T;

    private const Int32 _groundHeight = 20;
    private const Int32 _groundWidth = 30;

    static SnakeGame()
    {
        _snakeCoords = [ new Coords(7, 0) ];
        _direction = EDirection.Right;

        T = 600;
    }

    public static void StartGame()
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        Console.CursorVisible = false;

        var readingThread = new Thread(Read);
        var drawingThread = new Thread(Draw);

        readingThread.Start();
        drawingThread.Start();
    }

    private static void Draw()
    {
        while (true)
        {
            _snakeCoords[0] += Coords.DirectionToCoords( _direction );

            PrintAll();

            Thread.Sleep(T);
        }
    }

    private static void Read()
    {

        while (true)
        {
            var newDirection = _direction;

            switch ( Console.ReadKey(true).Key )
            {
                case ConsoleKey.W:
                    newDirection = EDirection.Down;
                    break;
                case ConsoleKey.D:
                    newDirection = EDirection.Right;
                    break;
                case ConsoleKey.S:
                    newDirection = EDirection.Up;
                    break;
                case ConsoleKey.A:
                    newDirection = EDirection.Left;
                    break;
            }

            if ( !Coords.AreOppositeDirections(newDirection, _direction) )
                _direction = newDirection;
        }
    }

    public static void PrintAll()
    {
        Console.SetCursorPosition(0, 0);

        Console.Write("╔");
        for (int i = 0; i < _groundWidth; i++)
        {
            Console.Write("══");
        }
        Console.Write("╗\n");

        for (int i = 0; i < _groundHeight; i++)
        {
            Console.Write("║");
            for (int j  = 0; j < _groundWidth; j++)
            {
                Console.Write( _snakeCoords.Contains( new Coords(i, j) ) ? '●' : ' ' );
                Console.Write(' ');
            }
            Console.Write("║\n");
        }

        Console.Write("╚");
        for (int i = 0; i < _groundWidth; i++)
        {
            Console.Write("══");
        }
        Console.Write("╝");
    }
}
