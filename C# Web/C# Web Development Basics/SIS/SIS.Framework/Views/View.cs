namespace SIS.Framework.Views
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using ActionResults.Contracts;

    public class View : IRenderable
    {
        public const string LayoutFile = "Layout";
        public const string ContentPlaceholder = "{{{content}}}";
        public const string ErrorMessagePlaceholder = "{{{errorMessage}}}";
        public const string HtmlExtension = ".html";
        public const string ErrorPath = "Errors\\Error.html";

        private readonly string viewPath;
        private readonly IDictionary<string, string> viewData;

        public View(string viewPath)
        {
            this.viewPath = viewPath;
            this.viewData = new Dictionary<string, string>();
        }

        public string Render()
        {
            var fullHtml = this.ReadFile();

            if (this.viewData.Any())
            {
                foreach (var parameter in this.viewData)
                {
                    fullHtml = fullHtml.Replace($"{{{{{{{parameter.Key}}}}}}}", parameter.Value);
                }
            }

            fullHtml = fullHtml.Replace(ErrorMessagePlaceholder, string.Empty);
            return fullHtml;
        }

        private string ReadFile()
        {
            //var layoutHtmlPath = $"{MvcContext.Instance.ViewsFolder}\\{LayoutFile}{HtmlExtension}";
            //var layoutHtml = this.ReadHtml(layoutHtmlPath);

            var contentHtmlPath = this.viewPath + HtmlExtension;
            var contentHtml = this.ReadHtml(contentHtmlPath);

            //    var html = layoutHtml.Replace(ContentPlaceholder, contentHtml);
            //    return html;
            return contentHtml;
        }

        private string ReadHtml(string filePath)
        {
            //var assemblyName = Assembly.GetEntryAssembly().GetName().Name;

            //var appDirectoryPath = Directory.GetCurrentDirectory();
            //var parentDirectory = Directory.GetParent(appDirectoryPath);

            //while (parentDirectory.GetDirectories(assemblyName).Length <= 0)
            //{
            //    parentDirectory = parentDirectory.Parent;
            //}

            //var fileFullPath = parentDirectory + filePath + HtmlExtension;
            if (!File.Exists(filePath))
            {
                return this.ErrorHtml($"The view on path \"{filePath}\" does not exists!");
            }

            var fileContent = File.ReadAllText(filePath);
            return fileContent;
        }

        private string ErrorHtml(string errorMessage)
        {
            var errorPath = this.GetErrorPath();
            var errorHtml = File.ReadAllText(errorPath);
            this.viewData["error"] = errorMessage;

            return errorHtml;
        }

        private string GetErrorPath()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            var appDirectoryPath = Directory.GetCurrentDirectory();
            var parentDirectory = Directory.GetParent(appDirectoryPath);

            while (parentDirectory.GetDirectories(assemblyName).Length <= 0)
            {
                parentDirectory = parentDirectory.Parent;
            }

            var errorPagePath = $"{parentDirectory}\\{assemblyName}\\{ErrorPath}";
            return errorPagePath;
        }
    }
}
