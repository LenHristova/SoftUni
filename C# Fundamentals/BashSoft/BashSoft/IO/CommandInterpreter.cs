using System;
using System.Linq;
using System.Reflection;

using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.IO.Commands;

namespace BashSoft.IO
{
    public class CommandInterpreter : IInterpreter
    {
        private readonly IContentComparer _judge;
        private readonly IDatabase _repository;
        private readonly IDirectoryManager _inputOutputManager;

        public CommandInterpreter(IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
        {
            this._judge = judge;
            this._repository = repository;
            this._inputOutputManager = inputOutputManager;
        }

        public void InterpretCommand(string input)
        {
            var data = input.Split();
            var commandName = data[0];
            try
            {
                var command = ParseCommand(input, data, commandName);
                command.Execute();
            }
            catch (Exception ex)
            {
                OutputWriter.DisplayException(ex.Message);
            }
        }

        //Directs command IF is valid
        private IExecutable ParseCommand(string input, string[] data, string command)
        {
            var parametersForConstruction = new object[]
            {
                input,
                data
            };

            var commandType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(type => type.GetCustomAttributes(typeof(AliasAttribute))
                                         .Where(attribute => attribute.Equals(command.ToLower()))
                                            .ToArray().Length > 0);

            var commandInstance = (Command)Activator.CreateInstance(commandType, parametersForConstruction);


            var commandFields = commandType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            var interpreterFields = typeof(CommandInterpreter).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var commandField in commandFields)
            {
                var attribute = commandField.GetCustomAttribute(typeof(InjectAttribute));
                if (attribute != null)
                {
                    var interpreterFieldEquivalentToCommandField = interpreterFields.FirstOrDefault(f => f.FieldType == commandField.FieldType);
                    if (interpreterFieldEquivalentToCommandField != null)
                    {
                        commandField.SetValue(commandInstance, interpreterFieldEquivalentToCommandField.GetValue(this));
                    }
                }
            }

            return commandInstance;












            //switch (command.ToLower())
            //{
            //    case "open":
            //        return new OpenFileCommand(input, data, _judge, _repository, _inputOutputManager);
            //    case "mkdir":
            //        return new MakeDirectoryCommand(input, data, _judge, _repository, _inputOutputManager);
            //    case "ls":
            //        return new TraverseFoldersCommand(input, data, _judge, _repository, _inputOutputManager);
            //    case "cmd":
            //        return new CompareFilesCommand(input, data, _judge, _repository, _inputOutputManager);
            //    case "cdrel":
            //        return new ChangeRelativePathCommand(input, data, _judge, _repository, _inputOutputManager);
            //    case "cdabs":
            //        return new ChangeAbsolutePathCommand(input, data, _judge, _repository, _inputOutputManager);
            //    case "readdb":
            //        return new ReadDatabaseCommand(input, data, _judge, _repository, _inputOutputManager);
            //    case "help":
            //        return new GetHelpCommand(input, data, _judge, _repository, _inputOutputManager);
            //    case "show":
            //        return new ShowCourseCommand(input, data, _judge, _repository, _inputOutputManager);
            //    case "filter":
            //        return new PrintFilteredStudentsCommand(input, data, _judge, _repository, _inputOutputManager);
            //    case "order":
            //        return new PrintOrderedStudentsCommand(input, data, _judge, _repository, _inputOutputManager);
            //    case "display":
            //        return new DisplayCommand(input, data, _judge, _repository, _inputOutputManager);
            //    case "decorder":
            //        throw new NotImplementedException();
            //    case "download":
            //        throw new NotImplementedException();
            //    case "downloadasynch":
            //        throw new NotImplementedException();
            //    case "dropdb":
            //        return new DropDatabaseCommand(input, data, _judge, _repository, _inputOutputManager);
            //    default:
            //        throw new InvalidCommandException(input);
            // }
        }
    }
}