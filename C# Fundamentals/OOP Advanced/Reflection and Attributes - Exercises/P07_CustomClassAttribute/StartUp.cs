using System;
using System.Reflection;

namespace P07_CustomClassAttribute
{
    public class StartUp
    {
        static void Main()
        {
            var assembly = Assembly.GetAssembly(typeof(Weapon));
            var weaponTypeInfo = assembly.GetType(nameof(Weapon));
            var attribute = (CustomAttribute)weaponTypeInfo.GetCustomAttribute(typeof(CustomAttribute));

            string searchedProperty;
            while ((searchedProperty = Console.ReadLine()) != "END")
            {
                var property = attribute.GetType().GetProperty(searchedProperty);
                Console.WriteLine(property.GetValue(attribute));
            }
        }
    }
}
