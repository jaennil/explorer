using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using DynamicData;
using ReactiveUI;

namespace explorer.ViewModels;

public class MainWindowViewModel : ViewModelBase, IActivatableViewModel
{
    private readonly SourceList<object> _fileSystemEntriesSource = new();
    private string _currentPath = string.Empty;
    private bool _imagesOnly = true;
    private int _fileSystemEntriesAmount;
    private int _directoriesAmount;
    private int _filesAmount;
    private readonly List<FileInfo> _files = [];
    private List<Models.Directory> _directories = [];
    private ObservableCollection<object> _fileSystemEntries = [];

    public string CurrentPath
    {
        get => _currentPath;
        set
        {
            _currentPath = value;
            UpdateFileSystemEntries();
            // UpadteCurrentFileParts();
        }
    }

    public bool ImagesOnly
    {
        get => _imagesOnly;
        set
        {
            this.RaiseAndSetIfChanged(ref _imagesOnly, value);
            UpdateFiles();
            MergeFoldersAndFiles();
        }
    }

    public int DirectoriesAmount
    {
        get => _directoriesAmount;
        private set => this.RaiseAndSetIfChanged(ref _directoriesAmount, value);
    }

    public int FilesAmount
    {
        get => _filesAmount;
        private set => this.RaiseAndSetIfChanged(ref _filesAmount, value);
    }

    public int FilesystemEntriesAmount
    {
        get => _fileSystemEntriesAmount;
        private set => this.RaiseAndSetIfChanged(ref _fileSystemEntriesAmount, value);
    }

    public ObservableCollection<string> CurrentPathParts { get; private set; }

    public ObservableCollection<object> FileSystemEntries
    {
        get => _fileSystemEntries;
        private set => this.RaiseAndSetIfChanged(ref _fileSystemEntries, value);
    }

    public ReactiveCommand<Unit, Unit> BackCommand { get; }

    public ViewModelActivator Activator { get; } = new();

    public MainWindowViewModel()
    {
        CurrentPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        // TODO: UpdateCurrentPathParts();
        var pathSeparatorChar = Path.DirectorySeparatorChar;
        var pathParts = CurrentPath.Split(pathSeparatorChar);
        pathParts[0] = pathSeparatorChar.ToString();
        CurrentPathParts = new ObservableCollection<string>(pathParts);

        BackCommand = ReactiveCommand.Create(Back);

        this.WhenActivated(disposables =>
        {
            Disposable.Create(() => _fileSystemEntriesSource.Dispose()).DisposeWith(disposables);
        });
    }

    private void MergeFoldersAndFiles()
    {
        _fileSystemEntries.Clear();
        foreach (var dir in _directories)
        {
            _fileSystemEntries.Add(dir);
        }
        foreach (var file in _files)
        {
            _fileSystemEntries.Add(file);
        }
        FilesystemEntriesAmount = FilesAmount + DirectoriesAmount;
    }

    private void UpdateFileSystemEntries()
    {
        UpdateDirectories();
        UpdateFiles();
        MergeFoldersAndFiles();
    }

    private void UpdateFiles()
    {
        _files.Clear();
        var files = Directory.EnumerateFiles(CurrentPath);
        foreach (var filePath in files)
        {
            var fileInfo = new FileInfo(filePath);
            if (ImagesOnly)
            {
                var extension = Path.GetExtension(filePath);
                if (!Models.Directory.SupportedImageExtensions.Contains(extension)) continue;
                _files.Add(fileInfo);
            }
            else
            {
                _files.Add(fileInfo);
            }
        }
        FilesAmount = _files.Count;
    }

    private void UpdateDirectories()
    {
        _directories = Directory.EnumerateDirectories(CurrentPath)
            .Select(dir => new Models.Directory(dir))
            .ToList();
        DirectoriesAmount = _directories.Count;
    }

    public void GoToDirectory(Models.Directory directory)
    {
        CurrentPath = directory.Info.FullName;
        UpdateFileSystemEntries();
    }

    public void Back()
    {
        var parent = Path.GetDirectoryName(CurrentPath);
        if (!string.IsNullOrEmpty(parent))
        {
            CurrentPath = parent;
        }
    }
}
