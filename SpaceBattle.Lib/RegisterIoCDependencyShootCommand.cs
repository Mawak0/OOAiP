using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyShootCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Commands.ShootCommand",
            (object[] args) => new ShootCommand((IShooting)args[0])
        ).Execute();
    }
}
