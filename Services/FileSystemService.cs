using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace explorer_async.Services;

public sealed class FileSystemService
{
    public async Task<List<FileInfo>> ListFilesAsync(string directoryPath)
    {
        try
        {
            return await Task.Run(() => Directory.EnumerateFiles(directoryPath).Select(p => new FileInfo(p)).ToList());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return [];
        }
    }

    public async Task<IEnumerable<string>> EnumerateFilesAsync(string directoryPath)
    {
        try
        {
            return await Task.Run(() => Directory.EnumerateFiles(directoryPath));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Enumerable.Empty<string>();
        }
    }
}
