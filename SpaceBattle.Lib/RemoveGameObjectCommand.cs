using App;

namespace SpaceBattle.Lib;

public class RemoveGameObjectCommand : ICommand
{
    private readonly string uuid;
    public RemoveGameObjectCommand(string uuid)
    {
        this.uuid = uuid;
    }
    public void Execute()
    {
        ((IDictionary<string, object>)Ioc.Resolve<object>("Game.Object.Repository")).Remove(uuid);
    }
}
