using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyShouldLoopRun : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.ShouldLoopRun",
            (object[] args) =>
            {
                var queue = Ioc.Resolve<ICanBeEmpty>("Game.Queue");
                var spentTime = args[0];
                var maxTimeSpan = Ioc.Resolve<TimeSpan>("Game.TimeSpan");

                return (object)(!queue.isEmpty() && (long)spentTime < maxTimeSpan.TotalMilliseconds);
            }).Execute();
    }
}
