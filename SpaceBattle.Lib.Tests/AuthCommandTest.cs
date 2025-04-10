using Moq;
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

        var authcommand = Ioc.Resolve<AuthCommand>("Commands.Auth", "Player2", "Fire", "Spaceship2");

        var exception = Assert.Throws<Exception>(() => authcommand.Execute());
        Assert.Equal("Сommand not allowed", exception.Message);
    }

    internal static void RegisterDependencies()
    {
        string username = "Player1";
        string command = "Fire";
        HashSet<string> objects = ["Spaceship4", "Spaceship3", "Spaceship2"];

        var mockRepo = new Mock<ILegalOrdersRepository>();
        var dict = new Dictionary<string, IDictionary<string, HashSet<string>>>()
        {
            [username] = new Dictionary<string, HashSet<string>>()
            {
                {command, objects}
            }
        };

        mockRepo.Setup(x => x.legalOrdersRepository).Returns(dict);
        var RegisterIoCDependencyLegalOrdersRepository = new Mock<ICommand>();

        RegisterIoCDependencyLegalOrdersRepository.Setup(x => x.Execute()).Callback(() =>
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "LegalOrders.Repository",
            (object[] _) => mockRepo.Object
        ).Execute());

        RegisterIoCDependencyLegalOrdersRepository.Object.Execute();

        var registerDependencyAuthCommand = new RegisterDependencyAuthCommand();
        registerDependencyAuthCommand.Execute();
    }
}
