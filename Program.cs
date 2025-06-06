using Avalonia;
using System;

namespace explorer;

sealed class Program
{
    [STAThread]
    public static void Main(string[] args)
    {

        LoggingService.ConfigureLogger();

        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont();
            // .LogToTrace(LogEventLevel.Verbose);
}
