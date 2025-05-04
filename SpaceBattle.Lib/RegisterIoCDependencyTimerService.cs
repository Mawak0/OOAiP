using App;
namespace SpaceBattle.Lib;

public class RegisterIoCTimerService : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.TimerService",
            (object[] _) => new TimerService()).Execute();
    }
}
