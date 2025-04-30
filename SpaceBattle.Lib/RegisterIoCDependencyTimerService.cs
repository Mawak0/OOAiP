using App;
namespace SpaceBattle.Lib;

public class RegisterIoCTimerService : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<ITimerService>(
            "IoC.Register",
            "Game.TimerService",
            (object[] _) => new TimerService());
    }
}