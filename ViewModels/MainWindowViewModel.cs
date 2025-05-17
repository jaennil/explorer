using System.Collections.Generic;
using System.IO;
using System;

namespace explorer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _currentPath = @"~";
    private IEnumerable<string> _directories;

    public MainWindowViewModel()
    {
        _currentPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        _directories = Directory.EnumerateDirectories(_currentPath);
        
        foreach (var dir in _directories)
        {
            Console.WriteLine(dir);
        }
    }
}
