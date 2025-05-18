using Xunit;

namespace SpaceBattle.Lib;

public class PolygonTests
{
    [Fact]
    public void CorrectInitTest()
    {
        var points = new List<Point>
        {
            new Point(0, 0),
            new Point(1, 0),
            new Point(1, 1),
            new Point(0, 1)
        };

        var polygon = new Polygon(points);

        Assert.Equal(points, polygon.Points);
    }

    [Fact]
    public void ReturnsCorrectEdgesTest()
    {
        var points = new List<Point>
        {
            new Point(0, 0),
            new Point(1, 0),
            new Point(1, 1),
            new Point(0, 1)
        };
        var expectedEdges = new List<(Point start, Point end)>
        {
            (points[0], points[1]),
            (points[1], points[2]),
            (points[2], points[3]),
            (points[3], points[0])
        };

        var polygon = new Polygon(points);
        var edges = polygon.Edges().ToList();

        Assert.Equal(expectedEdges, edges);
    }

    [Fact]
    public void LastEdgeConnectsLastAndFirstVertexTest()
    {
        var points = new List<Point>
        {
            new Point(0, 0),
            new Point(1, 0),
            new Point(1, 1),
            new Point(0, 1)
        };

        var polygon = new Polygon(points);
        var lastEdge = polygon.Edges().Last();

        Assert.Equal(points.Last(), lastEdge.start);
        Assert.Equal(points.First(), lastEdge.end);
    }
}
