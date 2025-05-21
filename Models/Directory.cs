using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using System.Linq;
using System;
using ReactiveUI;

namespace explorer.Models;

public class Directory : ReactiveObject
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
        var imageFiles = Info.EnumerateFiles()
            .Where(f => SupportedImageExtensions.Contains(f.Extension.ToLowerInvariant()))
            .Take(4);

        foreach (var file in imageFiles)
        {
            var bitmap = new Bitmap(file.FullName);
            Images.Add(bitmap);
        }
    }
}
