using Xunit;
namespace SpaceBattle.Lib.Tests;

public class PolygonCollisionCheckerByRaysTest
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

        Assert.True(collision.Capacity > 0);
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

        Assert.False(collision.Capacity > 0);
    }

    // [Fact]
    // public void IsCollision_WhenRayMissesEdge_ReturnsFalse()
    // {
    //     // Arrange
    //     var staticPolygon = new List<Point> { new Point(1, 1), new Point(3, 5), new Point(5, 1) };
    //     var movingPolygon = new List<Point> { new Point(0, 0), new Point(0, 2), new Point(2, 2), new Point(2, 0) };
    //     var checker = new PolygonCollisionCheckerByRays(staticPolygon, movingPolygon);

    //     // Act
    //     var collision = checker.IsCollision(6, 6, -1, -1);

    //     // Assert
    //     Assert.False(collision);
    // }

    // [Fact]
    // public void IsCollision_WhenRayTouchesEdge_ReturnsTrue()
    // {
    //     // Arrange
    //     var staticPolygon = new List<Point> { new Point(0, 0), new Point(0, 2), new Point(2, 2), new Point(2, 0) };
    //     var movingPolygon = new List<Point> { new Point(-1, 1), new Point(1, 1), new Point(1, 3), new Point(-1, 3) };
    //     var checker = new PolygonCollisionCheckerByRays(staticPolygon, movingPolygon);

    //     // Act
    //     var collision = checker.IsCollision(0, 0, 1, 0);

    //     // Assert
    //     Assert.True(collision);
    // }
}
