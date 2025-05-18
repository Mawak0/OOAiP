using Xunit;

namespace SpaceBattle.Lib;

public class RayTests
{
    [Fact]
    public void ConstructorNormalizesDirectionVectorTest()
    {
        var origin = new Point(0, 0);
        var direction = new Point(3, 4);

        var ray = new Ray(origin, direction);

        Assert.Equal(1, Math.Sqrt(ray.Direction.X * ray.Direction.X + ray.Direction.Y * ray.Direction.Y));
    }

    [Fact]
    public void PropertiesSetCorrectlyTest()
    {
        var origin = new Point(1, 2);
        var direction = new Point(3, 4);

        var ray = new Ray(origin, direction);

        Assert.Equal(origin, ray.Origin);
        Assert.NotEqual(direction, ray.Direction);
    }

    [Fact]
    public void DirectionHasUnitLengthTest()
    {
        var origin = new Point(0, 0);
        var direction = new Point(10, 0);

        var ray = new Ray(origin, direction);

        Assert.Equal(1.0, Math.Sqrt(ray.Direction.X * ray.Direction.X + ray.Direction.Y * ray.Direction.Y)); // Норма должна быть равна 1
    }
}
