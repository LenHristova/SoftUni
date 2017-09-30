using System;

namespace p01_PracticeIntegers
{
    class PracticeIntegers
    {
        static void Main(string[] args)
        {
            sbyte sbyteNum = -100;
            byte byteNum = 128;
            short shortNum = -3540;
            ushort ushortNum = 64876;
            uint uintNum = 2147483648u;
            int intNum = -1141583228;
            long longNum = -1223372036854775808;

            Console.WriteLine(
                $"{sbyteNum}\r\n" +
                $"{byteNum}\r\n" +
                $"{shortNum}\r\n" +
                $"{ushortNum}\r\n" +
                $"{uintNum}\r\n" +
                $"{intNum}\r\n" +
                $"{longNum}");
        }
    }
}
