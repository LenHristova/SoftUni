namespace SIS.HTTP.Extensions
{
    using System;
    using System.Linq;

    public static class StringExtensions
    {
        /// <summary>
        /// Returns copy of this string,
        /// but makes the first letter capital and all other – lowercase.
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static string Capitalize(this string stringValue)
        {
            if (stringValue == string.Empty)
            {
                return stringValue;
            }

            var stringValueToCharArray = stringValue.ToLower().ToCharArray();
            var firstUpperChar = char.ToUpper(stringValueToCharArray.First());
            var lowerChars = string.Join("", stringValueToCharArray.Skip(1));

            return  $"{firstUpperChar}{lowerChars}";
        }
    }
}
