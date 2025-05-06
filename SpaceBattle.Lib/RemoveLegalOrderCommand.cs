namespace SpaceBattle.Lib;
using App;

public class RemoveLegalOrder : ICommand
{
    public string user;
    public string command;
    public string obj;
    public RemoveLegalOrder(string user, string command, string obj)
    {
        this.user = user;
        this.command = command;
        this.obj = obj;
    }
    public void Execute()
    {
        var legalOrdersRepository = Ioc.Resolve<Dictionary<string, Dictionary<string, HashSet<string>>>>("LegalOrders.Repository");
        if (!legalOrdersRepository.ContainsKey(user) | !legalOrdersRepository[user].ContainsKey(command) | !legalOrdersRepository[user][command].Remove(obj))
        {
            throw new Exception("the order was not found");
        }
    }
}
