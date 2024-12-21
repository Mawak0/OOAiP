using App;
using App.Scopes;
using Moq;
using Xunit;

namespace SpaceBattle.Lib;

public class SendCommandRegisterTest
{
    [Fact]
    public void ExecuteTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

        var mockReceiver = new Mock<ICommandReceiver>();
        var mockCommand = new Mock<ICommand>();

        var registerSend = new RegisterIoCDependencySendCommand();
        registerSend.Execute();

        var sendCommand = Ioc.Resolve<ICommand>("Commands.Send", mockCommand.Object, mockReceiver.Object);

        Assert.IsType<SendCommand>(sendCommand);

    }
}
