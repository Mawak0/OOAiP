namespace SpaceBattle.Lib;

public class TimerService : ITimerService
{
    private DateTime _startTime;
    private TimeSpan _timeout;

    public bool IsTimeoutReached { get; private set; }

    public void StartTimer(TimeSpan timeout)
    {
        _startTime = DateTime.Now;
        _timeout = timeout;
        Task.Run(async () =>
        {
            await Task.Delay(timeout);
            IsTimeoutReached = true;
        });
    }
}
