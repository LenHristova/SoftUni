using System;

namespace PokeMon
{
    class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());

            int targetsCount = 0;
            int rem = n;
            while (rem >= m)
            {
                rem -= m;
                targetsCount++;
                if (rem < m)
                {
                    break;
                }
                if (rem == (double)n / 2 && y != 0)
                {
                    rem /= y;
                }
            }

            Console.WriteLine(rem);
            Console.WriteLine(targetsCount);
        }
    }
}