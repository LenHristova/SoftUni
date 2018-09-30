namespace SIS.HTTP.Extensions
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public static class EnumExtensions
    {
        /// <summary>
        /// Gets enum name. If the name is specified by DisplayAttribute
        /// returns a value that is used for field display in the UI.
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            var member = enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault();

            if (member == null)
            {
                throw new InvalidEnumArgumentException();
            }

            var displayAttribute = member
                .GetCustomAttributes<DisplayAttribute>()
                .FirstOrDefault();

            return displayAttribute == null
                ? member.Name
                : displayAttribute.GetName();
        }
    }
}
