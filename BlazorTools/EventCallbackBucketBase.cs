using System.Collections.Concurrent;

namespace BlazorTools;

public abstract class EventCallbackBucketBase<TCallback>
{
    protected ConcurrentDictionary<EventSubscription, TCallback> EventCallbacks { get; } = [];

    public EventSubscription Subscribe(TCallback callback)
    {
        return Subscribe(new(), callback);
    }

    public EventSubscription Subscribe(EventSubscription subscription, TCallback callback)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(subscription, default);

        EventCallbacks.TryAdd(subscription, callback);
        return subscription;
    }

    public bool Unsubscribe(EventSubscription subscription)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(subscription, default);

        return EventCallbacks.TryRemove(subscription, out _);
    }

    public void Clear() => EventCallbacks.Clear();
}