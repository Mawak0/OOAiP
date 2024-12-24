namespace SpaceBattle.Lib;

public class InvalidCommand : ICommand
{
    public void Execute()
    {
        throw new Exception("There is nothing to execute!");
    }
}
