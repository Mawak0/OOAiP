using App;
namespace SpaceBattle.Lib;

public class GameCommand : ICommand
{
    private readonly object gameScope;
    public GameCommand(object gameScope)
    {
        this.gameScope = gameScope;
    }

    public void Execute()
    {
        var oldScope = Ioc.Resolve<object>("IoC.Scope.Current");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", gameScope).Execute();

        while (Ioc.Resolve<bool>("Game.ShouldLoopRun"))
        {
            Ioc.Resolve<Action>("Game.Behaviour", gameScope)();
        }

        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", oldScope).Execute();
    }
}
