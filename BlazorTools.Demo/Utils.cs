using System.Diagnostics;

namespace BlazorTools.Demo;

public static class Utils
{
    public static void LogRender(this ILogger logger, string componentName)
    {
        logger.LogInformation(message, Stopwatch.GetTimestamp(), componentName);
    }

    const string message = """
        Component rendering...
        | ticks: {ticks}
        | componentName: {componentName}
        """;
}
