namespace SpaceBattle.Lib.Tests;

using Xunit;

public class TreeTests
{
    [Fact]
    public void ContainsFourTest()
    {
        var four = new List<(int, int, int, int)> { (4, 6, 8, 3) };
        var t = new Tree(four);

        Assert.True(t.Contains((4, 6, 8, 3)));
    }

    [Fact]
    public void NotContainsFourTest()
    {
        var four = new List<(int, int, int, int)> { (4, 6, 8, 3) };
        var t = new Tree(four);

        Assert.False(t.Contains((2, 6, 8, 3)));
    }

    [Fact]
    public void EmptyTreeTest()
    {
        var four = new List<(int, int, int, int)>();
        var t = new Tree(four);

        Assert.False(t.Contains((4, 6, 8, 3)));
    }

    [Fact]
    public void LastComparationTest()
    {
        var four = new List<(int, int, int, int)> { (5, 8, 4, 1) };
        var t = new Tree(four);

        Assert.False(t.Contains((5, 8, 4, 2)));
    }
}
