namespace Snake;

public class Coords
{
    public Coords(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; }
    public double Y { get; }

    public static Coords operator +(Coords a, Coords b) => new Coords(a.X + b.X, a.Y + b.Y);

    public static bool operator ==(Coords a, Coords b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Coords a, Coords b) => !(a == b);

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Coords)
            return false;

        return this == (Coords)obj;
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public static Coords DirectionToCoords(EDirection direction)
    {
        Int32 horizontally = 0;
        Int32 vertically = 0;

        if ( direction is EDirection.Left or EDirection.Right )
            vertically = (direction is EDirection.Right ? 1 : -1);

        if ( direction is EDirection.Up or EDirection.Down )
            horizontally = (direction is EDirection.Up ? 1 : -1);

        return new Coords( horizontally, vertically );
    }

    public static bool AreOppositeDirections(EDirection dir1, EDirection dir2) 
    {
        return DirectionToCoords( dir1 ) + DirectionToCoords( dir2 ) == new Coords( 0, 0 );
    }

    public override string ToString() => $"({X}, {Y})";
}