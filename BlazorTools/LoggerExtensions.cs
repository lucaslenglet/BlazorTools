using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BlazorTools;

public static class LoggerExtensions
{
    private static readonly long startTicks = TimeProvider.System.GetTimestamp();
    private static long previousTicks = default;
    private readonly static Lock previousTicksLock = new();

    [Conditional("DEBUG")]
    public static void LogRender(this ILogger logger,
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
    {
        if (logger.IsEnabled(LogLevel.Debug))
        {
            lock (previousTicksLock)
            {
                var currentTicks = TimeProvider.System.GetTimestamp();
                logger.LogDebug(
                    message,
                    TimeProvider.System.GetElapsedTime(startTicks, currentTicks).Duration(),
                    TimeProvider.System.GetElapsedTime(previousTicks, currentTicks),
                    Environment.CurrentManagedThreadId,
                    Path.GetFileNameWithoutExtension(sourceFilePath),
                    sourceLineNumber);
                previousTicks = currentTicks;
            }
        }
    }

    const string message = "{ticks} | {fromPreviousTicks} | {threadId} | Rendering component: {componentName} line {sourceLineNumber}";
}
