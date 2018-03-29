using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class PrintOrderedStudentsCommand : Command, IExecutable
    {
        public PrintOrderedStudentsCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager) 
            : base(input, data, judge, repository, inputOutputManager)
        {
        }

        //Orders and takes students IF Data consists 5 elements ->
        // "order" command + courseName + orderType + takeCommand + takeQuantity
        public override void Execute()
        {
            if (Data.Length != 5)
            {
                throw new InvalidTakeQuantityParameter();
            }

            var courseName = Data[1];
            var orderType = Data[2].ToLower();
            var takeCommand = Data[3].ToLower();
            var takeQuantity = Data[4].ToLower();

            TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, orderType);
        }

        //Parse parameters for order and take IF takeCommand is "take" and takeQuantity is number or "all"
        private void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity, string courseName, string orderType)
        {
            if (takeCommand != "take")
            {
                throw new InvalidTakeQuantityParameter();
            }

            if (takeQuantity == "all")
            {
                this.Repository.OrderAndTake(courseName, orderType);
            }
            else
            {
                var hasParsed = int.TryParse(takeQuantity, out var studentsToTake);
                if (!hasParsed)
                {
                    throw new InvalidTakeQuantityParameter();
                }

                Repository.OrderAndTake(courseName, orderType, studentsToTake);
            }
        }
    }
}