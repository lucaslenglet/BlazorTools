using System.Collections.Concurrent;

namespace BlazorTools;

public class EventCallbackBucketBase<TCallback>
{
    protected ConcurrentDictionary<EventSubscription, TCallback> EventCallbacks { get; } = [];

    public EventSubscription Subscribe(TCallback callback)
    {
        var subscription = CreateSubscription();
        EventCallbacks.TryAdd(subscription, callback);
        return subscription;
    }

    public void Unsubscribe(EventSubscription subscription) =>
        EventCallbacks.TryRemove(subscription, out _);

    public void Clear() => EventCallbacks.Clear();

    protected EventSubscription CreateSubscription() =>
        EventSubscription.FromRevokeCallback(this, Unsubscribe);
}
