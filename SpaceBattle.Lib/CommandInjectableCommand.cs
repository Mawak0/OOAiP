namespace SpaceBattle.Lib;

public class InjectableCommand : ICommand, ICommandInjectable
{
    private ICommand _cmd = new InvalidCommand();
    public void Execute()
    {
        _cmd.Execute();
    }
    public void Inject(ICommand cmd)
    {
        _cmd = cmd;
    }
}
