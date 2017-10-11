using System;
using System.Collections.Generic;
using System.Linq;

namespace p02_Append_Lists
{
    class AppendLists
    {
        static void Main()
        {
            List<string> lists = Console.ReadLine()
                .Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries)
                .Reverse()
                .ToList();

            List<List<int>> listOfLists = new List<List<int>>();
            foreach (var list in lists)
            {
                listOfLists.Add(list.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList());
            }

            Console.WriteLine(string.Join(" ", listOfLists.Select(l => string.Join(" ", l))));
        }
    }
}