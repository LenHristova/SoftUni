namespace SIS.HTTP.Extensions
{
    using Enums;
    using System.ComponentModel;

    public static class HttpResponseStatusExtensions
    {
        /// <summary>
        /// Returns string representation of this HttpResponseStatusCode in format,
        /// appropriate for Status Line of the HTTP Response
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        public static string GetResponseLine(this HttpResponseStatusCode httpResponse)
            => $"{(int) httpResponse}{httpResponse.GetDisplayName()}";
    }
}
