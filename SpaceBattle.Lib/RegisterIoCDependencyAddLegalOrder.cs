namespace SpaceBattle.Lib;
using App;

public class RegisterIoCDependencyAddLegalOrder : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "LegalOrder.Add",
            (object[] args) => new AddLegalOrder((string)args[0], (string)args[1], (string[])args[2])
        ).Execute();
    }
}
