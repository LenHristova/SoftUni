using BashSoft.Exceptions;
using BashSoft.Judge;
using BashSoft.Repository;

namespace BashSoft.IO.Commands
{
    public class PrintFilteredStudentsCommand : Command
    {
        public PrintFilteredStudentsCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
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
                throw new InvalidTakeQuantityPerameter();
            }

            if (takeQuantity == "all")
            {
                Repository.FilterAndTake(courseName, filter);
            }
            else
            {
                var hasParsed = int.TryParse(takeQuantity, out var studentsToTake);
                if (!hasParsed)
                {
                    throw new InvalidTakeQuantityPerameter();
                }

                Repository.FilterAndTake(courseName, filter, studentsToTake);
            }
        }
    }
}