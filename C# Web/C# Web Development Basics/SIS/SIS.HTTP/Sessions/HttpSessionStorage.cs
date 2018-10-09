namespace SIS.HTTP.Sessions
{
    using System.Collections.Concurrent;
    using Contracts;

    public class HttpSessionStorage
    {
        public const string SessionCookieKey = "SIS_ID";
        public const string UsernameKey = "^%Username_Key%^";

        private static readonly ConcurrentDictionary<string, IHttpSession> sessions =
            new ConcurrentDictionary<string, IHttpSession>();

        public static IHttpSession GetSession(string id)
            => sessions.GetOrAdd(id, _ => new HttpSession(id));
    }
}
