namespace SIS.Framework.Attributes.Methods
{
    using HTTP.Enums;

    public class HttpGetAttribute : HttpMethodAttribute
    {
        protected override HttpRequestMethod RequestMethod => HttpRequestMethod.Get;
    }
}
