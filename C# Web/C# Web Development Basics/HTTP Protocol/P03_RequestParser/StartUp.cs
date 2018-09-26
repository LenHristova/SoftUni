namespace P03_RequestParser
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        private static Dictionary<string, HashSet<string>> routesMethods;

        public static void Main()
        {
            routesMethods = RegisterRoutes();

            var httpRequest = Console.ReadLine();

            var httpResponse = GetResponse(httpRequest);

            Console.WriteLine(httpResponse);
        }

        private static HttpResponse GetResponse(string httpRequest)
        {
            var requestParts = httpRequest
                .ToLower()
                .Split(new[] { "/", " " }, StringSplitOptions.RemoveEmptyEntries);

            if (requestParts.Length < 3)
            {
                return new HttpResponse(StatusCode.BadRequest);
            }

            var route = requestParts[1];
            var method = requestParts[0];

            var isFound = routesMethods.ContainsKey(route) && routesMethods[route].Contains(method);

            var statusCode = isFound
                ? StatusCode.OK
                : StatusCode.NotFound;

            return new HttpResponse(statusCode);
        }

        private static Dictionary<string, HashSet<string>> RegisterRoutes()
        {
            var pathsMethods = new Dictionary<string, HashSet<string>>();

            var input = Console.ReadLine();
            while (input != "END")
            {
                if (input != null)
                {
                    var pathAndMethod = input.Split("/", StringSplitOptions.RemoveEmptyEntries);

                    if (pathAndMethod.Length < 2)
                    {
                        continue;
                    }

                    var path = pathAndMethod[0];
                    var method = pathAndMethod[1];

                    if (!pathsMethods.ContainsKey(path))
                    {
                        pathsMethods.Add(path, new HashSet<string>());
                    }

                    pathsMethods[path].Add(method);
                }

                input = Console.ReadLine();
            }

            return pathsMethods;
        }
    }
}
