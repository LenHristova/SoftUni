using System;
using System.Collections.Generic;
using System.IO;

class StartUp
{
    private const int BufferSize = 4096;

    static void Main()
    {
        var sourceFile = "../sliceMe.mp4";
        var destination = "../";
        Slice(sourceFile, destination, 5);

        var files = new List<string>()
        {
            "Part-0.mp4",
            "Part-1.mp4",
            "Part-2.mp4",
            "Part-3.mp4",
            "Part-4.mp4"
        };
        Assemble(files, destination);

    }

    static void Slice(string sourceFile, string destinationDirectory, int parts)
    {
        using (var reader = new FileStream(sourceFile, FileMode.Open))
        {
            var extension = sourceFile.Substring(sourceFile.LastIndexOf('.'));

            destinationDirectory = destinationDirectory == string.Empty
                ? "./"
                : destinationDirectory;

            var partsSize = (long)Math.Ceiling((double)reader.Length / parts);

            for (int i = 0; i < parts; i++)
            {
                var currentPart = $"{destinationDirectory}Part-{i}{extension}";

                using (var writer = new FileStream(currentPart, FileMode.Create))
                {
                    var buffer = new byte[BufferSize];

                    var currentPartSize = 0L;
                    while (reader.Read(buffer, 0, BufferSize) == BufferSize)
                    {
                        writer.Write(buffer, 0, BufferSize);

                        currentPartSize += BufferSize;
                        if (currentPartSize >= partsSize)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

    static void Assemble(List<string> files, string destinationDirectory)
    {
        var extension = files[0].Substring(files[0].LastIndexOf('.'));

        destinationDirectory = destinationDirectory == string.Empty
            ? "./"
            : destinationDirectory;

        if (!destinationDirectory.EndsWith('/'))
        {
            destinationDirectory += '/';
        }

        var assembled = $"{destinationDirectory}Assembled{extension}";
        using (var writer = new FileStream(assembled, FileMode.Create))
        {
            var buffer = new byte[BufferSize];

            foreach (var file in files)
            {
                using (var reader = new FileStream(destinationDirectory + file, FileMode.Open))
                {
                    while (reader.Read(buffer, 0, BufferSize) == BufferSize)
                    {
                        writer.Write(buffer, 0, BufferSize);
                    }
                }
            }
        }
    }
}