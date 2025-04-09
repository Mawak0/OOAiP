using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyShouldLoopRun : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.ShouldLoopRun",
            (object[] _) => (object)!Ioc.Resolve<ICanBeEmpty>("Game.Queue").isEmpty()
        ).Execute();
    }
}
