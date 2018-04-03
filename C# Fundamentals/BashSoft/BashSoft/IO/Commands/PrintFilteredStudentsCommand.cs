using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("filter")]
    public class PrintFilteredStudentsCommand : Command, IExecutable
    {
        [Inject]
        private readonly IDatabase _repository;

        public PrintFilteredStudentsCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        //Filters and takes students IF data consists 5 elements ->
        // "filter" command + courseName + filter + takeCommand + takeQuantity
        public override void Execute()
        {
            if (Data.Length != 5)
            {
                throw new InvalidCommandException(Input);
            }

            var courseName = Data[1];
            var filter = Data[2].ToLower();
            var takeCommand = Data[3].ToLower();
            var takeQuantity = Data[4].ToLower();

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