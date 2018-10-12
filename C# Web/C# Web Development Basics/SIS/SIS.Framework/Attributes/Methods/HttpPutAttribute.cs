namespace SIS.Framework.Attributes.Methods
{
    using HTTP.Enums;

    public class HttpPutAttribute : HttpMethodAttribute
    {
        protected override HttpRequestMethod RequestMethod => HttpRequestMethod.Put;
    }
}
