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
        var result = Points.Select((point, i) => (point, Points[(i + 1) % Points.Count]));
        return result;

    }
}
