using System;


class Program
{
    static void Main(string[] args)
    {
        string[] arr1 = Console.ReadLine().Split(' ');
        string[] arr2 = Console.ReadLine().Split(' ');

        int largestCommonEnd = GetLargestCommonEnd(arr1, arr2);
        Console.WriteLine(largestCommonEnd);
    }

    static int GetLargestCommonEnd(string[] arr1, string[] arr2)
    {
        int minArrLenght = Math.Min(arr1.Length, arr2.Length);
        int leftCommons = 0;
        int rightCommons = 0;
        for (int i = 0; i < minArrLenght; i++)
        {
            if (arr1[i] == arr2[i])
            {
                leftCommons++;
            }
            else if (arr1[arr1.Length - i - 1] == arr2[arr2.Length - i - 1])
            {
                rightCommons++;
            }
            else
            {
                break;
            }
        }

        return Math.Max(leftCommons, rightCommons);
    }
}

