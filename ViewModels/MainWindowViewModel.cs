using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace explorer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _currentPath = string.Empty;
    private string CurrentPath
    {
        get => _currentPath;
        set
        {
            if (!SetProperty(ref _currentPath, value)) return;
            UpdateFileSystemEntries();
        }
    }

    [ObservableProperty]
    private bool _imagesOnly = true;

    public int DirectoriesAmount { get; private set; }

    public int FilesAmount { get; private set; }

    public int FilesystemEntriesAmount { get; private set; }

    private List<FileInfo> _files = [];

    private List<Models.Directory> _directories = [];

    public ObservableCollection<string> CurrentPathParts { get; }

    [ObservableProperty]
    private ObservableCollection<object> _fileSystemEntries = [];

    public MainWindowViewModel()
    {
        CurrentPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var pathSeparatorChar = Path.DirectorySeparatorChar;
        var pathParts = CurrentPath.Split(pathSeparatorChar);
        pathParts[0] = pathSeparatorChar.ToString();
        CurrentPathParts = new ObservableCollection<string>(pathParts);
    }

    private void UpdateFileSystemEntries()
    {
        FileSystemEntries.Clear();
        DirectoriesAmount = 0;
        FilesAmount = 0;

        var dirs = Directory.EnumerateDirectories(CurrentPath);
        foreach (var dirPath in dirs)
        {
            var dir = new Models.Directory(dirPath);
            FileSystemEntries.Add(dir);
            DirectoriesAmount++;
        }

        var files = Directory.EnumerateFiles(CurrentPath);
        foreach (var filePath in files)
        {
            var fileInfo = new FileInfo(filePath);
            if (ImagesOnly)
            {
                var extension = Path.GetExtension(filePath);
                if (!Models.Directory.SupportedImageExtensions.Contains(extension)) continue;
                FileSystemEntries.Add(fileInfo);
                FilesAmount++;
            }
            else
            {
                FileSystemEntries.Add(fileInfo);
                FilesAmount++;
            }

        }

        FilesystemEntriesAmount = FileSystemEntries.Count;
    }

    private void UpdateFiles()
    {
        var files = Directory.EnumerateFiles(CurrentPath);
        List<FileInfo> newFiles = [];
        foreach (var filePath in files)
        {
            var fileInfo = new FileInfo(filePath);
            if (ImagesOnly)
            {
                var extension = Path.GetExtension(filePath);
                if (!Models.Directory.SupportedImageExtensions.Contains(extension)) continue;
                newFiles.Add(fileInfo);
            }
            else
            {
                newFiles.Add(fileInfo);
            }
        }

        _files = newFiles;
        FilesAmount = newFiles.Count;
        OnPropertyChanged();
    }

    private void UpdateDirectories()
    {
        _directories = Directory.EnumerateDirectories(CurrentPath)
            .Select(dir => new Models.Directory(dir))
            .ToList();
    }

    public void GoToDirectory(Models.Directory directory)
    {
        CurrentPath = directory.Info.FullName;
        UpdateFileSystemEntries();
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
