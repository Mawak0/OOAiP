namespace SpaceBattle.Lib;

public class RegisterNewGameDependenciesCommand : ICommand
{
    private readonly List<ICommand> commandList;
    public RegisterNewGameDependenciesCommand(List<ICommand> commandList)
    {
        this.commandList = commandList;
    }
    public void Execute()
    {
        commandList.ToList().ForEach(elem => elem.Execute());
    }
}
