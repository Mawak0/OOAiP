namespace SpaceBattle.Lib;

public interface IShooting
{
    Vector Position { get; }
    Vector Velocity { get; }
    Vector FireVelocity { get; }
}
