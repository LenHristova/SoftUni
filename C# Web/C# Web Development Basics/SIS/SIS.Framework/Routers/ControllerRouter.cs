namespace SIS.Framework.Routers
{
    using ActionResults.Contracts;
    using Attributes.Methods;
    using Controllers;
    using HTTP.Enums;
    using HTTP.Extensions;
    using HTTP.Requests.Contracts;
    using HTTP.Responses;
    using HTTP.Responses.Contracts;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Serialization;
    using WebServer.Api.Contracts;
    using WebServer.Results;

    public class ControllerRouter : IHttpHandler
    {
        public IHttpResponse Handle(IHttpRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Path))
            {
                return new HttpResponse(HttpResponseStatusCode.NotFound);
            }

            this.GetControllerAndMethodNames(request.Path, out var controllerName, out var actionName);

            var controller = this.GetController(controllerName, request);

            var action = this.GetAction(request.Method, controller, actionName);

            return this.PrepareResponse(controller, action);
        }

        private void GetControllerAndMethodNames(string path, out string controllerName, out string actionName)
        {
            var routeEndIndex = path.Contains("?")
                ? path.IndexOf("?", StringComparison.Ordinal)
                : path.Length;

            var pathParts = path
                .Substring(0, routeEndIndex)
                .Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (pathParts.Length == 0)
            {
                controllerName = $"Home{MvcContext.Instance.ControllersSuffix}";
                actionName = "Index";
            }
            else if(pathParts.Length >= 2)
            {
                controllerName = $"{pathParts[0].Capitalize()}{MvcContext.Instance.ControllersSuffix}";
                actionName = pathParts[1].Capitalize();
            }
            else 
            {
                controllerName = null;
                actionName = null;
            }
        }

        public object GetController(string controllerName, IHttpRequest request)
        {
            var controllerFullQualifiedName = string.Format("{0}.{1}.{2}, {0}",
                MvcContext.Instance.AssemblyName,
                MvcContext.Instance.ControllersFolder,
                controllerName);

            var controllerType = Type.GetType(controllerFullQualifiedName);

            if (controllerType == null)
            {
                return null;
            }

            var controllerInstance = Activator.CreateInstance(controllerType);

            if (controllerInstance is Controller controller)
            {
                controller.Request = request;
            }

            return controllerInstance;
        }

        private MethodInfo GetAction(HttpRequestMethod requestMethod, object controller, string action)
        {
            if (controller == null)
            {
                return null;
            }

            var suitableMethods = controller
                .GetType()
                .GetMethods()
                .Where(m => m.Name.ToLower() == action.ToLower());

            foreach (var methodInfo in suitableMethods)
            {
                var httpMethodAttributes = methodInfo
                    .GetCustomAttributes()
                    .Where(a => a is HttpMethodAttribute)
                    .Cast<HttpMethodAttribute>()
                    .ToList();

                if (!httpMethodAttributes.Any() && requestMethod == HttpRequestMethod.Get)
                {
                    return methodInfo;
                }

                foreach (var attribute in httpMethodAttributes)
                {
                    if (attribute.IsValid(requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }

            return null;
        }

        private IHttpResponse PrepareResponse(object controller, MethodInfo action)
        {
            if (action == null)
            {
                return new HttpResponse(HttpResponseStatusCode.NotFound);
            }

            var result = action.Invoke(controller, null);

            if (result == null)
            {
                return new HttpResponse(HttpResponseStatusCode.NotFound);
            }
            if (result is IActionResult actionResult)
            {
                return actionResult.Invoke();
            }

            return new TextResult(result.ToString(), HttpResponseStatusCode.Ok);

            //XmlSerializer xsSubmit = new XmlSerializer(result.GetType());

            //var xml = "";

            //using (var stringWriter = new StringWriter())
            //{
            //    using (XmlWriter writer = XmlWriter.Create(stringWriter))
            //    {
            //        xsSubmit.Serialize(writer, result);
            //        xml = stringWriter.ToString(); 
            //    }
            //}

            //return new TextResult(xml, HttpResponseStatusCode.Ok);
        }
    }
}
