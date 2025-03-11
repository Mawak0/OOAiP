using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyGameObject : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.Object",
            (object[] args) => (Ioc.Resolve<IDictionary<string, object>>("Game.Object.Repository"))[(string)args[0]]
        ).Execute();
    }
}
