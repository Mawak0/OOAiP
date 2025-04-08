namespace SpaceBattle.Lib;

public class LegalOrdersRepository : ILegalOrdersRepository
{
    public IDictionary<string, IDictionary<string, HashSet<string>>> legalOrdersRepository { get; }
    public bool flagCommandCheck = false;

    public LegalOrdersRepository(IDictionary<string, IDictionary<string, HashSet<string>>> legalOrdersRepository)
    {
        this.legalOrdersRepository = legalOrdersRepository;
    }

    public void AddLegalOrder(string user, string command, HashSet<string> obj)
    {
        Dictionary<string, HashSet<string>> internal_dict = new Dictionary<string, HashSet<string>>() { { command, obj } };
        legalOrdersRepository.TryAdd(user, internal_dict);

        legalOrdersRepository[user].TryAdd(command, obj);

        legalOrdersRepository[user].TryGetValue(command, out HashSet<string>? objects);
        legalOrdersRepository[user][command] = objects!.Union(obj.Except(objects!)).ToHashSet();
    }

    public void RemoveLegalOrder(string user, string command, string obj)
    {
        var flagSuccesRemove = legalOrdersRepository[user][command].Remove(obj);
        if (!flagSuccesRemove)
        {
            throw new Exception("the order was not found");
        }
    }
}
