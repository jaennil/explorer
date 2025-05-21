using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using explorer.Models;
using explorer.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace explorer.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.BindCommand(ViewModel,
                    vm => vm.BackCommand,
                    v => v.BackButton)
                .DisposeWith(disposables);
        });
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border { Tag: Directory directory })
        {
            ViewModel.GoToDirectory(directory);
        }
    }

    private void Button_Path_OnClick(object? sender, RoutedEventArgs e)
    {
        // if (sender is Button {})
    }
}
