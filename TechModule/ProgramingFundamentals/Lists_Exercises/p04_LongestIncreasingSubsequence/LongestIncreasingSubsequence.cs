using System;
using System.Collections.Generic;
using System.Linq;

namespace p04_LongestIncreasingSubsequence
{
    class LongestIncreasingSubsequence
    {
        static void Main()
        {
            int[] numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int[] len = new int[numbers.Length];
            int[] prev = new int[numbers.Length];

            int prevIndex = -1;
            for (int currentNum = 0; currentNum < numbers.Length; currentNum++)
            {
                int currentMaxLen = 1;     
                for (int checkedNumIndex = 0; checkedNumIndex < currentNum; checkedNumIndex++)
                {
                    if (numbers[currentNum] > numbers[checkedNumIndex])
                    {
                        if (len[checkedNumIndex] > currentMaxLen - 1)
                        {
                            prevIndex = checkedNumIndex;
                            currentMaxLen = len[checkedNumIndex] + 1;
                        }                       
                    }
                }
                len[currentNum] = currentMaxLen;
                prev[currentNum] = prevIndex;
            }
            
            int maxIndex = MaxLen(len);
            List<int> lis = new List<int>();
            while (maxIndex >= 0)
            {
                lis.Insert(0, numbers[maxIndex]);
                maxIndex = prev[maxIndex];
            }

            Console.WriteLine(string.Join(" ", lis));
        }

        //Find index of leftmost max length
        private static int MaxLen(int[] len)
        {
            int maxLength = 0;
            int maxIndex = 0; 
            for (int i = 1; i < len.Length; i++)
            {
                if (len[i] > maxLength)
                {
                    maxLength = len[i];
                    maxIndex = i;
                }
            }
            return maxIndex;
        }
    }
}
