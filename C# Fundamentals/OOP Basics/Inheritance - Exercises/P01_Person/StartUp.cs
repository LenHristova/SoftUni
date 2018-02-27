using System;

namespace P01_Person
{
    internal class StartUp
    {
        static void Main()
        {
            try
            {
                var name = Console.ReadLine();
                var age = int.Parse(Console.ReadLine());
                var child = new Child(name, age);
                Console.WriteLine(child);
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Age must be a number.");
            }
        }
    }
}