using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using explorer_async.ViewModels;
using explorer_async.Views;

namespace explorer_async;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var fileSystemService = new Services.FileSystemService();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(fileSystemService),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

}
