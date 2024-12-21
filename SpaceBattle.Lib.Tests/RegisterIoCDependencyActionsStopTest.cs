using App;
using App.Scopes;
using Moq;
using Xunit;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStopTest
{
    public RegisterIoCDependencyActionsStopTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void RegisterTest()
    {
        var obj = new Mock<IDictionary<string, object>>();

        var registerIoCDependencyActionsStop = new RegisterIoCDependencyActionsStop();
        registerIoCDependencyActionsStop.Execute();

        var res = Ioc.Resolve<ICommand>("Actions.Stop", obj.Object);

        Assert.IsType<StopCommand>(res);
    }
}
