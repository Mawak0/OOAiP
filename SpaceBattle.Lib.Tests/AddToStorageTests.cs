namespace SpaceBattle.Lib.Tests;

using Xunit;

public class AddToStorageTests
{
    [Fact]
    public void CorrectAdding()
    {
        var f1 = "shape1";
        var f2 = "shape2";

        var fours = new List<(int, int, int, int)> { (1, 1, 5, 6), (8, 2, 3, 10) };
        var t = new Tree(fours);
        var s = (Dictionary<(string, string), Tree>)[];

        var cmd = new AddToStorageCommand(f1, f2, t, s);

        cmd.Execute();
    }
}