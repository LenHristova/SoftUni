using System;
using System.Linq;
using System.Reflection;

using P01_Logger.Models.Layouts.Contracts;

namespace P01_Logger.Models.Layouts.Factories
{
    public class LayoutFactory
    {
        public ILayout CreateLayout(string type)
        {
            var layoutType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == type);

            Validator.ValidateNotNullType(layoutType, nameof(ILayout).Replace("I", ""), type);

            var layout = (ILayout)Activator.CreateInstance(layoutType);
            return layout;
        }
    }
}
