using System;

namespace p18_DifferentIntegersSize
{
    class DifferentIntegersSize
    {
        static void Main(string[] args)
        {
            string num = Console.ReadLine();

            if (long.TryParse(num, out long longNum))
            {
                Console.WriteLine($"{num} can fit in:");

                if (sbyte.TryParse(num, out sbyte sbNum))
                {
                    Console.WriteLine("* sbyte");
                }
                if (byte.TryParse(num, out byte bNum))
                {
                    Console.WriteLine("* byte");
                }
                if (short.TryParse(num, out short shNum))
                {
                    Console.WriteLine("* short");
                }
                if (ushort.TryParse(num, out ushort ushNum))
                {
                    Console.WriteLine("* ushort");
                }
                if (int.TryParse(num, out int iNum))
                {
                    Console.WriteLine("* int");
                }
                if (uint.TryParse(num, out uint uiNum))
                {
                    Console.WriteLine("* uint");
                }
                Console.WriteLine("* long");
            }
            else
            {
                Console.WriteLine($"{num} can't fit in any type");
            }
        }
    }
}
