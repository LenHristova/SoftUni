using System;
using System.Collections.Generic;

public static class TyreFactory
{
    public static Tyre CreateTyre(List<string> parameters)
    {
        var type = parameters[0];
        var hardness = double.Parse(parameters[1]);

        switch (type)
        {
            case "Ultrasoft":
                var grip = double.Parse(parameters[2]);
                return new UltrasoftTyre(hardness, grip);

            case "Hard":
                return new HardTyre(hardness);

                default:
                    throw new NotImplementedException();
        }
    }
}