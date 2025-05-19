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

        var dirs = Directory.EnumerateDirectories(_currentPath);

        foreach (var dirPath in dirs)
        {
            var dirInfo = new DirectoryInfo(dirPath);
            _directories.Add(dirInfo);
        }

        var files = Directory.EnumerateFiles(_currentPath);

        foreach (var filePath in files)
        {
            var fileInfo = new FileInfo(filePath);
            _files.Add(fileInfo);
        }
    }
}
