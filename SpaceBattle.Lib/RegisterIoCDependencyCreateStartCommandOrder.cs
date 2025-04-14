using App;
namespace SpaceBattle.Lib;

public class RegisterIoCDependencyCreateStartMoveOrder : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
                "IoC.Register",
                "Game.CreateStartMoveOrder",
                (object[] arg) =>
                {
                    var order = new Dictionary<string, object>
                    {
                        { "Action", "Move" },
                        { "Key", ((IDictionary<string, object>)arg[0])["Id"] },
                        { "Args", Ioc.Resolve<IMoving>("Adapters.IMovingObject", (IDictionary<string, object>)arg[0]) }
                    };
                    return order;
                }).Execute();
    }
}
