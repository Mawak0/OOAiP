namespace SpaceBattle.Lib;

public class Polygon
{
    public List<Point> Points { get; }

    public Polygon(IEnumerable<Point> points)
    {
        Points = points.ToList();
    }

    public IEnumerable<(Point start, Point end)> Edges()
    {
        for (int i = 0; i < Points.Count; i++)
        {
            yield return (Points[i], Points[(i + 1) % Points.Count]);
        }
    }
}
