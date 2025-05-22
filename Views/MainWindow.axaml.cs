using Avalonia.Controls;
using Avalonia.ReactiveUI;
using explorer_async.ViewModels;

namespace explorer_async.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}