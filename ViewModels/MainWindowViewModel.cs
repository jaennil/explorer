using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;

namespace explorer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<IStorageItem> _storageItems = [];

    private FileSystemService _fileSystemService;

    public MainWindowViewModel(FileSystemService fileSystemService)
    {
        Log.Debug("MainWindowViewModel constructor");

        _fileSystemService = fileSystemService;

        init();
    }

    private async Task init()
    {
        string homeDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        var dir = await _fileSystemService.GetDirectoryFromPathAsync(homeDirectoryPath);
        if (dir == null)
        {
            Log.Error("cant find directory {homeDirectoryPath}", homeDirectoryPath);
            return;
        }

        await GoToDirectoryAsync(dir);
    }

    [RelayCommand]
    public async Task GoToDirectoryAsync(IStorageFolder folder)
    {
        Log.Debug("GoToDirectoryAsync {folder.Path}", folder.Path);

        _storageItems.Clear();

        var items = await _fileSystemService.EnumerateItemsAsync(folder);

        await foreach (var item in items)
        {
            _storageItems.Add(item);
        }
    }
}
