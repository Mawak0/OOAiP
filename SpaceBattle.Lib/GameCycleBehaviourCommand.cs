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

        var cmd = Ioc.Resolve<ICommand>("Commands.GetNextCommand");
        try
        {
            cmd.Execute();
        }
        catch (Exception exception)
        {
            Ioc.Resolve<ICommand>("Game.ExceptionHandle", exception, cmd).Execute();
        }
        finally
        {
            _stopwatch.Stop();
        }
    }
}
