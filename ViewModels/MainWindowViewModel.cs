using ReactiveUI;
using DynamicData;
using System.Reactive;
using System;
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
        Files.CountChanged
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(count => Console.WriteLine($"{count}"));
    }

    private SourceList<string> _files = new();
    public IObservableList<string> Files => _files.AsObservableList();
    public ReactiveCommand<Unit, Unit> LoadFilesCommand;

    private async Task LoadFilesAsync()
    {
        var filePaths = await _fileSystemService.EnumerateFilesAsync(_path);
        _files.Edit(innerList =>
        {
            innerList.Clear();
            innerList.AddRange(filePaths);
        });
    }

}
