using System;
using System.Linq;

namespace p06_ByteFlip
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var decodedPhrase = Console.ReadLine()
                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .ToArray()
                .Where(w => w.Length == 2)
                .Select(w => string.Join("", w.Reverse()))
                .Reverse()
                .Select(w => (char) Convert.ToInt32(w, 16))
                .ToArray();

            Console.WriteLine(string.Join("", decodedPhrase));
        }
    }
}
