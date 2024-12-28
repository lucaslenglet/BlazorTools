namespace BlazorTools;

public readonly record struct EventSubscription()
{
    public Guid Id { get; } = Guid.NewGuid();
}
