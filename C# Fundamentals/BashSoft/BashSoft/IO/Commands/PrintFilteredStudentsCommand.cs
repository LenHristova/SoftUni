using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("filter")]
    public class PrintFilteredStudentsCommand : IExecutable
    {
        private readonly IDatabase _repository;

        public PrintFilteredStudentsCommand(IDatabase repository)
        {
            _repository = repository;
        }

        //Filters and takes students IF data consists 5 elements ->
        // "filter" command + courseName + filter + takeCommand + takeQuantity
        public void Execute(params string[] args)
        {
            var input = args[0];
            var data = input.Split();
            if (data.Length != 5)
            {
                throw new InvalidCommandException(input);
            }

            var courseName = data[1];
            var filter = data[2].ToLower();
            var takeCommand = data[3].ToLower();
            var takeQuantity = data[4].ToLower();

            TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
        }

        //Parse parameters for filter and take IF takeCommand is "take" and takeQuantity is number or "all"
        private void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        {
            if (takeCommand != "take")
            {
                throw new InvalidTakeQuantityParameter();
            }

            if (takeQuantity == "all")
            {
                _repository.FilterAndTake(courseName, filter);
            }
            else
            {
                var hasParsed = int.TryParse(takeQuantity, out var studentsToTake);
                if (!hasParsed)
                {
                    throw new InvalidTakeQuantityParameter();
                }

                _repository.FilterAndTake(courseName, filter, studentsToTake);
            }
        }
    }
}