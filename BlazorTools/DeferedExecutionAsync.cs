namespace BlazorTools;

public class DeferedExecutionAsync(Func<Task> func) : IAsyncDisposable
{
    public async ValueTask DisposeAsync() => await func();

    public static DeferedExecutionAsync For(Func<Task> func) => new(func);

    public static DeferedExecutionAsync<TValue> For<TValue>(TValue value, Func<TValue, Task> func) => new(value, func);
}

public class DeferedExecutionAsync<TValue>(TValue value, Func<TValue, Task> func) : IAsyncDisposable
{
    public TValue Value => value;

    public async ValueTask DisposeAsync() => await func(value);

    public static DeferedExecutionAsync<TValue> For(TValue value, Func<TValue, Task> func) => new(value, func);
}