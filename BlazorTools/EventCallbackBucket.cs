namespace BlazorTools;

public class EventCallbackBucket : EventCallbackBucketBase<Func<Task>>
{
    public EventSubscription Subscribe(Action callback)
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
}