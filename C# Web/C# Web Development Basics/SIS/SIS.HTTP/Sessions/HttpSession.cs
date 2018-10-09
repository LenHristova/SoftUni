namespace SIS.HTTP.Sessions
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Contracts;

    public class HttpSession : IHttpSession
    {
        private readonly IDictionary<string, object> parameters;

        public HttpSession(string id)
        {
            CoreValidator.ThrowIfNullOrEmpty(id, nameof(id));

            this.Id = id;
            this.parameters = new Dictionary<string, object>();
        }

        public string Id { get; }

        public bool ContainsParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            return this.parameters.ContainsKey(name);
        }

        public object GetParameter(string name)
            => this.ContainsParameter(name)
                ? this.parameters[name]
                : null;

        public void AddParameter(string name, object parameter)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            CoreValidator.ThrowIfNull(parameter, nameof(parameter));

            if (this.ContainsParameter(name))
            {
                throw new InvalidOperationException(
                    $"HttpSession already contains key \"{name}\"");
            }

            this.parameters.Add(name, parameter);
        }

        public void Clear() => this.parameters.Clear();
    }
}
