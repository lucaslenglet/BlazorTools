namespace BlazorTools;

public class EventCallbackBucket : EventCallbackBucketBase<Func<Task>>
{
    public EventSubscription SubscribeSync(Action callback)
    {
        var subscription = new EventSubscription();
        EventCallbacks.TryAdd(subscription, () =>
        {
            callback();
            return Task.CompletedTask;
        });
        return subscription;
    }

    public async Task InvokeAsync()
    {
        foreach (var callback in EventCallbacks)
        {
            await callback.Value();
        }
    }

    public async Task InvokeExceptAsync(params IEnumerable<EventSubscription> eventSubscriptionsToExclude)
    {
        foreach (var callback in EventCallbacks.Where(kv => !eventSubscriptionsToExclude.Contains(kv.Key)))
        {
            await callback.Value();
        }
    }
}