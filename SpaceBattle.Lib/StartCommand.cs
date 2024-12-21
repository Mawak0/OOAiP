using App;

namespace SpaceBattle.Lib;

public class StartCommand(IDictionary<string, object> order) : ICommand
{
    public void Execute()
    {
        var mc = Ioc.Resolve<ICommand>("Macro." + (string)order["Action"], (object[])order["Args"]);
        var inj = Ioc.Resolve<ICommand>("Commands.CommandInjectable");
        var q = Ioc.Resolve<ICommandReceiver>("Game.Queue");
        var RepeatableMacro = Ioc.Resolve<ICommand>("Commands.Macro", (ICommand[])[mc, inj]);
        var repeatCommand = Ioc.Resolve<ICommand>("Commands.Send", q, RepeatableMacro);
        var injInjectable = (ICommandInjectable)inj;
        injInjectable.Inject(repeatCommand);
        var sendCommand = Ioc.Resolve<ICommand>("Commands.Send", q, repeatCommand);
        sendCommand.Execute();
        var obj = Ioc.Resolve<IDictionary<string, object>>("Game.Object", (string)order["Key"]);
        obj.Add((string)order["Action"], inj);
    }
}
