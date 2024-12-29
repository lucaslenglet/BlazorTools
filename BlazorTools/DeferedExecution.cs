namespace BlazorTools;

public readonly struct DeferedExecution(Action action) : IDisposable
{
    public void Dispose() => action();
}
