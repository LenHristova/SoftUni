using System;
using System.Linq;
using System.Reflection;

namespace P01_Stealer
{
    public class Tracker
    {
        public void PrintMethodsByAuthor()
        {
            var types = Assembly.GetCallingAssembly().DefinedTypes;

            foreach (var type in types)
            {
                var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static)
                    .Where(m => m.CustomAttributes.Any(a => a.AttributeType == typeof(SoftUniAttribute)));

                foreach (var method in methods)
                {
                    foreach (var attribute in method.GetCustomAttributes<SoftUniAttribute>(false))
                    {
                        Console.WriteLine($"{method.Name} is written by {attribute.Name}");
                    }
                }
            }
        }
    }
}