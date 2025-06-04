using Avalonia.Controls;
using explorer.ViewModels;

namespace explorer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel(this.StorageProvider);
    }
}
