using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class StartUp
{
    static void Main()
    {
        Console.Write("Directory: ");
        var path = Console.ReadLine();

        var files = Directory.GetFiles(path);

        var filesByExtensions = GetFilesByExtensions(files);

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

    private static Dictionary<string, List<FileInfo>> GetFilesByExtensions(string[] files)
    {
        var filesByExtensions = new Dictionary<string, List<FileInfo>>();
        foreach (var file in files)
        {
            var fileInfo = new FileInfo(file);
            var extension = fileInfo.Extension;

            if (!filesByExtensions.ContainsKey(extension))
            {
                filesByExtensions.Add(extension, new List<FileInfo>());
            }

            filesByExtensions[extension].Add(fileInfo);
        }

        return filesByExtensions;
    }
}