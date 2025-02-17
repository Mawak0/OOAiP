using Moq;
using Xunit;
namespace SpaceBattle.Lib.Tests;

public class MCommandTests
{
    [Fact]
    public void Execute_AllCommandsExecuted()
    {
        var cmd1 = new Mock<ICommand>();
        var cmd2 = new Mock<ICommand>();
        ICommand[] lst = [cmd1.Object, cmd2.Object];
        var mc = new MCommand(lst);
        mc.Execute();

        cmd1.Verify(x => x.Execute());
        cmd2.Verify(x => x.Execute());
    }

    [Fact]
    public void Execute_Exception()
    {
        var cmd1 = new Mock<ICommand>();
        var cmd2 = new Mock<ICommand>();
        var cmd3 = new Mock<ICommand>();

        cmd2.Setup(x => x.Execute()).Throws(new Exception());

        ICommand[] lst = [cmd1.Object, cmd2.Object, cmd3.Object];

        var mc = new MCommand(lst);

        Assert.Throws<Exception>(() => mc.Execute());
        cmd1.Verify(c => c.Execute(), Times.Once);
        cmd2.Verify(c => c.Execute(), Times.Once);
        cmd3.Verify(c => c.Execute(), Times.Never);
    }
}
