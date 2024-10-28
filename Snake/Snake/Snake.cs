namespace Snake;

public static class SnakeGame
{
    private static List<Coords> _snakeCoords;
    private static List<Coords> _applesCoords;

    private static EDirection _direction;
    private static EDirection _readDirection;

    private static bool _gameIsOn;

    private static Int32 T;

    private const Int32 GroundHeight = 10;
    private const Int32 GroundWidth = 10;

    private const Int32 MaxAppleCount = 10;
    private const Int32 AppleAppearanceFrequency = 6;

    static SnakeGame()
    {
        _snakeCoords = [ new Coords(7, 0) ];
        _applesCoords = [];

        _direction = EDirection.Right;
        _readDirection = EDirection.Right;

        T = 600;
    }

    public static void StartGame()
    {
        Console.Clear();
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        Console.CursorVisible = false;

        _gameIsOn = true;

        var readingThread = new Thread(Read);
        var drawingThread = new Thread(Draw);

        readingThread.Start();
        drawingThread.Start();
    }

    private static void Draw()
    {
        var frameCount = 0;
        Coords newCoords;
        
        while (true)
        {
            if ( !_gameIsOn ) break;

            if ( !Coords.AreOppositeDirections( _readDirection, _direction ) )
                _direction = _readDirection;

            newCoords = _snakeCoords[^1] + Coords.DirectionToCoords( _direction );

            newCoords = new Coords( (GroundWidth + newCoords.X) % GroundWidth, 
                                    (GroundHeight + newCoords.Y) % GroundHeight );

            if ( _snakeCoords.Contains( newCoords ) )
            {
                _gameIsOn = false;
                break;
            }

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
            if ( frameCount > AppleAppearanceFrequency )
            {
                if ( _applesCoords.Count() < MaxAppleCount )
                    AddRandomApple();
                frameCount = 0;
            }

            Thread.Sleep(T);
        }

        Console.Clear();
        Console.WriteLine("Thanks for the game!");
    }

    private static void AddRandomApple()
    {
        var rnd = new Random();

        Int32 x, y;

        do
        {
            x = rnd.Next(0, GroundWidth - 5);
            y = rnd.Next(0, GroundHeight - 5);
        } 
        while ( _snakeCoords.Contains( new Coords(x, y) ) );

        _applesCoords.Add( new Coords(x, y) );
    }

    private static void Read()
    {
        while (true)
        {
            if ( !_gameIsOn ) return;

            switch ( Console.ReadKey(true).Key )
            {
                case ConsoleKey.W:
                    _readDirection = EDirection.Down;
                    break;
                case ConsoleKey.D:
                    _readDirection = EDirection.Right;
                    break;
                case ConsoleKey.S:
                    _readDirection = EDirection.Up;
                    break;
                case ConsoleKey.A:
                    _readDirection = EDirection.Left;
                    break;
                case ConsoleKey.Escape:
                    _gameIsOn = false;
                    break;
            };
        }
    }

    public static void PrintAll()
    {
        Console.SetCursorPosition(0, 0);

        Console.Write("╔");
        for (int i = 0; i < 2 * GroundWidth + 1; i++)
        {
            Console.Write("═");
        }
        Console.Write("╗\n");

        for (int i = 0; i < GroundHeight; i++)
        {
            Console.Write("║ ");
            for (int j  = 0; j < GroundWidth; j++)
            {
                if ( _snakeCoords.Contains( new Coords(i, j) ) )
                {
                    if ( _snakeCoords[^1] == new Coords(i, j) )
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("●");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else Console.Write("●");
                }
                else if ( _applesCoords.Contains( new Coords(i, j) ) )
                    Console.Write("○");
                else
                    Console.Write(" ");

                Console.Write(' ');
            }
            Console.Write("║\n");
        }

        Console.Write("╚");
        for (int i = 0; i < 2 * GroundWidth + 1; i++)
        {
            Console.Write("═");
        }
        Console.Write("╝");
    }
}
