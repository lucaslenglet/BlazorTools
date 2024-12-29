namespace BlazorTools;

public readonly struct DeferedExecutionAsync(Func<ValueTask> func) : IAsyncDisposable
{
    public ValueTask DisposeAsync() => func();
}