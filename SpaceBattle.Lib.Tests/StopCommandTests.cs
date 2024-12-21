using App;
using App.Scopes;
using Moq;
using Xunit;
namespace SpaceBattle.Lib;

public class StopCommandTests
{
    public StopCommandTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void StopCmdTest()
    {
        var mockInj = new Mock<ICommandInjectable>();
        var mockEmpty = new Mock<ICommand>();
        var gameObj = new Mock<IDictionary<string, object>>();
        gameObj.Setup(m => m[It.Is<string>(key => key == "TestAction")]).Returns(mockInj.Object);
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Object", (object[] _) => gameObj.Object).Execute();

        var order = new Mock<IDictionary<string, object>>();
        order.Setup(m => m[It.Is<string>(key => key == "Action")]).Returns("TestAction");
        order.Setup(m => m[It.Is<string>(key => key == "Key")]).Returns("Test");

        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Injectable", (object[] args) => mockInj.Object).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Empty", (object[] args) => mockEmpty.Object).Execute();

        var stopCommand = new StopCommand(order.Object);
        stopCommand.Execute();
        mockInj.Verify(i => i.Inject(mockEmpty.Object), Times.Once);
    }
}
