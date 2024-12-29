namespace BlazorTools;

public class DeferedExecution(Action action) : IDisposable
{
    public void Dispose() => action();

    public static DeferedExecution For(Action action) => new(action);

    public static DeferedExecution<TValue> For<TValue>(TValue value, Action<TValue> action) => new(value, action);
}

public class DeferedExecution<TValue>(TValue value, Action<TValue> action) : IDisposable
{
    public TValue Value => value;

    public void Dispose() => action(value);

    public static DeferedExecution<TValue> For(TValue value, Action<TValue> action) => new(value, action);
}