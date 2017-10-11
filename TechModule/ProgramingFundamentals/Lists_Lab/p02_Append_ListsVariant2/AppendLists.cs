using System;
using System.Collections.Generic;
using System.Linq;

namespace p02_Append_ListsVariant2
{
    class AppendLists
    {
        static void Main()
        {

            List<List<int>> listOfnumbers = Console.ReadLine()
                .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                .Reverse()
                .Select(l => l.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList())
                .ToList();

            Console.WriteLine(string.Join(" ", listOfnumbers.Select(l => string.Join(" ", l))));
        }
    }
}