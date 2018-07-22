namespace MiniORM
{
    using System;
    using System.Linq;
    using System.Reflection;

    internal static class SqlExtensions
    {
        private static readonly Type[] Types =
        {
            typeof(string),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(decimal),
            typeof(bool),
            typeof(DateTime)
        };

        /// <summary>
        /// Extension method for PropertyInfo, which check if PropertyType is allowed Sql type
        /// </summary>
        internal static bool IsAllowedSqlType(this PropertyInfo property) => Types.Contains(property.PropertyType);
    }
}
