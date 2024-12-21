using App;
using App.Scopes;
using Moq;
using Xunit;
namespace SpaceBattle.Lib;

public class StartCommandTest
{
    [Fact]
    public void StartCommandExecuteTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
        var mCommand = new Mock<ICommand>();
        var inj = new Mock<ICommand>();
        inj.As<ICommandInjectable>().Setup(m => m.Inject(It.IsAny<ICommand>())).Verifiable();
        var q = new Mock<ICommandReceiver>();
        var sendCommand = new Mock<ICommand>();
        var gameObj = new Mock<IDictionary<string, object>>();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Macro.Move", (object[] args) => mCommand.Object).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Macro", (object[] args) => mCommand.Object).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.CommandInjectable", (object[] args) => inj.Object).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Queue", (object[] args) => q.Object).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Send", (object[] args) => sendCommand.Object).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Object", (object[] args) => gameObj.Object).Execute();

        var order = new Mock<IDictionary<string, object>>();
        order.Setup(m => m[It.Is<string>(key => key == "Action")]).Returns("Move");
        order.Setup(m => m[It.Is<string>(key => key == "Args")]).Returns(new object[] { });
        order.Setup(m => m[It.Is<string>(key => key == "Key")]).Returns("");

        var startCommand = new StartCommand(order.Object);

        startCommand.Execute();

        inj.As<ICommandInjectable>().Verify(c => c.Inject(It.IsAny<ICommand>()), Times.Once);
        sendCommand.Verify(c => c.Execute(), Times.Once);
    }
}
