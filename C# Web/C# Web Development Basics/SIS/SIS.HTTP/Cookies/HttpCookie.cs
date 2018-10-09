namespace SIS.HTTP.Cookies
{
    using Common;
    using System;

    public class HttpCookie
    {
        private const int DefaultExpirationDays = 3;

        public HttpCookie(
            string key, 
            string value, 
            int expiresInDays = DefaultExpirationDays)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));
            CoreValidator.ThrowIfNull(value, nameof(value));

            this.Key = key;
            this.Value = value;
            this.Expires = DateTime.UtcNow.AddDays(expiresInDays);
            this.IsNew = true;
            this.IsHttpOnly = true;
        }

        public HttpCookie(
            string key, 
            string value, 
            bool isNew, 
            int expiresInDays = DefaultExpirationDays)
            : this(key, value, expiresInDays)
        {
            this.IsNew = isNew;
        }

        public string Key { get; }

        public string Value { get; }

        public DateTime Expires { get; private set; }

        public bool IsNew { get; }

        public bool IsHttpOnly { get; set; }

        public override string ToString()
        {
            var httpOnly = this.IsHttpOnly ? "; HttpOnly" : string.Empty;

            return $"{this.Key}={this.Value}; Expires={this.Expires:R}{httpOnly}";
        }

        public void Delete() => this.Expires = DateTime.UtcNow.AddDays(-1);
    }
}
