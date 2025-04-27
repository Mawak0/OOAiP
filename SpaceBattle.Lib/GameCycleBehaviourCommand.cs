using App;
namespace SpaceBattle.Lib;
using System.Diagnostics;

public class GameCycleBehaviourCommand : ICommand
{
    private readonly Stopwatch _stopwatch;

    public GameCycleBehaviourCommand()
    {
        _stopwatch = new Stopwatch();
    }

    public void Execute()
    {
        _stopwatch.Reset();
        var timeLimit = Ioc.Resolve<TimeSpan>("Command.GameTime");

        var cmd = Ioc.Resolve<ICommand>("Commands.GetNextCommand");
        try
        {
            _stopwatch.Start();
            cmd.Execute();
        }
        catch (Exception exception)
        {
            Ioc.Resolve<ICommand>("Game.ExceptionHandle", exception, cmd).Execute();
        }
        finally
        {
            _stopwatch.Stop();
            if (_stopwatch.Elapsed >= timeLimit)
            {
                Ioc.Resolve<ITimeCounting>("IoC.Register", "Game.EndOfTime", true);
            }
        }
    }
}
