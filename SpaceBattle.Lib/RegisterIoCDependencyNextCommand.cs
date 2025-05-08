using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyNextCommand : ICommand
{
    public void Execute()
    {
        ITake q = Ioc.Resolve<ITake>("Game.Queue");

        Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Commands.GetNextCommand",
                (object[] _) => q.Take()).Execute();
    }
}
