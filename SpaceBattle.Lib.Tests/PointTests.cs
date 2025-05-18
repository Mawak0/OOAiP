using Xunit;

namespace SpaceBattle.Lib.Tests;

public class PointTests
{
    [Fact]
    public void CreatePointTest()
    {
        var point = new Point(1, 5);

        Assert.Equal(1, point.X);
        Assert.Equal(5, point.Y);
    }

    [Fact]
    public void AdditionPointsTest()
    {
        var p1 = new Point(1, 2);
        var p2 = new Point(3, 4);

        var result = p1 + p2;

        Assert.Equal(4.0, result.X);
        Assert.Equal(6.0, result.Y);
    }

    [Fact]
    public void SubtractionPointsTest()
    {
        var p1 = new Point(5, 8);
        var p2 = new Point(2, 3);

        var result = p1 - p2;

        Assert.Equal(3, result.X);
        Assert.Equal(5, result.Y);
    }

    [Fact]
    public void ScalarMultiplicationTest()
    {
        var point = new Point(2, 3);
        const double scalar = 3;

        var result = scalar * point;

        Assert.Equal(6, result.X);
        Assert.Equal(9, result.Y);
    }

    [Fact]
    public void Normalization_ReturnsNormalizedVector()
    {
        var point = new Point(3, 4);

        var normalized = point.Normalize();

        Assert.True(Math.Abs(normalized.X * normalized.X + normalized.Y * normalized.Y - 1.0) < 1E-10);
    }

    [Fact]
    public void CrossProduct_ReturnsCorrectValue()
    {
        var p1 = new Point(1, 2);
        var p2 = new Point(3, 4);

        var crossProduct = Point.Cross(p1, p2);

        Assert.Equal(-2, crossProduct);
    }
}
