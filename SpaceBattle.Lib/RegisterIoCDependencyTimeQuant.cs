using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyTimeQuant : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.HaveTimeToGame",
            (object[] _) => (object)!Ioc.Resolve<ITimeCounting>("Game.EndOfTime").isEndOfTime()
        ).Execute();
    }
}
