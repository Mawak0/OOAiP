using Moq;
using Xunit;
namespace SpaceBattle.Lib.Tests;

public class InjectableCommandTests
{
    [Fact]
    public void InjectableCommandTest()
    {
        var cmd = new Mock<ICommand>();
        var injectableCommand = new InjectableCommand();
        injectableCommand.Inject(cmd.Object);
        injectableCommand.Execute();
        cmd.Verify(x => x.Execute());
    }

    [Fact]
    public void CommandHaventInjected()
    {
        var injectableCommand = new InjectableCommand();
        Assert.Throws<Exception>(() => injectableCommand.Execute());
    }
}
