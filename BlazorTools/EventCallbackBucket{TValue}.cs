using Microsoft.AspNetCore.Components;

namespace BlazorTools;

public class EventCallbackBucket<TValue> : EventCallbackBucketBase<EventCallback<TValue>>
{
    public EventSubscription Subscribe(object receiver, Action<TValue> callback)
    {
        var subscription = CreateSubscription();
        EventCallbacks.TryAdd(subscription, EventCallback.Factory.Create(receiver, callback));
        return subscription;
    }

    public EventSubscription Subscribe(object receiver, Func<TValue, Task> callback)
    {
        var subscription = CreateSubscription();
        EventCallbacks.TryAdd(subscription, EventCallback.Factory.Create(receiver, callback));
        return subscription;
    }

    public async Task InvokeAsync(TValue value)
    {
        foreach (var callback in EventCallbacks)
        {
            await callback.Value.InvokeAsync(value);
        }
    }
}
