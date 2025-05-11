namespace SpaceBattle.Lib;

public class Point
{
    public double X { get; }
    public double Y { get; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public static double Cross(Point p1, Point p2)
    {
        return p1.X * p2.Y - p1.Y * p2.X;
    }

    public static Point operator -(Point p1, Point p2)
    {
        return new Point(p1.X - p2.X, p1.Y - p2.Y);
    }

    public static Point operator +(Point p1, Point p2)
    {
        return new Point(p1.X + p2.X, p1.Y + p2.Y);
    }

    public static Point operator *(double scalar, Point p)
    {
        return new Point(p.X * scalar, p.Y * scalar);
    }

    public Point Normalize()
    {
        double len = Math.Sqrt(X * X + Y * Y);
        return new Point(X / len, Y / len);
    }
}