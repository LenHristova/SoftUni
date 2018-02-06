using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class StartUp
{
    private static readonly Dictionary<string, List<FileInfo>> FilesByExtensions = new Dictionary<string, List<FileInfo>>();

    static void Main()
    {
        Console.Write("Directory: ");
        var path = Console.ReadLine();

        GetFilesByExtensions(path);

        CreateReport(FilesByExtensions);
    }

    private static void GetFilesByExtensions(string path)
    {
        var files = Directory.GetFiles(path);
        foreach (var file in files)
        {
            var fileInfo = new FileInfo(file);
            var extension = fileInfo.Extension;

            if (!FilesByExtensions.ContainsKey(extension))
            {
                FilesByExtensions.Add(extension, new List<FileInfo>());
            }

            FilesByExtensions[extension].Add(fileInfo);
        }

        var directories = Directory.GetDirectories(path);

        foreach (var dir in directories)
        {
            GetFilesByExtensions(dir);
        }
    }

    private static void CreateReport(Dictionary<string, List<FileInfo>> filesByExtensions)
    {
        string desctopPath = Environment.GetFolderPath(
            Environment.SpecialFolder.DesktopDirectory);
        using (var writer = new StreamWriter(desctopPath + "/report.txt"))
        {
            foreach (var filesByExtension in filesByExtensions
                .OrderByDescending(kvp => kvp.Value.Count)
                .ThenBy(kvp => kvp.Key))
            {
                writer.WriteLine(filesByExtension.Key);

                foreach (var fileInfo in filesByExtension.Value.OrderBy(f => f.Length))
                {
                    var size = (double)fileInfo.Length / 1024;
                    writer.WriteLine($"--{fileInfo.Name} - {size:F3}kb");
                }
            }
        }
    }
}