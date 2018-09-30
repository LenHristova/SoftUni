namespace SIS.HTTP.Headers.Contracts
{
    public interface IHttpHeaderCollection
    {
        void Add(HttpHeader header);

        bool Contains(string key);

        HttpHeader GetHeader(string key);
    }
}
