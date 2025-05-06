using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyRemoveLegalOrderCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "LegalOrders.Remove",
            (object[] args) => new RemoveLegalOrder((string)args[0], (string)args[1], (string)args[2])
        ).Execute();
    }
}
