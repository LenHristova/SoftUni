﻿namespace SIS.WebServer.Results
{
    using HTTP.Common;
    using HTTP.Enums;
    using HTTP.Headers;
    using HTTP.Responses;

    public class InlineResourceResult : HttpResponse
    {
        public InlineResourceResult(byte[] content, HttpResponseStatusCode statusCode)
        : base(statusCode)
        {
            this.Headers.Add(new HttpHeader(GlobalConstants.HeaderNames.ContentLength, content.Length.ToString()));
            this.Headers.Add(new HttpHeader(GlobalConstants.HeaderNames.ContentDisposition, "inline"));

            this.Content = content;
        }
    }
}
