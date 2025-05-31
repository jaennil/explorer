using Serilog;

namespace explorer_async.Services;

public static class LoggingService
{
    public static void ConfigureLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            // .Enrich()
            .WriteTo.Console()
            .CreateLogger();

        Log.Information("=== Explorer app started ===");
        Log.Debug("Debug logging is enabled");
    }
}
