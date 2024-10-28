namespace Snake;

public readonly struct Coords
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

    public override string ToString() => $"({X}, {Y})";
}