using System;

namespace p06_StringsAndObjects
{
    class StringsAndObjects
    {
        static void Main(string[] args)
        {
            string hello = "Hello";
            string world = "World";
            object greeting = hello + " " + world;
            string greet = (string)greeting;

            Console.WriteLine(greeting);
        }
    }
}
