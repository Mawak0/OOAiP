using App;
namespace SpaceBattle.Lib;

public class RegisterDependencyCommandInjectableCommand
{
    public static void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Commands.CommandInjectable",
            (object[] args) => new InjectableCommand()
        ).Execute();
    }
}
