using System;

using NUnit.Framework;

using P04_BubbleSort;

namespace P04_BubbleSortTests
{
    public class BubbleSorterTests
    {
        [Test]
        [TestCase(new int[] { })]
        [TestCase(new int[] { 1 })]
        [TestCase(new int[] {2, 5, 8, 10, 3, 15})]
        [TestCase(new int[] {2, 5, -8, 10, 3, -15})]
        [TestCase(new int[] {-2, -5, -8, -10, 0, -3, -15})]
        [TestCase(new int[] {int.MaxValue, 5, -8, 10, int.MinValue, -15})]
        public void SortShouldSortCorrectly(params int[] elementsToSortForActualResult)
        {
            var elementsToSortForExpectedResult = new int[elementsToSortForActualResult.Length];
            elementsToSortForActualResult.CopyTo(elementsToSortForExpectedResult, 0);

            var bubbleSorter = new BubbleSorter<int>();
            bubbleSorter.Sort(elementsToSortForActualResult);

            Array.Sort(elementsToSortForExpectedResult);

            Assert.That(elementsToSortForActualResult, Is.EquivalentTo(elementsToSortForExpectedResult));
        }
    }
}