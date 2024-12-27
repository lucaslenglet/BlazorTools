using Microsoft.AspNetCore.Components;

namespace BlazorTools;

public readonly struct EventSubscription
{
    private readonly EventCallback<EventSubscription>? revokeCallback;

    public EventSubscription()
    {
#if NET8_0
        Id = Guid.NewGuid();
#elif NET9_0_OR_GREATER
        Id = Guid.CreateVersion7();
#endif
    }

    public EventSubscription(EventCallback<EventSubscription> revokeCallback)
        : this()
    {
        this.revokeCallback = revokeCallback;
    }

    public Guid Id { get; }

    public static EventSubscription FromRevokeCallback(object receiver, Action<EventSubscription> revokeCallback) =>
        new(EventCallback.Factory.Create(receiver, revokeCallback));

    public Task Revoke() => revokeCallback?.InvokeAsync(this) ?? Task.CompletedTask;
}
