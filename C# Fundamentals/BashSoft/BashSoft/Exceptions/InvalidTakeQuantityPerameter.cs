using System;

namespace BashSoft.Exceptions
{
    public class InvalidTakeQuantityPerameter:Exception
    {
        private const string INVALID_TAKE_QUANTITY_PARAMETER = "The take command expected does not match the format wanted!";

        public InvalidTakeQuantityPerameter() : base(INVALID_TAKE_QUANTITY_PARAMETER) { }

        public InvalidTakeQuantityPerameter(string message) : base(message) { }
    }
}
