using System;
using System.Collections.Generic;

    class Program
    {
        static void Main()
        {
            List<string> words = new List<string>()
            {
                Console.ReadLine().Replace(" ", ""),
                Console.ReadLine().Replace(" ", "")
            };
            words.Sort();
            Console.WriteLine(string.Join(Environment.NewLine, words));
        }
    }

