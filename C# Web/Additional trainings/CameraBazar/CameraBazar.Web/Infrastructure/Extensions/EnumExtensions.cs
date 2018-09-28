namespace CameraBazar.Web.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var member = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First();

            var displayAttribute = member.GetCustomAttribute<DisplayAttribute>();
            if (displayAttribute == null)
            {
                return member.Name;
            }

            return displayAttribute.GetName();
        }

        public static IEnumerable<Enum> GetFlags(this Enum enumValue)
        {
            foreach (Enum value in Enum.GetValues(enumValue.GetType()))
            {
                if (enumValue.HasFlag(value))
                {
                    yield return value;
                }
            }
        }
    }
}
