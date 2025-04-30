namespace SpaceBattle.Lib;

public interface ITimerService
{
    bool IsTimeoutReached { get; }
    void StartTimer(TimeSpan timeout);
}