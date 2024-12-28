using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BlazorTools;

public static class LoggerExtensions
{
    private static readonly long startTicks = TimeProvider.System.GetTimestamp();
    private static long previousTicks = TimeProvider.System.GetTimestamp();
    private readonly static Lock previousTicksLock = new();

    [Conditional("DEBUG")]
    public static void LogRender(this ILogger logger,
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
    {
        const LogLevel logLevel = LogLevel.Debug;
        if (logger.IsEnabled(logLevel))
        {
            lock (previousTicksLock)
            {
                var currentTicks = TimeProvider.System.GetTimestamp();
                logger.Log(
                    logLevel,
                    message,
                    TimeProvider.System.GetElapsedTime(startTicks, currentTicks).Duration(),
                    TimeProvider.System.GetElapsedTime(previousTicks, currentTicks),
                    Environment.CurrentManagedThreadId,
                    Path.GetFileNameWithoutExtension(sourceFilePath));
                previousTicks = currentTicks;
            }
        }
    }

    const string message = "{ticksStart} | {ticksPrevious} | {threadId} | {componentName}";
}
