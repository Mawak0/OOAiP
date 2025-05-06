using App;
using System.Diagnostics;
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
        var timer = Stopwatch.StartNew();
        var oldScope = Ioc.Resolve<object>("IoC.Scope.Current");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", gameScope).Execute();

        while (Ioc.Resolve<bool>("Game.ShouldLoopRun", timer.ElapsedMilliseconds))
        {
            Ioc.Resolve<Action>("Game.Behaviour", gameScope)();
        }

        timer.Stop();
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", oldScope).Execute();
    }
}
