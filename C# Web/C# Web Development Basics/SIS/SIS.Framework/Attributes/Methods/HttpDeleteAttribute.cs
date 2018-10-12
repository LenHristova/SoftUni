namespace SIS.Framework.Attributes.Methods
{
    using HTTP.Enums;

    public class HttpDeleteAttribute : HttpMethodAttribute
    {
        protected override HttpRequestMethod RequestMethod => HttpRequestMethod.Delete;
    }
}
