using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyLegalOrdersRepository : ICommand
{
    public void Execute()
    {
        Dictionary<string, Dictionary<string, HashSet<string>>> dict = new Dictionary<string, Dictionary<string, HashSet<string>>>() { };
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "LegalOrders.Repository",
            (object[] _) => dict
        ).Execute();
    }
}
