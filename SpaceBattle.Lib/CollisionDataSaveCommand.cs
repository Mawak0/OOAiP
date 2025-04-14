using App;

namespace SpaceBattle.Lib;

public class CollisionDataSaveCommand : ICommand
{
    private readonly IList<int[]> collisionData;
    private readonly string collisionName;

    public CollisionDataSaveCommand(string collisionName, IList<int[]> collisionData)
    {
        this.collisionData = collisionData;
        this.collisionName = collisionName;
    }
    public void Execute()
    {
        var pathForCollisionFile = Ioc.Resolve<string>("Data.CollisionFilesPath");
        Ioc.Resolve<ICommand>("Commands.WriteObjectToFile", pathForCollisionFile + collisionName, collisionData).Execute();
        Ioc.Resolve<ICommand>("Collision.LoadDataToMemory", collisionName, collisionData).Execute();
    }
}
