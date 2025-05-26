namespace SpaceBattle.Lib;
using App;

public class AuthCommand : ICommand
{
    private readonly string user;
    private readonly string command;
    private readonly string obj;
    public AuthCommand(string user_, string command_, string obj_)
    {
        user = user_;
        command = command_;
        obj = obj_;
    }

    public void Execute()
    {
        var legalOrdersRepository = Ioc.Resolve<Dictionary<string, Dictionary<string, HashSet<string>>>>("LegalOrders.Repository");

        bool flagCommandCheck = false;
        if (legalOrdersRepository.ContainsKey(user) && legalOrdersRepository[user].ContainsKey(command))
        {
            legalOrdersRepository[user].TryGetValue(command, out HashSet<string>? objects);
            flagCommandCheck = objects!.Any(p => p == obj);
        }

        if (!flagCommandCheck)
        {
            throw new Exception("Сommand not allowed");
        }
    }
}
