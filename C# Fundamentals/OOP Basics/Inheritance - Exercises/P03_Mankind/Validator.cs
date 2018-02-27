using System;
using System.Text.RegularExpressions;

namespace P03_Mankind
{
    public static class Validator
    {
        private const string FACULTY_NUMBER_PATTERN = "^[A-Za-z0-9]{5,10}$";
        private const string INVALID_FIRST_LETTER = "Expected upper case letter! Argument: {0}";
        private const string INVALID_LENGTH = "Expected length at least {0} symbols! Argument: {1}";
        private const string INVALID_FACULTY_NUMBER = "Invalid faculty number!";
        private const string INVALID_VALUE = "Expected value mismatch! Argument: {0}";

        public static void ValidateNameFirstLetter(char valueFirstLetter, string argument)
        {
            if (char.IsLower(valueFirstLetter))
            {
                throw new ArgumentException(string.Format(INVALID_FIRST_LETTER, argument));
            }
        }

        public static void ValidateNameLength(string value, int minLength, string argument)
        {
            if (value.Length < minLength)
            {
                throw new ArgumentException(string.Format(INVALID_LENGTH, minLength, argument));
            }
        }

        public static void ValidateFacultyNumber(string value)
        {
            if (!Regex.IsMatch(value, FACULTY_NUMBER_PATTERN))
            {
                throw new ArgumentException(INVALID_FACULTY_NUMBER);
            }
        }

        public static void ValidateSalary(decimal value, decimal minValue, string argument)
        {
            if (value < minValue)
            {
                throw new ArgumentException(string.Format(INVALID_VALUE, argument));
            }
        }

        public static void ValidateWorkHours(double value, int minValue, int maxValue, string argument)
        {
            if (value < minValue || value > maxValue)
            {
                throw new ArgumentException(string.Format(INVALID_VALUE, argument));
            }
        }
    }
}
