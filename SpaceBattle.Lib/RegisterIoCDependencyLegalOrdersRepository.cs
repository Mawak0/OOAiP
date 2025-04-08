using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyLegalOrdersRepository : ICommand
{
    public void Execute()
    {
        var dict = new Dictionary<string, IDictionary<string, HashSet<string>>>();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "LegalOrders.Repository",
            (object[] _) => new LegalOrdersRepository(dict)
        ).Execute();
    }
}
