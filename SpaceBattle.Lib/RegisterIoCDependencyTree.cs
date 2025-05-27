using App;

namespace SpaceBattle.Lib;

public class RegisterIoCDependencyTree : ICommand
{
    public void Execute()
    {
        var storage = new Dictionary<(string, string), Tree>();

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Collision.Tree.Add",
            (object[] args) =>
            {
                var f1 = (string)args[0];
                var f2 = (string)args[1];
                var tree = (Tree)args[2];

                return new AddToStorageCommand(f1, f2, tree, storage);
            }
        ).Execute();
    }
}
