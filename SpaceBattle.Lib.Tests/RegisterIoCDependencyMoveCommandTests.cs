using App;
using App.Scopes;
using Moq;
using Xunit;
namespace SpaceBattle.Lib.Tests;

public class RegisterIoCDependencyMoveCommandTests
{
    [Fact]
    public void ResolveDependencyCheck()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();

        var mockRegisterMove = new Mock<RegisterIoCDependencyMoveCommand>();
        var registerMove = mockRegisterMove.Object;
        registerMove.Execute();

        var mockImoving = new Mock<IMoving>();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Adapters.IMovingObject", (object[] args) => mockImoving.Object).Execute();

        var res = Ioc.Resolve<ICommand>("Commands.Move");

        Assert.IsType<MoveCommand>(res);

    }
}
