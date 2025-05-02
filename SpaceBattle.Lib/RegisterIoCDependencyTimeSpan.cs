using App;
namespace SpaceBattle.Lib;

public class RegisterIoCTimeSpan : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.TimeSpan",
            (object[] _args) => (object)new TimeSpan(0, 0, 200)).Execute();
    }
}
