using App;
namespace SpaceBattle.Lib;

public class RegisterIoCRegisterNewGameDependenciesCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Commands.RegisterNewGameDependenciesCommand",
            (object[] obj) => new RegisterNewGameDependenciesCommand((List<ICommand>)obj[0])
        ).Execute();
    }
}
