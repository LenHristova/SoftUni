using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p02_Change_List
{
    class ChangeList
    {
        static void Main()
        {
            List<int> numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.None)
                .Select(int.Parse)
                .ToList();

            while (true)
            {
                string[] tokens = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();
                string command = tokens[0];
                if (command == "Odd")
                {
                    Console.WriteLine(string.Join(
                        " ", numbers.Where(index => index % 2 != 0)));
                    break;
                }
                if (command == "Even")
                {
                    Console.WriteLine(string.Join(
                        " ", numbers.Where(index => index % 2 == 0)));
                    break;
                }
                if (command == "Delete")
                {
                    int element = int.Parse(tokens[1]);
                    numbers.RemoveAll(n => n == element);
                }
                else if (command == "Insert")
                {
                    int element = int.Parse(tokens[1]);
                    int position = int.Parse(tokens[2]);
                    numbers.Insert(position, element);
                }
            }
        }
    }
}
