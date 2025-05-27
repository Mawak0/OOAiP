namespace SpaceBattle.Lib;

public class AddToStorageCommand : App.ICommand
{
    private readonly Tree tree;
    private readonly string fig1;
    private readonly string fig2;
    private readonly Dictionary<(string, string), Tree> storage;

    public AddToStorageCommand(string f1, string f2, Tree t, Dictionary<(string, string), Tree> s)
    {
        fig1 = f1;
        fig2 = f2;
        tree = t;
        storage = s;
    }

    public void Execute()
    {
        storage[(fig1, fig2)] = tree;
    }
}
