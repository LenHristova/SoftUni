namespace SIS.HTTP.Common
{
    public static class GlobalConstants
    {
        public const string HttpOneProtocolFragment = "HTTP/1.1";
        public const string AppResourceFolderName = "Resources";

        public class HeaderNames
        {
            public const string ContentType = "Content-Type";
            public const string ContentLength = "Content-Length";
            public const string ContentDisposition  = "Content-Disposition ";
            public const string Cookie = "Cookie";
            public const string Host = "Host";
            public const string Location = "Location";
            public const string SetCookie = "Set-Cookie";
        }

        public class ExceptionMessages
        {
            public const string BadRequest = "The Request was malformed or contains unsupported elements.";
            public const string InternalServerError = "The Server has encountered an error.";
        }
    }
}
