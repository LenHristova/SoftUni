using System;

namespace BashSoft.Exceptions
{
    public class InvalidTakeQuantityParameter : Exception
    {
        private const string INVALID_TAKE_QUANTITY_PARAMETER = "The take command expected does not match the format wanted!";

        public InvalidTakeQuantityParameter() : base(INVALID_TAKE_QUANTITY_PARAMETER) { }

        public InvalidTakeQuantityParameter(string message) : base(message) { }
    }
}
