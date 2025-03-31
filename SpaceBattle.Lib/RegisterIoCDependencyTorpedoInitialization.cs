using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyTorpedoInitialization : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Game.TorpedoInitialization",
                (object[] arg) => new TorpedoInitialization((IDictionary<string, object>)arg[0], (IShooting)arg[1])
        ).Execute();
    }
}
