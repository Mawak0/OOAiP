using App;

public class RegisterIoCDependencyLegalOrderRepository : ICommand
{
    public void Execute()
    {
        var legalOrdersRepository = new Dictionary<string, Dictionary<string, string[]>>();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "LegalOrder.Repository",
            (object[] _) => legalOrdersRepository
        ).Execute();
    }
}
