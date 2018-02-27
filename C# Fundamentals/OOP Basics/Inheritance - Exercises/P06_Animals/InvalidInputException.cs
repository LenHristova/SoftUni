using System;

namespace P06_Animals
{
    public class InvalidInputException : Exception
    {
        public override string Message => "Invalid input!";
    }
}