using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyGetVectorDifference : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.GetVectorDifference",
            (object[] args) => ((Vector)args[0]).Values.Zip(((Vector)args[1]).Values, (a, b) => a - b).ToArray()
        ).Execute();
    }
}
