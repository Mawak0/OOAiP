using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyActionsStart : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Actions.Start",
            (object[] order) => new StartCommand((IDictionary<string, object>)order[0])
        ).Execute();
    }
}
