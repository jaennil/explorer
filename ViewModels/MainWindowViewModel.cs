using ReactiveUI;
using DynamicData;
using System.Reactive;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using explorer_async.Services;
using System.Threading.Tasks;

namespace explorer_async.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private FileSystemService _fileSystemService;
    private string _path;

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

    private SourceList<string> _filesSource = new();
    public ReadOnlyObservableCollection<string> Files => _files;
    private ReadOnlyObservableCollection<string> _files;
    public ReactiveCommand<Unit, Unit> LoadFilesCommand;

    private async Task LoadFilesAsync()
    {
        var filePaths = await _fileSystemService.EnumerateFilesAsync(_path);
        _filesSource.Edit(innerList =>
        {
            innerList.Clear();
            innerList.AddRange(filePaths);
        });
    }

}
