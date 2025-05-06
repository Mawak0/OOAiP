using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyAddLegalOrderCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "LegalOrders.Add",
            (object[] args) => new AddLegalOrder((string)args[0], (string)args[1], (HashSet<string>)args[2])
        ).Execute();
    }
}
