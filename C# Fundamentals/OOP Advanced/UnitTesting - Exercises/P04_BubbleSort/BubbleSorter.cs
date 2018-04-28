using System;
using System.Collections.Generic;

namespace P04_BubbleSort
{
    public class BubbleSorter<T> where T : IComparable<T>
    {
        public void Sort(IList<T> elements)
        {
            var currentUnorderedElementsCount = elements.Count;
            var hasSwap = true;
            while (hasSwap)
            {
                hasSwap = false;
                for (int index = 1; index < currentUnorderedElementsCount; index++)
                {
                    if (elements[index].CompareTo(elements[index - 1]) < 1)
                    {
                        Swap(index, elements);
                        hasSwap = true;
                    }
                }

                currentUnorderedElementsCount--;
            }
        }

        private static void Swap(int index, IList<T> elements)
        {
            var element = elements[index];
            elements[index] = elements[index - 1];
            elements[index - 1] = element;
        }
    }
}
