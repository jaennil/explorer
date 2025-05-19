using System;
using System.Collections.ObjectModel;
using System.IO;

namespace explorer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _currentPath;
    public string CurrentPath
    {
        get => _currentPath;
        private set
        {
            if (SetProperty(ref _currentPath, value))
            {
                UpdateDirectories();
            }
        }
    }
    
    private ObservableCollection<Models.Directory> _directories = [];
    public ObservableCollection<Models.Directory> Directories
    {
        get => _directories;
        private set => SetProperty(ref _directories, value);
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
        CurrentPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        var files = Directory.EnumerateFiles(CurrentPath);
        foreach (var filePath in files)
        {
            var fileInfo = new FileInfo(filePath);
            _files.Add(fileInfo);
        }
    }

    private void UpdateDirectories()
    {
        _directories.Clear();
        
        var dirs = Directory.EnumerateDirectories(CurrentPath);
        foreach (var dirPath in dirs)
        {
            var dir = new Models.Directory(dirPath);
            _directories.Add(dir);
        }
    }

    private void UpdateFiles()
    {
        _files.Clear();
        var files = Directory.EnumerateFiles(CurrentPath);
        foreach (var filePath in files)
        {
            var fileInfo = new FileInfo(filePath);
            _files.Add(fileInfo);
        }
    }

    public void GoToDirectory(Models.Directory directory)
    {
        CurrentPath = directory.Info.FullName;
        UpdateDirectories();
    }

    public void Back()
    {
        var parent = Path.GetDirectoryName(CurrentPath);
        if (parent == null)
        {
            Console.WriteLine("No directory upper");
            return;
        }
        
        CurrentPath = parent;
    }
}
