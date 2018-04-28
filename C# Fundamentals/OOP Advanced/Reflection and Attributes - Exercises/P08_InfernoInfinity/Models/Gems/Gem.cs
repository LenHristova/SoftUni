using System;

public abstract class Gem : IGem
{
    protected Gem(int strengthIncreasing, int agilityIncreasing, int vitalityIncreasing, string clarity)
    {
        Clarity = SetClarity(clarity);
        StrengthIncreasing = strengthIncreasing + (int)Clarity;
        AgilityIncreasing = agilityIncreasing + (int)Clarity;
        VitalityIncreasing = vitalityIncreasing + (int)Clarity;
    }
    private Clarity SetClarity(string clarityStr)
    {
        var isValidClarity = Enum.TryParse(typeof(Clarity), clarityStr, out var clarity);
        if (!isValidClarity)
        {
            throw new ArgumentException($"Invalid clarity type: {clarityStr}");
        }

        return (Clarity)clarity;
    }

    public Clarity Clarity { get; }

    public int StrengthIncreasing { get; }

    public int AgilityIncreasing { get; }

    public int VitalityIncreasing { get; }
}