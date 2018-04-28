using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("order")]
    public class PrintOrderedStudentsCommand : IExecutable
    {
        private readonly IDatabase _repository;

        public PrintOrderedStudentsCommand(IDatabase repository)
        {
            _repository = repository;
        }

        //Orders and takes students IF Data consists 5 elements ->
        // "order" command + courseName + orderType + takeCommand + takeQuantity
        public void Execute(params string[] args)
        {
            var input = args[0];
            var data = input.Split();
            if (data.Length != 5)
            {
                throw new InvalidTakeQuantityParameter();
            }

            var courseName = data[1];
            var orderType = data[2].ToLower();
            var takeCommand = data[3].ToLower();
            var takeQuantity = data[4].ToLower();

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
                _repository.OrderAndTake(courseName, orderType);
            }
            else
            {
                var hasParsed = int.TryParse(takeQuantity, out var studentsToTake);
                if (!hasParsed)
                {
                    throw new InvalidTakeQuantityParameter();
                }

                _repository.OrderAndTake(courseName, orderType, studentsToTake);
            }
        }
    }
}