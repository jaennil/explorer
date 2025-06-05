using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using Serilog;

namespace explorer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<IStorageItem> StorageItems { get; } = [];
    public IAsyncRelayCommand GoToDirectoryCommand { get; }

    private FileSystemService _fileSystemService;
    private IStorageFolder _currentFolder;

    public MainWindowViewModel(FileSystemService fileSystemService)
    {
        Log.Debug("MainWindowViewModel constructor");

        _fileSystemService = fileSystemService;

        string homeDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);



        loadStorageItemsAsync(homeDirectoryPath);
    }

    private async Task loadStorageItemsAsync(string directoryPath)
    {
        Log.Debug("loadStorageItemsAsync");

        var items = await _fileSystemService.EnumerateItemsAsync(directoryPath);

        await foreach (var item in items)
        {
            StorageItems.Add(item);
        }
    }

    private async Task goToDirectoryAsync()
    {
        Log.Debug("goToDirectoryAsync");


    }
}
