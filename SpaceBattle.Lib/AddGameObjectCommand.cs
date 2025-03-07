using App;

namespace SpaceBattle.Lib;

public class AddGameObjectCommand : ICommand
{
    private readonly object ObjToAdd;
    private readonly string uuid;
    public AddGameObjectCommand(string uuid, object ObjToAdd)
    {
        this.uuid = uuid;
        this.ObjToAdd = ObjToAdd;
    }
    public void Execute()
    {
        Ioc.Resolve<IDictionary<string, object>>("Game.Object.Repository").Add(uuid, ObjToAdd);
    }
}
