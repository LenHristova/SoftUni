using System;

using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string[] words = Console.ReadLine().Split(' ').ToArray();
        string[] reversed = words.Reverse().ToArray();

        Console.WriteLine(string.Join(" ", reversed));
    }
}

