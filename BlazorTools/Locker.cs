namespace BlazorTools;

public class Locker : IAsyncDisposable
{
    private readonly DeferedExecutionAsync? deferedToggleDisable;
    private readonly DeferedExecutionAsync<MinimumExecutionTime>? deferedMinimumTimeExecution;

    private readonly Lock _lock = new();
    private bool isDisposingOrDisposed = false;

    public Locker(TimeSpan? minimumTime, Func<Task>? toggleDisable, CancellationToken cancellationToken)
    {
        if (toggleDisable is not null)
        {
            deferedToggleDisable = DeferedExecutionAsync.For(
                func: toggleDisable);
        }

        if (minimumTime is not null)
        {
            deferedMinimumTimeExecution = DeferedExecutionAsync.For(
                value: MinimumExecutionTime.FromNowAtLeast(minimumTime.Value),
                func: min => min.WaitIfUnder(cancellationToken));
        }
    }

    public async ValueTask DisposeAsync()
    {
        lock (_lock)
        {
            if (isDisposingOrDisposed)
            {
                return;
            }

            isDisposingOrDisposed = true;
        }

        if (deferedMinimumTimeExecution is not null)
        {
            await deferedMinimumTimeExecution.DisposeAsync();
        }

        if (deferedToggleDisable is not null)
        {
            await deferedToggleDisable.DisposeAsync();
        }
    }
}