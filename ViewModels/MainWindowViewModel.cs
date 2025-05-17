using System.IO;
using System;
using System.Collections.ObjectModel;

namespace explorer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _currentPath = @"~";
    private ObservableCollection<DirectoryInfo> _directories = [];
    public ObservableCollection<DirectoryInfo> Directories
    {
        get => _directories;
        private set
        {
            _directories = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<FileInfo> _files = [];
    public ObservableCollection<FileInfo> Files
    {
        get => _files;
        private set
        {
            _files = value;
            OnPropertyChanged();
        }
    }

    public MainWindowViewModel()
    {
        _currentPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        Console.WriteLine($"_currentPath: {_currentPath}");

        var dirs = Directory.EnumerateDirectories(_currentPath);

        Console.WriteLine($"dirs: {dirs}");

        foreach (var dirPath in dirs)
        {
            Console.WriteLine($"dirPath: {dirPath}");
            var dirInfo = new DirectoryInfo(dirPath);
            Console.WriteLine($"dirInfo: {dirInfo}");
            _directories.Add(dirInfo);
        }

        var files = Directory.EnumerateFiles(_currentPath);

        Console.WriteLine($"files: {files}");

        foreach (var filePath in files)
        {
            Console.WriteLine($"filePath: {filePath}");
            var fileInfo = new FileInfo(filePath);
            _files.Add(fileInfo);
        }
    }
}
