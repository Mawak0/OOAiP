using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyGameCycleBehaviourCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.Behaviour",
            (object[] _) => new GameCycleBehaviourCommand().Execute
        ).Execute();
    }
}
