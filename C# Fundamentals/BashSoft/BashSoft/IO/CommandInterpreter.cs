using System.Diagnostics;
using System.IO;
using BashSoft.Judge;
using BashSoft.Repository;
using BashSoft.Static_data;

namespace BashSoft.IO
{
    public class CommandInterpreter
    {
        private readonly Tester _judge;
        private readonly StudentsRepository _repository;
        private readonly IOManager _inputOutputManager;

        public CommandInterpreter(Tester judge, StudentsRepository repository, IOManager inputOutputManager)
        {
            _judge = judge;
            _repository = repository;
            _inputOutputManager = inputOutputManager;
        }

        public void InterpretCommand(string input)
        {
            var data = input.Split();
            var command = data[0];
            switch (command)
            {
                case "open":
                    TryOpenFile(input, data);
                    break;
                case "mkdir":
                    TryCreateDirectory(input, data);
                    break;
                case "ls":
                    TryTraverseFolders(input, data);
                    break;
                case "cmd":
                    TryCompareFiles(input, data);
                    break;
                case "cdRel":
                    TryChangePathRelatively(input, data);
                    break;
                case "cdAbs":
                    TryChangePathAbsolute(input, data);
                    break;
                case "readDb":
                    TryReadDatabaseFromFile(input, data);
                    break;
                case "help":
                    TryGetHelp(input, data);
                    break;
                case "show":
                    TryShowWantedData(input, data);
                    break;
                case "filter":
                    TryFilterAndTake(input, data);
                    break;
                case "order":
                    TryOrderAndTake(input, data);
                    break;
                case "decOrder":
                    //TODO
                    break;
                case "download":
                    //TODO
                    break;
                case "downloadAsynch":
                    //TODO
                    break;
                case "drobDb":
                    TryDropDb(input, data);
                    break;
                default:
                    DisplayInvalidCommandMessage(input);
                    break;
            }
        }

        private void TryDropDb(string input, string[] data)
        {
            if (data.Length!=1)
            {
                DisplayInvalidCommandMessage(input);
                return;
            }
            
            _repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database dropped!");
        }

        //Orders and takes students IF data consists 5 elements ->
        // courseName + orderType + takeCommand + takeQuantity
        private void TryOrderAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                var courseName = data[1];
                var orderType = data[2].ToLower();
                var takeCommand = data[3].ToLower();
                var takeQuantity = data[4].ToLower();

                TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, orderType);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        //Parse parameters for order and take IF takeCommand is "take" and takeQuantity is number or "all"
        private void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity, string courseName, string orderType)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    _repository.OrderAndTake(courseName, orderType);
                }
                else
                {
                    var hasParsed = int.TryParse(takeQuantity, out var studentsToTake);

                    if (hasParsed)
                    {
                        _repository.OrderAndTake(courseName, orderType, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
            }
        }

        //Filters and takes students IF data consists 5 elements ->
        // courseName + filter + takeCommand + takeQuantity
        private void TryFilterAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                var courseName = data[1];
                var filter = data[2].ToLower();
                var takeCommand = data[3].ToLower();
                var takeQuantity = data[4].ToLower();

                TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        //Parse parameters for filter and take IF takeCommand is "take" and takeQuantity is number or "all"
        private void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    _repository.FilterAndTake(courseName, filter);
                }
                else
                {
                    var hasParsed = int.TryParse(takeQuantity, out var studentsToTake);
                    if (hasParsed)
                    {
                        _repository.FilterAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
            }
        }

        //Shows info IF data consists 2 or 3 elements 
        // 2 elements -> command + course's name 
        // 3 elements -> command + course's name + student's username
        private void TryShowWantedData(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var courseName = data[1];
                _repository.GetAllStudentsFromCourse(courseName);
            }
            else if (data.Length == 3)
            {
                var courseName = data[1];
                var studentUsername = data[2];
                _repository.GetStudentScoresFromCourse(courseName, studentUsername);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        //Shows help IF data consists 1 elements -> command
        private void TryGetHelp(string input, string[] data)
        {
            if (data.Length == 1)
            {
                //Displays information about all of the commands
                OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|",
                    "make directory - mkdir: path "));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|",
                    "traverse directory - ls: depth "));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|",
                    "comparing files - cmp: path1 path2"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|",
                    "change directory - cdREl: relative path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|",
                    "change directory - cdAbs: absolute path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|",
                    "read students data base - readDb: path"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|",
                    "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|",
                    "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|",
                    "download file - download: path of file (saved in current directory)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|",
                    "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
                OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|",
                    "get help – help"));
                OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
                OutputWriter.WriteEmptyLine();
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        //Reads database from file IF data consists 2 elements -> command + file's name
        private void TryReadDatabaseFromFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var fileName = data[1];
                _repository.LoadData(fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        //Changes path relatively IF data consists 2 elements -> command + absolute path
        private void TryChangePathAbsolute(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var absPath = data[1];
                _inputOutputManager.ChangeCurrentDirectoryAbsolute(absPath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        //Changes path relatively IF data consists 2 elements -> command + relative path
        private void TryChangePathRelatively(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var relPath = data[1];
                _inputOutputManager.ChangeCurrentDirectoryRelative(relPath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        //Compares files IF data consists 3 elements ->
        //command + absolute path of the first file + absolute path of the second file 
        private void TryCompareFiles(string input, string[] data)
        {
            if (data.Length == 3)
            {
                var firstPath = data[1];
                var secondPath = data[2];

                _judge.CompareContent(firstPath, secondPath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        //Traverses directory. Valid input (data) consists 1 or 2 elements
        //1 element -> command (default depth is 0)
        //2 element -> command + depth
        private void TryTraverseFolders(string input, string[] data)
        {
            if (data.Length == 1)
            {
                var depth = 0;
                _inputOutputManager.TraverseDirectory(depth);
            }
            else if (data.Length == 2)
            {
                //check if second element is a number
                var hasParsed = int.TryParse(data[1], out var depth);
                if (hasParsed)
                {
                    _inputOutputManager.TraverseDirectory(depth);
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                }
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        //Creates directory IF data consist 2 elements -> command + directory's name
        private void TryCreateDirectory(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var folderName = data[1];
                _inputOutputManager.CreateDirectoryInCurrentFolder(folderName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        //Opens file IF data consist 2 elements -> command + file's name
        private void TryOpenFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var fileName = data[1];
                var filePath = $"{SessionData.currentPath}\\{fileName}";

                //TODO File can not be opened?
                if (Directory.Exists(filePath) || File.Exists(filePath))
                {
                    Process.Start(filePath);
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                }
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void DisplayInvalidCommandMessage(string input)
        {
            OutputWriter.WriteMessageOnNewLine($"The command '{input}' is invalid");
        }
    }
}
