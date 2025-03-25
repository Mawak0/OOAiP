using Xunit;
namespace SpaceBattle.Lib.Tests;
using App;
using App.Scopes;

public class AuthTests
{
    public AuthTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void AuthCommandSuccess()
    {
        RegisterDependencies();

        var authcommand = Ioc.Resolve<AuthCommand>("Commands.Auth", "Player1", "Fire", "Spaceship3");

        var exception = Record.Exception(() => authcommand.Execute());
        Assert.Null(exception);
    }

    [Fact]
    public void AuthCommandException()
    {
        RegisterDependencies();

        var authcommand = Ioc.Resolve<AuthCommand>("Commands.Auth", "Player1", "Fire", "Spaceship1");

        var exception = Assert.Throws<Exception>(() => authcommand.Execute());
        Assert.Equal("Сommand not allowed", exception.Message);
    }

    internal static void RegisterDependencies()
    {
        string username = "Player1";
        string command = "Fire";
        string[] objects = ["Spaceship4", "Spaceship3", "Spaceship2"];

        var registerIoCDependencyLegalOrderRepository = new RegisterIoCDependencyLegalOrderRepository();
        registerIoCDependencyLegalOrderRepository.Execute();

        var registerIoCDependencyAddLegalOrder = new RegisterIoCDependencyAddLegalOrder();
        registerIoCDependencyAddLegalOrder.Execute();

        var addorder = Ioc.Resolve<AddLegalOrder>("LegalOrder.Add", username, command, objects);
        addorder.Execute();

        var registerDependencyAuthCommand = new RegisterDependencyAuthCommand();
        registerDependencyAuthCommand.Execute();
    }
}
