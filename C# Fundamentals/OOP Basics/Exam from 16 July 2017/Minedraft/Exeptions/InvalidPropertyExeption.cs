using System;

public class InvalidPropertyExeption : ArgumentException
{
    public InvalidPropertyExeption(string message, string paramName) : base(message, paramName)
    {
    }
}