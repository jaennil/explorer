using System;
using System.Collections.ObjectModel;
using Avalonia.Platform.Storage;
using Serilog;

namespace explorer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<IStorageItem> StorageItems { get; } = [];

    public MainWindowViewModel(IStorageProvider storageProvider)
    {
        _storageProvider = storageProvider;

        _currentPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        loadStorageItemsAsync();
    }

    private async void loadStorageItemsAsync()
    {
        var dir = await _storageProvider.TryGetFolderFromPathAsync(_currentPath);
        Log.Debug(dir.Path.ToString());
        var items = dir.GetItemsAsync();
        await foreach (var item in items)
        {
            StorageItems.Add(item);
            Log.Debug(item.Name);
        }
    }

    private IStorageProvider _storageProvider;
    private string _currentPath;
}
