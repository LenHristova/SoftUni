using System;
using System.IO;
using System.Linq;

namespace MaxSequenceOfEqualElements
{
    class StartUp
    {
        static void Main()
        {
            var lines = File.ReadAllLines(@"..\..\input.txt");

            File.Delete(@"..\..\output.txt");

            foreach (var line in lines)
            {
                var numbers = line
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var maxSequenceOfEqualElements =
                    FindMaxSequenceOfEqualElements(numbers);
                var res = string.Join(" ", maxSequenceOfEqualElements);
                File.AppendAllText(@"..\..\output.txt", res + Environment.NewLine);
            }
        }

        static int[] FindMaxSequenceOfEqualElements(int[] nums)
        {
            var counter = 0;
            var max = -1;
            var element = nums[0];
            var lastPos = nums.Length;
            for (var pos = 0; pos < lastPos; pos++)
            {
                if (pos != lastPos - 1)
                {
                    if (nums[pos] == nums[pos + 1])
                    {
                        counter++;
                    }
                    else
                    {
                        if (counter > max)
                        {
                            max = counter;
                            element = nums[pos];
                        }
                        counter = 0;
                    }
                }
                else
                {
                    if (counter <= max) continue;
                    max = counter;
                    element = nums[pos];
                }
            }
            return new int[max + 1].Select(n => element).ToArray();
        }
    }
}
