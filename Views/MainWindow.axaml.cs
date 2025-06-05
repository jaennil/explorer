using Avalonia.Controls;
using explorer.ViewModels;

namespace explorer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var fileSystemService = new FileSystemService(this.StorageProvider);

        DataContext = new MainWindowViewModel(fileSystemService);
    }
}
