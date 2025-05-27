using ReactiveUI;
using DynamicData;
using System.Reactive;
using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using explorer_async.Services;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace explorer_async.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private FileSystemService _fileSystemService;
    private string _path;
    private SourceList<FileInfo> _filesSource = new();
    private ReadOnlyObservableCollection<FileInfo> _files;

    public MainWindowViewModel(FileSystemService fileSystemService)
    {
        _fileSystemService = fileSystemService;
        _path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        LoadFilesCommand = ReactiveCommand.CreateFromTask(LoadFilesAsync);
        LoadFilesCommand.Execute().Subscribe();
        
        _filesSource.Connect()
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _files)
            .DisposeMany()
            .Subscribe();

        _filesSource.CountChanged
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(count => Console.WriteLine($" Loaded {count} files"));
    }

    public ReadOnlyObservableCollection<FileInfo> Files => _files;
    public ReactiveCommand<Unit, Unit> LoadFilesCommand;

    private async Task LoadFilesAsync()
    {
        var filePaths = await _fileSystemService.ListFilesAsync(_path);
        // var topLevel = TopLevel.GetTopLevel
        // var storageProvider = TopLevel.StorageProvider;
        // var filePaths = await 
        _filesSource.Edit(innerList =>
        {
            innerList.Clear();
            innerList.AddRange(filePaths);
        });
    }

}
