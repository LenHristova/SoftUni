using System;

namespace P04_ShoppingSpree
{
    public static class Validation
    {
        private const string EmptyName = "Name cannot be empty";
        private const string NegativeMoney = "Money cannot be negative";

        public static void ValidateName(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
            {
                throw new ArgumentException(EmptyName);
            }
        }

        public static void ValidateMoney(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException(NegativeMoney);
            }
        }
    }
}