using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using Serilog;

public class FileSystemService
{
    private IStorageProvider _storageProvider;

    public FileSystemService(IStorageProvider storageProvider)
    {
        _storageProvider = storageProvider;
    }

    public async Task<IAsyncEnumerable<IStorageItem>> EnumerateItemsAsync(string directoryPath)
    {
        Log.Debug("EnumerateItemsAsync Path {directoryPath}", directoryPath);

        var dir = await GetDirectoryFromPathAsync(directoryPath);
        if (dir == null)
        {
            return AsyncEnumerable.Empty<IStorageItem>();
        }
        return dir.GetItemsAsync();
    }

    public async Task<IAsyncEnumerable<IStorageItem>> EnumerateItemsAsync(IStorageFolder folder)
    {
        Log.Debug("EnumerateItemsAsync Folder {folder.Path}", folder.Path);

        return folder.GetItemsAsync();
    }

    public async Task<IStorageFolder?> GetDirectoryFromPathAsync(string directoryPath)
    {
        Log.Debug("GetDirectoryFromPathAsync {directoryPath}", directoryPath);

        var dir = await _storageProvider.TryGetFolderFromPathAsync(directoryPath);
        return dir;
    }
}
