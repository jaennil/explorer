using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using explorer.Models;
using explorer.ViewModels;

namespace explorer.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border { Tag: Directory directory })
        {
            ViewModel.GoToDirectory(directory);
        }
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        ViewModel.Back();
    }
}
