using System;

public class Factory
{
    protected void EnsureType(Type type, Type wantedType, string actualType)
    {
        if (type == null)
        {
            throw new ArgumentException($"Invalid {wantedType.Name.Replace("I", "")} type: {actualType}");
        }

        if (!wantedType.IsAssignableFrom(type))
        {
            throw new ArgumentException($"{type} is not a {wantedType}!");
        }
    }
}