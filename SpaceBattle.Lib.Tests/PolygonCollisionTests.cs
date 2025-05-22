using Xunit;
namespace SpaceBattle.Lib.Tests;

public class PolygonCollisionTests
{
    [Fact]
    public void HasCollisionTest()
    {
        var firstPolygonPoints = new List<Point> { new Point(3, 5), new Point(6, 7), new Point(8, 3), new Point(6, 2) };
        var secondPolygonPoints = new List<Point> { new Point(3, 3), new Point(6, 5), new Point(5, 2) };
        var firstPolygon = new Polygon(firstPolygonPoints);
        var secondPolygon = new Polygon(secondPolygonPoints);
        var v = new Point(0, 1);

        var collision = CollisionDetector.FindIntersections(firstPolygon, secondPolygon, v);

        Assert.True(collision.Count > 0);
    }

    [Fact]
    public void HasNotCollisionTest()
    {
        var firstPolygonPoints = new List<Point> { new Point(3, 5), new Point(6, 7), new Point(8, 3), new Point(6, 2) };
        var secondPolygonPoints = new List<Point> { new Point(3, 3), new Point(6, 5), new Point(5, 2) };
        var firstPolygon = new Polygon(firstPolygonPoints);
        var secondPolygon = new Polygon(secondPolygonPoints);
        var v = new Point(1, 1);

        var collision = CollisionDetector.FindIntersections(firstPolygon, secondPolygon, v);

        Assert.False(collision.Count > 0);
    }
}
