namespace SpaceBattle.Lib;

public class RotateCommand : ICommand
{
    private readonly IRotate obj;
    public RotateCommand(IRotate obj)
    {
        this.obj = obj;
    }
    public void Execute()
    {
        obj.PositionAngle += obj.VelocityAngle;
    }
}
