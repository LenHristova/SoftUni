namespace SIS.HTTP.Cookies.Contracts
{
    using System.Collections.Generic;

    public interface IHttpCookieCollection : IEnumerable<HttpCookie>
    {
        void Add(HttpCookie cookie);

        bool ContainsKey(string key);

        HttpCookie GetCookie(string key);

        bool HasCookies();
    }
}
