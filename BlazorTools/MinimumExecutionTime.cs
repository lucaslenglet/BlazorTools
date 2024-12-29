using System.Diagnostics;

namespace BlazorTools;

public readonly struct MinimumExecutionTime()
{
    public MinimumExecutionTime(TimeSpan minimumTime)
        : this()
    {
        MinimumTime = minimumTime;
    }

    public readonly long Start = Stopwatch.GetTimestamp();
    public readonly TimeSpan MinimumTime = TimeSpan.Zero;

    public Task WaitIfUnder(TimeSpan minimumTime, CancellationToken cancellationToken = default)
    {
        var elaspsed = Stopwatch.GetElapsedTime(Start);
        if (elaspsed >= minimumTime)
        {
            return Task.CompletedTask;
        }

        var timeToWait = Convert.ToInt32((minimumTime - elaspsed).TotalMilliseconds);
        return Task.Delay(timeToWait, cancellationToken);
    }

    public Task WaitIfUnder(CancellationToken cancellationToken = default)
    {
        return WaitIfUnder(MinimumTime, cancellationToken);
    }

    public static MinimumExecutionTime FromNowAtLeast(TimeSpan minimumTime) => new(minimumTime);
    public static MinimumExecutionTime FromNowAtLeast(int milliseconds) => new(TimeSpan.FromMilliseconds(milliseconds));
}