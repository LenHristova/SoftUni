using System;
using System.Reflection;

public class GemFactory : Factory, IGemFactory
{
    public IGem CreateGem(string gemType, string gemClarity)
    {
        var type = Assembly
            .GetExecutingAssembly()
            .GetType(gemType);

        EnsureType(type, typeof(IGem), gemType);

        var gem = (IGem)Activator.CreateInstance(type, new object[] {gemClarity});
        return gem;
    }
}
