using Xunit;
namespace SpaceBattle.Lib;
using App;
using App.Scopes;

public class LegalOrdersRepositoryTests
{
    public LegalOrdersRepositoryTests()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void RegisterIoCDependencyAddLegalOrderTest()
    {
        var dict2 = new Dictionary<string, Dictionary<string, HashSet<string>>>();

        var registerIoCDependencyLegalOrdersRepository = new RegisterIoCDependencyLegalOrdersRepository();
        registerIoCDependencyLegalOrdersRepository.Execute();

        var registerIoCDependencyAddLegalOrderCommand = new RegisterIoCDependencyAddLegalOrderCommand();
        registerIoCDependencyAddLegalOrderCommand.Execute();

        var dict1 = Ioc.Resolve<Dictionary<string, Dictionary<string, HashSet<string>>>>("LegalOrders.Repository");

        var cmd = Ioc.Resolve<ICommand>("LegalOrders.Add", "player1", "shoot", new HashSet<string>() { "spaceship4" });
        cmd.Execute();

        dict2.Add("player1", new Dictionary<string, HashSet<string>> { { "shoot", ["spaceship4"] } });

        Assert.Equivalent(dict1, dict2);
    }

    [Fact]
    public void RegisterIoCDependencyRemoveOrderTest()
    {
        var dict2 = new Dictionary<string, Dictionary<string, HashSet<string>>>();

        var registerIoCDependencyLegalOrdersRepository = new RegisterIoCDependencyLegalOrdersRepository();
        registerIoCDependencyLegalOrdersRepository.Execute();

        var registerIoCDependencyAddLegalOrderCommand = new RegisterIoCDependencyAddLegalOrderCommand();
        registerIoCDependencyAddLegalOrderCommand.Execute();

        var registerIoCDependencyRemoveLegalOrderCommand = new RegisterIoCDependencyRemoveLegalOrderCommand();
        registerIoCDependencyRemoveLegalOrderCommand.Execute();

        var dict1 = Ioc.Resolve<Dictionary<string, Dictionary<string, HashSet<string>>>>("LegalOrders.Repository");

        var cmdAdd = Ioc.Resolve<ICommand>("LegalOrders.Add", "player1", "shoot", new HashSet<string>() { "spaceship4" });
        cmdAdd.Execute();

        dict2.Add("player1", new Dictionary<string, HashSet<string>>() { { "shoot", ["spaceship4"] } });

        var cmdRemove = Ioc.Resolve<ICommand>("LegalOrders.Remove", "player1", "shoot", "spaceship4");
        cmdRemove.Execute();

        dict2["player1"]["shoot"].Remove("spaceship4");

        Assert.Equivalent(dict1, dict2);
    }

    [Fact]
    public void RemoveOrderExceptionTest()
    {
        var registerIoCDependencyLegalOrdersRepository = new RegisterIoCDependencyLegalOrdersRepository();
        registerIoCDependencyLegalOrdersRepository.Execute();

        var registerIoCDependencyAddLegalOrderCommand = new RegisterIoCDependencyAddLegalOrderCommand();
        registerIoCDependencyAddLegalOrderCommand.Execute();

        var registerIoCDependencyRemoveLegalOrderCommand = new RegisterIoCDependencyRemoveLegalOrderCommand();
        registerIoCDependencyRemoveLegalOrderCommand.Execute();

        var dict1 = Ioc.Resolve<Dictionary<string, Dictionary<string, HashSet<string>>>>("LegalOrders.Repository");

        var cmdAdd = Ioc.Resolve<ICommand>("LegalOrders.Add", "player1", "shoot", new HashSet<string>() { "spaceship4" });
        cmdAdd.Execute();

        var cmdRemove = Ioc.Resolve<ICommand>("LegalOrders.Remove", "player1", "shoot", "spaceship10");

        var exception = Assert.Throws<Exception>(() => cmdRemove.Execute());

        Assert.Equal("the order was not found", exception.Message);
    }
    [Fact]
    public void AddLegalOrderCommandTest()
    {
        var registerIoCDependencyLegalOrdersRepository = new RegisterIoCDependencyLegalOrdersRepository();
        registerIoCDependencyLegalOrdersRepository.Execute();

        var cmdAdd = new AddLegalOrder("player1", "shoot", new HashSet<string>() { "spaceship4" });
        var dict2 = new Dictionary<string, Dictionary<string, HashSet<string>>>();

        var dict1 = Ioc.Resolve<Dictionary<string, Dictionary<string, HashSet<string>>>>("LegalOrders.Repository");

        cmdAdd.Execute();

        dict2.Add("player1", new Dictionary<string, HashSet<string>> { { "shoot", ["spaceship4"] } });

        Assert.Equivalent(dict1, dict2);
    }

    [Fact]
    public void RemoveOrderCommandTest()
    {
        var registerIoCDependencyLegalOrdersRepository = new RegisterIoCDependencyLegalOrdersRepository();
        registerIoCDependencyLegalOrdersRepository.Execute();

        var cmdAdd = new AddLegalOrder("player1", "shoot", new HashSet<string>() { "spaceship4" });
        var cmdRemove = new RemoveLegalOrder("player1", "shoot", "spaceship4");

        var dict1 = Ioc.Resolve<Dictionary<string, Dictionary<string, HashSet<string>>>>("LegalOrders.Repository");

        var dict2 = new Dictionary<string, Dictionary<string, HashSet<string>>>();

        cmdAdd.Execute();

        dict2.Add("player1", new Dictionary<string, HashSet<string>> { { "shoot", ["spaceship4"] } });

        cmdRemove.Execute();
        dict2["player1"]["shoot"].Remove("spaceship4");

        Assert.Equivalent(dict1, dict2);
    }
}
