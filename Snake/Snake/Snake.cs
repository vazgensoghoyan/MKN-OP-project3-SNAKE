namespace Snake;

public static class SnakeGame
{
    private static List<Coords> _snakeCoords;
    private static List<Coords> _applesCoords;
    private static EDirection _direction;

    private static Int32 T;

    private const Int32 _GroundHeight = 15;
    private const Int32 _GroundWidth = 15;

    private const Int32 _MaxAppleCount = 10;
    private const Int32 _AppleAppearanceFrequency = 6;

    static SnakeGame()
    {
        _snakeCoords = [ new Coords(7, 0) ];
        _applesCoords = [];
        _direction = EDirection.Right;

        T = 600;
    }

    public static void StartGame()
    {
        Console.Clear();
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        Console.CursorVisible = false;

        var readingThread = new Thread(Read);
        var drawingThread = new Thread(Draw);

        readingThread.Start();
        drawingThread.Start();
    }

    private static void Draw()
    {
        var frameCount = 0;
        
        while (true)
        {
            var newCoords = _snakeCoords[^1] + Coords.DirectionToCoords( _direction );
            
            if ( _applesCoords.Contains( newCoords ) )
            {
                _applesCoords.Remove( newCoords );
                _snakeCoords.Add( newCoords );
            }
            else
            {
                _snakeCoords.RemoveAt( 0 );
                _snakeCoords.Add( newCoords );
            }

            PrintAll();

            frameCount++;
            if ( frameCount > _AppleAppearanceFrequency )
            {
                if ( _applesCoords.Count() < _MaxAppleCount )
                    AddRandomApple();
                frameCount = 0;
            }

            Thread.Sleep(T);
        }
    }

    private static void AddRandomApple()
    {
        var rnd = new Random();

        Int32 x, y;

        do
        {
            x = rnd.Next(0, _GroundWidth - 5);
            y = rnd.Next(0, _GroundHeight - 5);
        } 
        while ( _snakeCoords.Contains( new Coords(x, y) ));

        _applesCoords.Add( new Coords(x, y) );
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
        for (int i = 0; i < _GroundWidth; i++)
        {
            Console.Write("══");
        }
        Console.Write("╗\n");

        for (int i = 0; i < _GroundHeight; i++)
        {
            Console.Write("║");
            for (int j  = 0; j < _GroundWidth; j++)
            {
                if ( _snakeCoords.Contains( new Coords(i, j) ) )
                    Console.Write("●");
                else if ( _applesCoords.Contains( new Coords(i, j) ) )
                    Console.Write("○");
                else
                    Console.Write(" ");

                Console.Write(' ');
            }
            Console.Write("║\n");
        }

        Console.Write("╚");
        for (int i = 0; i < _GroundWidth; i++)
        {
            Console.Write("══");
        }
        Console.Write("╝");
    }
}
