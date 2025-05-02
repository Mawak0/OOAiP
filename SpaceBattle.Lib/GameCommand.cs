using App;
namespace SpaceBattle.Lib;

public class GameCommand : ICommand
{
    private readonly object gameScope;
    private readonly ITimerService timerService;

    public GameCommand(object gameScope, ITimerService timerService)
    {
        this.gameScope = gameScope;
        this.timerService = timerService;
    }

    public void Execute()
    {
        var oldScope = Ioc.Resolve<object>("IoC.Scope.Current");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", gameScope).Execute();

        timerService.StartTimer(Ioc.Resolve<TimeSpan>("Game.TimeSpan"));

        while (Ioc.Resolve<bool>("Game.ShouldLoopRun"))
        {
            Ioc.Resolve<Action>("Game.Behaviour", gameScope)();
        }

        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", oldScope).Execute();
    }
}
