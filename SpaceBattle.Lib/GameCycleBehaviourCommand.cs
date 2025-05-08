using App;
namespace SpaceBattle.Lib;

public class GameCycleBehaviourCommand : ICommand
{
    public void Execute()
    {
        var cmd = Ioc.Resolve<ICommand>("Commands.GetNextCommand");
        try
        {
            cmd.Execute();
        }
        catch (Exception exception)
        {
            Ioc.Resolve<ICommand>("Game.ExceptionHandle", cmd, exception).Execute();
        }
    }
}
