namespace SpaceBattle.Lib;

public class TimerService : ITimerService
{
    public DateTime startTime;
    public TimeSpan timeout;

    public bool IsTimeoutReached { get; private set; }

    public void StartTimer(TimeSpan time)
    {
        startTime = DateTime.Now;
        timeout = time;
        Task.Run(async () =>
        {
            await Task.Delay(timeout);
            IsTimeoutReached = true;
        });
    }
}
