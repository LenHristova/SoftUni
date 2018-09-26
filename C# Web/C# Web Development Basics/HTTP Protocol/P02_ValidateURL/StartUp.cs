namespace P02_ValidateURL
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    public class StartUp
    {
        private const string UrlPattern = @"^(?<protocol>{0}):\/\/(?<host>[A-Za-z0-9-]+\.[A-Za-z0-9]+)(?::(?<port>{1}))?(?<path>\/([A-Za-z0-9\/.]+)?)?(?:\?(?<query>([A-Za-z0-9]+=[A-Za-z0-9]+)(&[A-Za-z0-9]+=[A-Za-z0-9]+)*))?(?:#(?<fragment>[A-Za-z0-9]+))?$";

        private static readonly IDictionary<string, int> ProtocolsPorts = new Dictionary<string, int>
        {
            { "http", 80},
            { "https", 443},
        };

        public static void Main()
        {
            while (true)
            {
                Console.Write("Enter URL: ");
                var inputUrl = Console.ReadLine();
                var decodedUrl = WebUtility.UrlDecode(inputUrl);

                var result = GetResult(decodedUrl);

                Console.WriteLine(result);
                Console.WriteLine();
            }
        }

        private static string GetResult(string decodedUrl)
        {
            foreach(var protocolPort in ProtocolsPorts)
            {
                var isValidUrl = TryValidateUrl(decodedUrl, protocolPort.Key, protocolPort.Value, out var parts);

                if (isValidUrl)
                {
                    return parts;
                }
            }

            return "Invalid URL";
        }

        private static bool TryValidateUrl(string decodedUrl, string protocol, int port, out string parts)
        {
            var pattern = string.Format(UrlPattern, protocol, port);
            var regex = new Regex(pattern);
            var match = regex.Match(decodedUrl);

            if (!match.Success)
            {
                parts = null;
                return false;
            }

            var groups = match.Groups;
            parts = GetUrlParts(groups, port);
            return true;
        }

        private static string GetUrlParts(GroupCollection groups, int port)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Protocol: {groups["protocol"]}")
                .AppendLine($"Host: {groups["host"]}")
                .AppendLine($"Port: {port}")
                .AppendLine($"Path: {groups["path"]}");

            var query = groups["query"].ToString();
            if (!string.IsNullOrEmpty(query))
            {
                sb.AppendLine($"Query: {query}");
            }

            var fragment = groups["fragment"].ToString();
            if (!string.IsNullOrEmpty(query))
            {
                sb.AppendLine($"Fragment: {fragment}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
