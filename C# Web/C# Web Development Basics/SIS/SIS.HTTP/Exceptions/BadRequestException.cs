namespace SIS.HTTP.Exceptions
{
    using System;
    using Common;

    public class BadRequestException : Exception
    {
        public override string Message => GlobalConstants.ExceptionMessages.BadRequest;
    }
}
