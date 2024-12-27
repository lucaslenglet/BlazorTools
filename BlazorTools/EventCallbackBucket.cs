using Microsoft.AspNetCore.Components;

namespace BlazorTools;

public class EventCallbackBucket : EventCallbackBucketBase<EventCallback>
{
    public EventSubscription Subscribe(object receiver, Action callback)
    {
        var subscription = CreateSubscription();
        EventCallbacks.TryAdd(subscription, EventCallback.Factory.Create(receiver, callback));
        return subscription;
    }

    public EventSubscription Subscribe(object receiver, Func<Task> callback)
    {
        var subscription = CreateSubscription();
        EventCallbacks.TryAdd(subscription, EventCallback.Factory.Create(receiver, callback));
        return subscription;
    }

    public async Task InvokeAsync()
    {
        foreach (var callback in EventCallbacks)
        {
            await callback.Value.InvokeAsync();
        }
    }
}