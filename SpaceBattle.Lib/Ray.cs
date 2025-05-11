namespace SpaceBattle.Lib;

public class Ray
{
    public Point Origin { get; }
    public Point Direction { get; }

    public Ray(Point origin, Point direction)
    {
        Origin = origin;
        Direction = direction.Normalize();
    }
}