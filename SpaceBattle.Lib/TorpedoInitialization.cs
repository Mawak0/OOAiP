namespace SpaceBattle.Lib;

public class TorpedoInitialization : ICommand
{
    private readonly IDictionary<string, object> torpedo;
    private readonly IShooting shooterObj;
    public TorpedoInitialization(IDictionary<string, object> torpedo, IShooting shooterObj)
    {
        this.torpedo = torpedo;
        this.shooterObj = shooterObj;
    }
    public void Execute()
    {
        torpedo["Position"] = shooterObj.Position + shooterObj.FireVelocity;
        torpedo["Velocity"] = shooterObj.Velocity + shooterObj.FireVelocity;

    }
}
