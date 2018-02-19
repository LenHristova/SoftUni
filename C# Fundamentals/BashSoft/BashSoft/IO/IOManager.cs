using System;
using System.Collections.Generic;
using System.IO;
using BashSoft.Static_data;

namespace BashSoft.IO
{
    public class IOManager
    {
        //Traverses the current directory and the subdirectories to the required depth 
        //and writes their paths and the names of the files in each directory
        public void TraverseDirectory(int depth)
        {
            OutputWriter.WriteEmptyLine();
            var initialIdentation = SessionData.currentPath.Split('\\').Length;
            var subFolders = new Queue<string>();
            subFolders.Enqueue(SessionData.currentPath);

            //Traversal stops when have no subfolders or required depth is reached
            while (subFolders.Count != 0)
            {
                var currentPath = subFolders.Dequeue();
                var identation = currentPath.Split('\\').Length - initialIdentation;

                if (depth - identation < 0) // <- check if required depth is reached
                {
                    break;
                }

                OutputWriter.WriteMessageOnNewLine($"{new string('-', identation)}{currentPath}");

                try
                {
                    //Gets current directory files and prints them
                    foreach (var file in Directory.GetFiles(currentPath))
                    {
                        var indexOfLastSlash = file.LastIndexOf("\\");
                        var fileName = file.Substring(indexOfLastSlash);
                        OutputWriter.WriteMessageOnNewLine(new string('-', indexOfLastSlash) + fileName);
                    }

                    //Gets current directory subdirectories and add them to the queue for later traversal
                    foreach (var directoryPath in Directory.GetDirectories(currentPath))
                    {
                        subFolders.Enqueue(directoryPath);

                    }
                }
                catch (UnauthorizedAccessException)
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnauthorizedAccessExceptionMessage);
                }
            }
        }

        public void CreateDirectoryInCurrentFolder(string name)
        {
            try
            {
                var path = SessionData.currentPath + "\\" + name;
                Directory.CreateDirectory(path);
            }
            catch (ArgumentException)
            {
                OutputWriter.DisplayException(ExceptionMessages.ForbiddenSymbolsContainedInName);
            }
        }

        public void ChangeCurrentDirectoryRelative(string realitivePath)
        {
            //Check if user wants to goes one folder back
            if (realitivePath == "..")
            {
                try
                {
                    var currentPath = SessionData.currentPath;
                    var indexOfLastSlash = currentPath.LastIndexOf("\\");
                    var newPath = currentPath.Substring(0, indexOfLastSlash);
                    SessionData.currentPath = newPath;
                }
                catch (ArgumentOutOfRangeException)
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToGoHigherInPartitionHierarchy);
                }
            }
            // or else down in the directory tree
            else
            {
                var currentPath = SessionData.currentPath;
                currentPath += "\\" + realitivePath;
                ChangeCurrentDirectoryAbsolute(currentPath);
            }
        }

        public void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                return;
            }

            SessionData.currentPath = absolutePath;
        }
    }
}
