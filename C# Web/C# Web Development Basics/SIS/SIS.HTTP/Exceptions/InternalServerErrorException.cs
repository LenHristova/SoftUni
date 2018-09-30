namespace SIS.HTTP.Exceptions
{
    using System;
    using Common;

    public class InternalServerErrorException : Exception
    {
        public override string Message => GlobalConstants.ExceptionMessages.InternalServerError;
    }
}
