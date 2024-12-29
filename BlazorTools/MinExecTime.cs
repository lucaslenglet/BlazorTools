using System.Diagnostics;

namespace BlazorTools;

public readonly struct MinExecTime()
{
    public readonly long Start = Stopwatch.GetTimestamp();

    public Task WaitBeforeContinue(TimeSpan minimumTime, CancellationToken cancellationToken = default)
    {
        var elaspsed = Stopwatch.GetElapsedTime(Start);
        if (elaspsed >= minimumTime)
        {
            return Task.CompletedTask;
        }

        var timeToWait = minimumTime - elaspsed;
        return Task.Delay(Convert.ToInt32(timeToWait.TotalMilliseconds), cancellationToken);
    }

    public static MinExecTime FromNow() => new();
}