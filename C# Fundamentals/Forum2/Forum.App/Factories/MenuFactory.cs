using System;
using System.Linq;
using System.Reflection;

using Forum.App.Contracts;

namespace Forum.App.Factories
{
    public class MenuFactory : IMenuFactory
    {
        private readonly IServiceProvider serviceProvider;

        public MenuFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IMenu CreateMenu(string menuName)
        {
            Type menuType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == menuName);

            if (menuType == null)
            {
                throw new InvalidOperationException("Menu not found!");
            }

            if (!typeof(IMenu).IsAssignableFrom(menuType))
            {
                throw new InvalidOperationException($"{menuType} is not a menu!");
            }

            var args = menuType
                .GetConstructors()
                .First()
                .GetParameters()
                .Select(p => serviceProvider.GetService(p.ParameterType))
                .ToArray();

            IMenu menu = (IMenu) Activator.CreateInstance(menuType, args);

            return menu;
        }
    }
}
