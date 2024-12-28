using System.Collections.Concurrent;

namespace BlazorTools;

public class EventCallbackBucketBase<TCallback>
{
    protected ConcurrentDictionary<EventSubscription, TCallback> EventCallbacks { get; } = [];

    public EventSubscription Subscribe(TCallback callback)
    {
        return Subscribe(new(), callback);
    }

    public EventSubscription Subscribe(EventSubscription subscription, TCallback callback)
    {
        EventCallbacks.TryAdd(subscription, callback);
        return subscription;
    }

    public bool Unsubscribe(EventSubscription subscription) =>
        EventCallbacks.TryRemove(subscription, out _);

    public void Clear() => EventCallbacks.Clear();
}