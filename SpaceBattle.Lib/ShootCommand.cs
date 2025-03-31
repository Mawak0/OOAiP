using App;

namespace SpaceBattle.Lib;

public class ShootCommand : ICommand
{
    private readonly IShooting shooterObj;

    public ShootCommand(IShooting shooterObj)
    {
        this.shooterObj = shooterObj;
    }

    public void Execute()
    {
        var torpedo = Ioc.Resolve<object>("Game.GetTorpedo");
        Ioc.Resolve<ICommand>("Game.TorpedoInitialization", torpedo, shooterObj).Execute();

        var order = Ioc.Resolve<Dictionary<string, object>>("Game.CreateStartCommandOrder", torpedo);

        var startCommand = Ioc.Resolve<ICommand>("Actions.Start", order);

        startCommand.Execute();

    }
}
