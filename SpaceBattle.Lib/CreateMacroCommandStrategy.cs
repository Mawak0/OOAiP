using App;
namespace SpaceBattle.Lib;

public class CreateMacroCommandStrategy(string commandSpec)
{
    public ICommand Resolve(object[] args)
    {
        var commands = Ioc.Resolve<IEnumerable<string>>("Specs." + commandSpec);

        var addCommands = commands.Select(p => Ioc.Resolve<ICommand>(p, args)).ToArray<ICommand>();
        return Ioc.Resolve<ICommand>("Commands.Macro", addCommands);
    }
}
