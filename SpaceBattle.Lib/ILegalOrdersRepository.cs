namespace SpaceBattle.Lib;

public interface ILegalOrdersRepository
{
    public IDictionary<string, IDictionary<string, HashSet<string>>> legalOrdersRepository { get; }
    public void AddLegalOrder(string user, string command, HashSet<string> obj);
    public void RemoveLegalOrder(string user, string command, string obj);
}
