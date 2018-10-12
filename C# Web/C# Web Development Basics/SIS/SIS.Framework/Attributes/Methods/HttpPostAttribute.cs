namespace SIS.Framework.Attributes.Methods
{
    using HTTP.Enums;

    public class HttpPostAttribute : HttpMethodAttribute
    {
        protected override HttpRequestMethod RequestMethod => HttpRequestMethod.Post;
    }
}
