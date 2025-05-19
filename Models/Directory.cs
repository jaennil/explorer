using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;

namespace exlorer.Models;

public class Directory
{
    private System.IO.DirectoryInfo _info;
    private Bitmap _source;
    private ObservableCollection<Bitmap> _images;
    public ObservableCollection<Bitmap> Images => _images;
    public string Name => _info.Name;
    private string[] supportedImageExtensions = ["jpg", "webp"];

    public Directory(string path)
    {
        _info = new System.IO.DirectoryInfo(path);
    }

    private void loadImages()
    {
        var count = 0;
        var files = System.IO.Directory.EnumerateFiles(_info.FullName);
        foreach (var filePath in files)
        {
            var uri = new Uri(filePath);
            var extension = uri.Extension;
            if (supportedImageExtensions.Contains(extension))
            {
                count++;
                _images.Add(new Bitmap(filepath));
            }
            if (count == 4)
            {
                return;
            }
        }
    }
}
