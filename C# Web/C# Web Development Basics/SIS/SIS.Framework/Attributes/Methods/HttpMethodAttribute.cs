namespace SIS.Framework.Attributes.Methods
{
    using System;
    using HTTP.Enums;

    public abstract class HttpMethodAttribute : Attribute
    {
        protected abstract HttpRequestMethod RequestMethod { get; }

        public bool IsValid(HttpRequestMethod requestMethod)
            => requestMethod == this.RequestMethod;
    }
}
