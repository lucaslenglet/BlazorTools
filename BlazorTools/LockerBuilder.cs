namespace BlazorTools;

public class LockerBuilder(CancellationToken cancellationToken = default)
{
    private TimeSpan? minimumTime;
    private Func<Task>? toggleDisable;
    private CancellationToken cancellationToken = cancellationToken;

    public static LockerBuilder Create() => new();
    public static LockerBuilder Create(CancellationToken cancellationToken) => new(cancellationToken);

    public LockerBuilder MinimumTime(TimeSpan minimumTime)
    {
        this.minimumTime = minimumTime;
        return this;
    }

    public LockerBuilder ToggleDisableUsing(Func<Task> toggleDisable)
    {
        this.toggleDisable = toggleDisable;
        return this;
    }

    public Locker Build() => new(minimumTime, toggleDisable, cancellationToken);

    public async Task<Locker> BuildAndToggle()
    {
        ArgumentNullException.ThrowIfNull(toggleDisable);

        await toggleDisable.Invoke();

        return Build();
    }
}
