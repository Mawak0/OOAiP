namespace SpaceBattle.Lib;

public class MCommand : ICommand
{
    private readonly ICommand[] cmds;
    public MCommand(ICommand[] cmds)
    {
        this.cmds = cmds;
    }

    public void Execute()
    {
        List<ICommand> iCommandList = new List<ICommand>();
        iCommandList = cmds.ToList<ICommand>();
        iCommandList.ForEach(c => c.Execute());
    }
}
