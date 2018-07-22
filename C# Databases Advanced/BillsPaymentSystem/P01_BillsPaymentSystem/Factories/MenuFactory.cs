namespace P01_BillsPaymentSystem.Factories
{
	using System;
	using System.Linq;
	using System.Reflection;
	using Contracts;
	using Contracts.Factories;

    public class MenuFactory : IMenuFactory
    {
        private readonly IServiceProvider serviceProvider;

        public MenuFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IMenu CreateMenu(string menuName, params object[] models)
        {
            var menuType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => typeof(IMenu).IsAssignableFrom(t) && t.Name == menuName && !t.IsInterface && !t.IsAbstract);

            if (menuType == null)
            {
                throw new InvalidOperationException("Menu not found!");
            }

            var args = menuType
                .GetConstructors()
                .First()
                .GetParameters()
                .Select(p => serviceProvider.GetService(p.ParameterType))
                .Concat(models)
                .Where(p => p != null)
                .ToArray();

            var menu = Activator.CreateInstance(menuType, args);

            return (IMenu)menu;
        }
    }
}
