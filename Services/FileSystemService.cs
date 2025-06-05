using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

public class FileSystemService
{
    private IStorageProvider _storageProvider;

    public FileSystemService(IStorageProvider storageProvider)
    {
        _storageProvider = storageProvider;
    }

    public async Task<IAsyncEnumerable<IStorageItem>> EnumerateItemsAsync(string directoryPath)
    {
        var dir = await GetDirectoryFromPathAsync(directoryPath);
        if (dir == null)
        {
            return AsyncEnumerable.Empty<IStorageItem>();
        }
        return dir.GetItemsAsync();
    }

    public async Task<IStorageFolder?> GetDirectoryFromPathAsync(string directoryPath)
    {
        var dir = await _storageProvider.TryGetFolderFromPathAsync(directoryPath);
        return dir;
    }
}
