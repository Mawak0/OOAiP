using Xunit;
namespace SpaceBattle.Lib.Tests;

public class VectorTests
{
    [Fact]
    public void Execute_Adding()
    {
        var vector1 = new Vector([1, -1, 2]);
        var vector2 = new Vector([-1, 1, -2]);
        var resVector = new Vector([0, 0, 0]);
        var newVector = vector1 + vector2;

        Assert.Equal(newVector, resVector);
    }

    [Fact]
    public void Execute_IncorrectDims()
    {
        var vector1 = new Vector([1, 2, 3]);
        var vector2 = new Vector([1, 2]);

        Assert.Throws<ArgumentException>(() => vector1 + vector2);
    }

    [Fact]
    public void Execute_IncorrectDims2()
    {
        var vector1 = new Vector([1, 2]);
        var vector2 = new Vector([1, 2, 3]);

        Assert.Throws<ArgumentException>(() => vector1 + vector2);
    }

    [Fact]
    public void Execute_Equals()
    {
        var vector1 = new Vector([1, 2, 3]);
        var vector2 = new Vector([1, 2, 3]);

        Assert.True(vector1.Equals(vector2));
    }

    [Fact]
    public void Execute_Equal()
    {
        var vector1 = new Vector([1, 2, 3]);
        var vector2 = new Vector([1, 2, 3]);

        Assert.True(vector1 == vector2);
    }

    [Fact]
    public void Execute_NotEquals()
    {
        var vector1 = new Vector([1, 2, 3]);
        var vector2 = new Vector([3, 2, 1]);

        Assert.False(vector1.Equals(vector2));
    }

    [Fact]
    public void Execute_NotEqual()
    {
        var vector1 = new Vector([1, 2, 3]);
        var vector2 = new Vector([3, 2, 1]);

        Assert.True(vector1 != vector2);
    }

    [Fact]
    public void Execute_Hash()
    {
        var v = new Vector([1, 2, 3]);
        Assert.True(v.GetHashCode() != 0);
    }

    [Fact]
    public void Execute_Equal_Null()
    {
        var vector1 = new Vector([3, 5]);
        object? vector2 = null;

        Assert.False(vector1.Equals(vector2));
    }

    [Fact]
    public void Execute_GetByIndex()
    {
        var vector = new Vector([3, 5]);

        Assert.Equal(3, vector[0]);
    }

    [Fact]
    public void Execute_IndexOutOfRange_ThrowsException()
    {
        var vector = new Vector([3, 5]);

        Assert.Throws<IndexOutOfRangeException>(() => vector[666]);
    }

    [Fact]
    public void Execute_Print()
    {
        var vector = new Vector([3, 5]);

        Assert.Equal("[3, 5]", vector.ToString());
    }

    [Fact]
    public void Execute_SetByIndex()
    {
        var vector = new Vector([3, 5]);
        var resVector = new Vector([10, 5]);

        vector[0] = 10;

        Assert.Equal(resVector, vector);
    }

    [Fact]
    public void Execute_GetHashCode()
    {
        var vector1 = new Vector([3, 5]);
        var vector2 = new Vector([3, 5]);

        Assert.True(vector1.GetHashCode() == vector2.GetHashCode());
    }

    [Fact]
    public void Execute_GetLength()
    {
        var vector = new Vector([3, 5]);
        var l = vector.Len;

        Assert.Equal(2, l);
    }

    [Fact]
    public void Execute_GetValues()
    {
        var vector = new Vector([3, 5]);
        var vls = vector.Values;

        Assert.Equal([3, 5], vls);
    }

    [Fact]
    public void Execute_SetValues()
    {
        var vector = new Vector([3, 5]);
        vector.Values = [7, 9];

        Assert.Equal([7, 9], vector.Values);
    }
}
