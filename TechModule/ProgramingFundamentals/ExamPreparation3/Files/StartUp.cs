using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Files
{
    class StartUp
    {
        class File
        {
            public string Name { get; set; }
            public string Extension { get; set; }
            public long Size { get; set; }

            public override string ToString()
            {
                return $"{Name} - {Size} KB ";
            }
        }
        static void Main()
        {
            Dictionary<string, List<File>> rootsFiles = new Dictionary<string, List<File>>();

            int filesCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < filesCount; i++)
            {
                string[] fileInfo = Regex.Split(Console.ReadLine(), @"\\");

                string root = fileInfo.First();

                string[] fileParams = Regex.Split(fileInfo.Last(), @"\;");
                string name = fileParams[0];
                string extension = Regex.Split(name, @"\.").Last();
                long size = long.Parse(fileParams[1]);

                if (!rootsFiles.ContainsKey(root))
                {
                    rootsFiles[root] = new List<File>();
                }

                if (rootsFiles[root].Any(f => f.Name == name))
                {
                    File old = rootsFiles[root].Find(f => f.Name == name);
                    rootsFiles[root].Remove(old);
                }

                rootsFiles[root].Add(new File
                {
                    Name = name, Extension = extension, Size = size
                });
            }

            string[] searchedFileArgs = Regex.Split(Console.ReadLine(), @" in ");

            string serchedExtension = searchedFileArgs[0];
            string searchedRoot = searchedFileArgs[1];

            if (!rootsFiles.ContainsKey(searchedRoot))
            {
                Console.WriteLine("No");
            }
            else if (rootsFiles[searchedRoot].All(f => f.Extension != serchedExtension))
            {
                Console.WriteLine("No");
            }
            else
            {
                rootsFiles[searchedRoot]
                    .Where(f => f.Extension == serchedExtension)
                    .OrderByDescending(f => f.Size)
                    .ThenBy(f => f.Name)
                    .ToList()
                    .ForEach(Console.WriteLine);
            }
        }
    }
}
