using System;
using System.Collections.Generic;
using System.IO;

using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.Static_data;

namespace BashSoft.IO
{
    public class IOManager : IDirectoryManager
    {
        //Traverses the current directory and the subdirectories to the required depth 
        //and writes their paths and the names of the files in each directory
        public void TraverseDirectory(int depth)
        {
            OutputWriter.WriteEmptyLine();
            var initialIdentation = SessionData.CurrentPath.Split('\\').Length;
            var subFolders = new Queue<string>();
            subFolders.Enqueue(SessionData.CurrentPath);

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
                    OutputWriter.DisplayException(ExceptionMessages.UNAUTHORIZED_ACCESS_EXCEPTION_MESSAGE);
                }
            }
        }

        public void CreateDirectoryInCurrentFolder(string name)
        {
            try
            {
                var path = SessionData.CurrentPath + "\\" + name;
                Directory.CreateDirectory(path);
            }
            catch (ArgumentException)
            {
                throw new InvalidFileNameException();
            }
        }

        public void ChangeCurrentDirectoryRelative(string realitivePath)
        {
            //Check if user wants to goes one folder back
            if (realitivePath == "..")
            {
                try
                {
                    var currentPath = SessionData.CurrentPath;
                    var indexOfLastSlash = currentPath.LastIndexOf("\\");
                    var newPath = currentPath.Substring(0, indexOfLastSlash);
                    SessionData.CurrentPath = newPath;
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new InvalidPathException();
                }
            }
            // or else down in the directory tree
            else
            {
                var currentPath = SessionData.CurrentPath;
                currentPath += "\\" + realitivePath;
                ChangeCurrentDirectoryAbsolute(currentPath);
            }
        }

        public void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                throw new InvalidPathException();
            }

            SessionData.CurrentPath = absolutePath;
        }
    }
}
