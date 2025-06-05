using System.Diagnostics;
using Serilog;

public class LoggingService
{
    public static void ConfigureLogger()
    {
        // Log Avalonia events to console
        Trace.Listeners.Add(new ConsoleTraceListener());

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .Enrich.WithThreadId()
            .CreateLogger();
    }
}
