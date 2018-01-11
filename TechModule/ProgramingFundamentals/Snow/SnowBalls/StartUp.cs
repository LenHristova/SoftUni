using System;
using System.Numerics;

class StartUp
{
    static void Main()
    {
        var n = int.Parse(Console.ReadLine());

        var maxSnowballSnow = 0.0;
        var maxSnowballTime = 0.0;
        var maxSnowballQuality = 0.0;
        BigInteger maxSnowballValue = 0;
        for (int i = 0; i < n; i++)
        {
            var snowballSnow = double.Parse(Console.ReadLine());
            var snowballTime = double.Parse(Console.ReadLine());
            var snowballQuality = int.Parse(Console.ReadLine());

            var temp = snowballSnow / snowballTime;
            BigInteger snowballValue = BigInteger.Pow((BigInteger)temp, snowballQuality);

            if (snowballValue > maxSnowballValue)
            {
                maxSnowballSnow = snowballSnow;
                maxSnowballTime = snowballTime;
                maxSnowballQuality = snowballQuality;
                maxSnowballValue = snowballValue;
            }
        }

        Console.WriteLine($"{maxSnowballSnow} : {maxSnowballTime} = {maxSnowballValue} ({maxSnowballQuality})");
    }
}