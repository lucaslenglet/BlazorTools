namespace BlazorTools;

public class EventCallbackBucket<TValue> : EventCallbackBucketBase<Func<TValue, Task>>
{
    public EventSubscription Subscribe(Action<TValue> callback)
    {
        var subscription = new EventSubscription();
        EventCallbacks.TryAdd(subscription, (value) =>
        {
            callback(value);
            return Task.CompletedTask;
        });
        return subscription;
    }

    public async Task InvokeAsync(TValue value)
    {
        foreach (var callback in EventCallbacks)
        {
            await callback.Value(value);
        }
    }

    public async Task InvokeExceptAsync(TValue value, params IEnumerable<EventSubscription> eventSubscriptionsToExclude)
    {
        foreach (var callback in EventCallbacks.Where(kv => eventSubscriptionsToExclude.Contains(kv.Key)))
        {
            await callback.Value(value);
        }
    }
}
