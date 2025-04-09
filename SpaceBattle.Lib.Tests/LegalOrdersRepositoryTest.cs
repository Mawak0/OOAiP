using Xunit;
namespace SpaceBattle.Lib.Tests;
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
    public void RegisterIoCDependencyLegalOrderRepositoryTest()
    {
        RegisterIoCDependencyLegalOrdersRepository registerIoCDependencyLegalOrdersRepository = new RegisterIoCDependencyLegalOrdersRepository();
        registerIoCDependencyLegalOrdersRepository.Execute();
        var res = Ioc.Resolve<object>("LegalOrders.Repository");
        Assert.IsType<LegalOrdersRepository>(res);
    }

    [Fact]
    public void LegalOrderRepositoryAddOrderTest()
    {
        var dict = new Dictionary<string, Dictionary<string, List<string>>>();
        LegalOrdersRepository legalOrdersRepository = new LegalOrdersRepository(new Dictionary<string, IDictionary<string, HashSet<string>>>());

        legalOrdersRepository.AddLegalOrder("player1", "shoot", ["spaceship4"]);
        dict.Add("player1", new Dictionary<string, List<string>> { { "shoot", ["spaceship4"] } });

        Assert.Equivalent(legalOrdersRepository.legalOrdersRepository, dict);
    }

    [Fact]
    public void LegalOrderRepositoryRemoveOrderTest()
    {
        var dict = new Dictionary<string, Dictionary<string, HashSet<string>>>();
        LegalOrdersRepository legalOrdersRepository = new LegalOrdersRepository(new Dictionary<string, IDictionary<string, HashSet<string>>>());

        legalOrdersRepository.AddLegalOrder("player1", "shoot", ["spaceship4"]);
        dict.Add("player1", new Dictionary<string, HashSet<string>>() { { "shoot", ["spaceship4"] } });
        legalOrdersRepository.RemoveLegalOrder("player1", "shoot", "spaceship4");
        dict["player1"]["shoot"].Remove("spaceship4");

        Assert.Equivalent(legalOrdersRepository.legalOrdersRepository, dict);
    }

    [Fact]
    public void LegalOrderRepositoryRemoveOrderExceptionTest()
    {
        LegalOrdersRepository legalOrdersRepository = new LegalOrdersRepository(new Dictionary<string, IDictionary<string, HashSet<string>>>());
        legalOrdersRepository.AddLegalOrder("player1", "shoot", ["spaceship4"]);

        var exception = Assert.Throws<Exception>(() => legalOrdersRepository.RemoveLegalOrder("player1", "shoot", "spaceship10"));

        Assert.Equal("the order was not found", exception.Message);
    }
}
