using App;
namespace SpaceBattle.Lib;

public class StopCommand(IDictionary<string, object> order) : ICommand
{
    public void Execute()
    {
        var gameObject = Ioc.Resolve<IDictionary<string, object>>("Game.Object", (string)order["Key"]);
        var inj = (ICommandInjectable)gameObject[(string)order["Action"]];
        var empty = Ioc.Resolve<ICommand>("Commands.Empty");

        inj.Inject(empty);
    }
}

