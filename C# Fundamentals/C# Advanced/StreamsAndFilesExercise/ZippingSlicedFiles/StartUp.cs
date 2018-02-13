using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

class StartUp
{
    private const int BufferSize = 4096;

    static void Main()
    {
        var sourceFile = "../sliceMe.mp4";
        var destination = "../";
        Zip(sourceFile, destination, 5);

        var files = new List<string>()
        {
            "Part-0.gz",
            "Part-1.gz",
            "Part-2.gz",
            "Part-3.gz",
            "Part-4.gz"
        };

        Unzip(files, destination);
    }

    static void Zip(string sourceFile, string destinationDirectory, int parts)
    {
        using (var reader = new FileStream(sourceFile, FileMode.Open))
        {
           destinationDirectory = destinationDirectory == string.Empty
                ? "./"
                : destinationDirectory;

            var partsSize = (long) Math.Ceiling((double) reader.Length / parts);

            for (int i = 0; i < parts; i++)
            {
                var currentPart = $"{destinationDirectory}Part-{i}.gz";

                using (var writer =
                    new GZipStream(new FileStream(currentPart, FileMode.Create), CompressionLevel.Optimal))
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

    static void Unzip(List<string> files, string destinationDirectory)
    {
        destinationDirectory = destinationDirectory == string.Empty
            ? "./"
            : destinationDirectory;

        if (!destinationDirectory.EndsWith('/'))
        {
            destinationDirectory += '/';
        }

        var assembled = $"{destinationDirectory}Decompressed Assembled.mp4";
        using (var writer = new FileStream(assembled, FileMode.Create))
        {
            var buffer = new byte[BufferSize];

            foreach (var file in files)
            {
                using (var reader = new GZipStream(new FileStream(destinationDirectory + file, FileMode.Open), CompressionMode.Decompress))
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