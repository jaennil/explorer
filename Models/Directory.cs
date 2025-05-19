using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using System;

namespace explorer.Models;

public class Directory
{
    public System.IO.DirectoryInfo Info { get; }
    private ObservableCollection<Bitmap> _images = [];
    public ObservableCollection<Bitmap> Images => _images;
    // TODO: add .webp
    private string[] supportedImageExtensions = [".jpg", ".png", ".jpeg"];

    public Directory(string path)
    {
        Info = new System.IO.DirectoryInfo(path);
        loadImages();
    }

    private void loadImages()
    {
        var count = 0;
        var files = System.IO.Directory.EnumerateFiles(Info.FullName);
        foreach (var filePath in files)
        {
            var extension = System.IO.Path.GetExtension(filePath);

            if (Array.Exists(supportedImageExtensions, ext => ext == extension))
            {
                count++;
                var bitmap = new Bitmap(filePath);
                _images.Add(bitmap);
            }

            if (count == 4)
            {
                return;
            }
        }
    }
}
