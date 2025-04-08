using App;
namespace SpaceBattle.Lib;

public class CollisionCommand : ICommand
{
    private readonly IColliding firstObj;

    private readonly IColliding secondObj;

    private readonly ICommand command;

    public CollisionCommand(IColliding firstObj, IColliding secondObj, ICommand command)
    {
        this.firstObj = firstObj;
        this.secondObj = secondObj;
        this.command = command;
    }
    public void Execute()
    {
        var deltaPosition = firstObj.Position.Values
        .Zip(secondObj.Position.Values, (a, b) => a - b)
        .ToArray();

        var deltaVelocity = firstObj.Velocity.Values
        .Zip(secondObj.Velocity.Values, (a, b) => a - b)
        .ToArray();

        var isCollision = Ioc.Resolve<bool>("Game.IsCollision", deltaPosition, deltaVelocity, firstObj.Shape, secondObj.Shape);
        if (isCollision)
        {
            command.Execute();
        }
    }
}
