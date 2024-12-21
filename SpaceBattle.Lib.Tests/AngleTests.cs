using Xunit;
namespace SpaceBattle.Lib.Tests;

public class AngleTests
{
    [Fact]
    public void Execute_Adding()
    {
        var angle1 = new Angle(5, 8);
        var angle2 = new Angle(7, 8);
        var resAngle = new Angle(4, 8);
        var newAngle = angle1 + angle2;

        Assert.Equal(resAngle, newAngle);
    }

    [Fact]
    public void Execute_Equals()
    {
        var angle1 = new Angle(15, 8);
        var angle2 = new Angle(23, 8);
        Assert.True(angle1.Equals(angle2));
    }

    [Fact]
    public void Execute_Equal()
    {
        var angle1 = new Angle(15, 8);
        var angle2 = new Angle(23, 8);
        Assert.True(angle1 == angle2);
    }

    [Fact]
    public void Execute_NotEquals()
    {
        var angle1 = new Angle(1, 8);
        var angle2 = new Angle(2, 8);
        Assert.False(angle1.Equals(angle2));
    }

    [Fact]
    public void Execute_NotEqual()
    {
        var angle1 = new Angle(1, 8);
        var angle2 = new Angle(2, 8);
        Assert.True(angle1 != angle2);
    }

    [Fact]
    public void Execute_Hash()
    {
        var angle = new Angle(1, 8);
        Assert.True(angle.GetHashCode() != 0);
    }

    [Fact]
    public void Execute_GetNumerator()
    {
        var angle = new Angle(1, 8);

        Assert.Equal(1, angle.Numerator);
    }

    [Fact]
    public void Execute_GetDenominator()
    {
        var angle = new Angle(1, 8);

        Assert.Equal(8, angle.Denominator);
    }

    [Fact]
    public void Execute_Equal_Null()
    {
        Angle angle1 = new Angle(1, 8);
        object? angle2 = null;
        Assert.False(angle1.Equals(angle2));

    }

    [Fact]
    public void Execute_Equal_False()
    {
        Angle angle1 = new Angle(2, 8);
        Angle angle2 = new Angle(1, 8);
        Assert.False(angle1.Equals(angle2));

    }

    [Fact]
    public void Execute_ToDouble()
    {
        Angle angle1 = new Angle(4, 8);
        Assert.Equal(Math.PI, angle1.ToDouble());
    }
}
