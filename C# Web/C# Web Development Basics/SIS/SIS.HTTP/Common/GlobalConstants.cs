namespace SIS.HTTP.Common
{
    public static class GlobalConstants
    {
        public const string HttpOneProtocolFragment = "HTTP/1.1";

        public class ExceptionMessages
        {
            public const string BadRequest = "The Request was malformed or contains unsupported elements.";
            public const string InternalServerError = "The Server has encountered an error.";
        }
    }
}
