using Xunit;
namespace SpaceBattle.Lib.Tests;

public class InvalidCommandTests
{
    [Fact]
    public void Execute_WhenCalled_ThrowsException()
    {
        var invalidCommand = new InvalidCommand();

        var ex = Assert.Throws<Exception>(() => invalidCommand.Execute());
        Assert.Equal("There is nothing to execute!", ex.Message);
    }
}
