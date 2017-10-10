using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)
    {
        char[] arr1 = Console.ReadLine()
            .Replace(" ", "")
            .ToCharArray();
        char[] arr2 = Console.ReadLine()
            .Replace(" ", "")
            .ToCharArray();

        PrintArraysInAlphabeticalOrder(arr1, arr2);
    }

    private static void PrintArraysInAlphabeticalOrder(char[] arr1, char[] arr2)
    {
        int shorterArrLenght = Math.Min(arr1.Length, arr2.Length);
        string arr1ToString = string.Join("", arr1);
        string arr2ToString = string.Join("", arr2);

        for (int pos = 0; pos < shorterArrLenght; pos++)
        {
            if (arr1[pos] < arr2[pos])
            {
                Console.WriteLine($"{arr1ToString}\r\n{arr2ToString}");
                return;
            }
            else if (arr1[pos] > arr2[pos])
            {
                Console.WriteLine($"{arr2ToString}\r\n{arr1ToString}");
                return;
            }
        }
        if (arr1.Length <= arr2.Length)
        {
            Console.WriteLine($"{arr1ToString}\r\n{arr2ToString}");
        }
        else
        {
            Console.WriteLine($"{arr2ToString}\r\n{arr1ToString}");
        }
    }
}

