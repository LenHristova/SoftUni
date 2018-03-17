public static class Validator
{
    public static void ValidateNotNegative(double value, string property)
    {
        if (value < 0)
        {
            throw new InvalidPropertyExeption("Value must be zero or positive", property);
        }
    }

    public static void ValidateIsPositive(double value, string property)
    {
        if (value <= 0)
        {
            throw new InvalidPropertyExeption("Value must be positive", property);
        }
    }

    public static void ValidateMaxValue(double value, double maxValue, string property)
    {
        if (value > maxValue)
        {
            throw new InvalidPropertyExeption($"Given value: {value} is bigger then appropriate max value: {maxValue}", property);
        }
    }

    public static void ValidateStringNotNullOrWhiteSpace(string value, string property)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidPropertyExeption("Invalid string!", property);
        }
    }
}