namespace SIS.Framework.ActionResults.Contracts
{
    using HTTP.Responses.Contracts;

    public interface IActionResult
    {
        IHttpResponse Invoke();
    }
}
