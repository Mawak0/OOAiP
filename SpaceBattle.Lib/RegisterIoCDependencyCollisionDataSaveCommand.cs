using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyCollisionDataSaveCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Collision.DataSaveCommand",
                (object[] arg) => new CollisionDataSaveCommand((string)arg[0], (IList<int[]>)arg[1])).Execute();
    }
}
