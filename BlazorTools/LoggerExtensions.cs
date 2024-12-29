using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BlazorTools;

public static class LoggerExtensions
{
    private static readonly long startTicks = TimeProvider.System.GetTimestamp();
    private static long previousTicks = TimeProvider.System.GetTimestamp();
    private readonly static Lock logRenderLock = new();
    private static int logRenderId = 0;

    [Conditional("DEBUG")]
    public static void LogRender(this ILogger logger,
        object? value = default,
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
    {
        const LogLevel logLevel = LogLevel.Debug;
        if (logger.IsEnabled(logLevel))
        {
            lock (logRenderLock)
            {
                var currentTicks = TimeProvider.System.GetTimestamp();
                logger.Log(
                    logLevel,
                    message,
                    TimeProvider.System.GetElapsedTime(startTicks, currentTicks).Duration(),
                    TimeProvider.System.GetElapsedTime(previousTicks, currentTicks),
                    Environment.CurrentManagedThreadId,
                    ++logRenderId,
                    Path.GetFileNameWithoutExtension(sourceFilePath),
                    value?.ToString() ?? "NA");
                previousTicks = currentTicks;
            }
        }
    }

    const string message = "{ticksStart} | {ticksPrevious} | {threadId} | {logRenderId} | {componentName} ({value})";
}
