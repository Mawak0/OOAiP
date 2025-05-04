<<<<<<< HEAD
using Xunit;
=======
﻿using Xunit;
>>>>>>> c173ec7a49059c4246b45cc517c2bfbf6611202c
namespace SpaceBattle.Lib;

public class TimerServiceTests
{
    [Fact]
    public async Task Test_ShouldSetIsTimeoutReachedAfterDelay()
    {
        var service = new TimerService();
        var delay = TimeSpan.FromMilliseconds(500);

        service.StartTimer(delay);

        Assert.False(service.IsTimeoutReached);

        await Task.Delay(delay + TimeSpan.FromMilliseconds(100));

        Assert.True(service.IsTimeoutReached);
    }

    [Fact]
    public async Task Test_ShouldNotSetIsTimeoutReachedBeforeDelay()
    {
        var service = new TimerService();
        var delay = TimeSpan.FromSeconds(1);

        service.StartTimer(delay);

        await Task.Delay(TimeSpan.FromMilliseconds(500));
        Assert.False(service.IsTimeoutReached);
    }

    [Fact]
    public void Test_CorrectInit()
    {
        var service = new TimerService();

        Assert.Equal(default(DateTime), service.startTime);
        Assert.Equal(default(TimeSpan), service.timeout);
        Assert.False(service.IsTimeoutReached);
    }
}
