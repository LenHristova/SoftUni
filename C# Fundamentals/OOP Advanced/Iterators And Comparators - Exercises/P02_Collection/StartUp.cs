using System;
using System.Linq;

namespace P02_Collection
{
    public class StartUp
    {
        static void Main()
        {
            var colection = Console.ReadLine().Split().Skip(1).ToList();
            var listyIterator = new ListyIterator<string>(colection);
            var engine = new Engine(listyIterator);
            engine.Run();
        }
    }
}