namespace SpaceBattle.Lib;

using App;

public class AddLegalOrder : ICommand
{
    public string user;
    public string command;
    public HashSet<string> obj;
    public AddLegalOrder(string user, string command, HashSet<string> obj)
    {
        this.user = user;
        this.command = command;
        this.obj = obj;
    }
    public void Execute()
    {
        System.Diagnostics.Debug.WriteLine("Execute called");
        var legalOrdersRepository = Ioc.Resolve<Dictionary<string, Dictionary<string, HashSet<string>>>>("LegalOrders.Repository");

        Dictionary<string, HashSet<string>> internal_dict = new Dictionary<string, HashSet<string>>() { { command, obj } };
        legalOrdersRepository.TryAdd(user, internal_dict);

        legalOrdersRepository[user].TryAdd(command, obj);

        legalOrdersRepository[user].TryGetValue(command, out HashSet<string>? objects);
        legalOrdersRepository[user][command] = objects!.Union(obj.Except(objects!)).ToHashSet();
    }
}
