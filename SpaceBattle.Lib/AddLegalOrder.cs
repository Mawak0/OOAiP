namespace SpaceBattle.Lib;
using App;

public class AddLegalOrder : ICommand
{
    private readonly string user;
    private readonly string command;
    private readonly string[] obj;
    public AddLegalOrder(string user_, string command_, string[] obj_)
    {
        user = user_;
        command = command_;
        obj = obj_;
    }
    public void Execute()
    {
        var d = Ioc.Resolve<Dictionary<string, Dictionary<string, string[]>>>("LegalOrder.Repository");
        d.Add(user, new Dictionary<string, string[]>() { { command, obj } });
    }
}
