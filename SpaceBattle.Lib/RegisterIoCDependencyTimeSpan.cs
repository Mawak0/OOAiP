using App;
namespace SpaceBattle.Lib;

public class RegisterIoCTimeSpan : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<Func<object[], TimeSpan>>(
            "IoC.Register",
            "Game.TimeSpan",
            (object _args) => new TimeSpan(0, 0, 200));
    }
}