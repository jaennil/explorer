using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using System;

namespace explorer.Models;

public class Directory
{
    public System.IO.DirectoryInfo Info { get; }
    public ObservableCollection<Bitmap> Images { get; } = [];

    // TODO: add other extensions
    public static string[] SupportedImageExtensions { get; } = [".jpg", ".png", ".jpeg"];

    public Directory(string path)
    {
        Info = new System.IO.DirectoryInfo(path);
        LoadImages();
    }

    private void LoadImages()
    {
        var count = 0;
        var files = System.IO.Directory.EnumerateFiles(Info.FullName);
        foreach (var filePath in files)
        {
            var extension = System.IO.Path.GetExtension(filePath);

            if (Array.Exists(SupportedImageExtensions, ext => ext == extension))
            {
                count++;
                var bitmap = new Bitmap(filePath);
                Images.Add(bitmap);
            }

            if (count == 4)
            {
                return;
            }
        }
    }
}
