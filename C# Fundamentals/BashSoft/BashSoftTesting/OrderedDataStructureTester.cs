using System;
using System.Collections.Generic;

using BashSoft.Contracts;
using BashSoft.DataStructures;

using NUnit.Framework;

namespace BashSoftTesting
{
    public class OrderedDataStructureTester
    {
        private ISimpleOrderedBag<string> _names;

        private static SimpleSortedList<string> CreateSimpleSortedList()
        {
            return new SimpleSortedList<string>();
        }

        [Test]
        public void TestEmptyCtor()
        {
            _names = CreateSimpleSortedList();

            var expectedCapacity = 16;
            var expectedSize = 0;

            Assert.That(_names.Capacity, Is.EqualTo(expectedCapacity), "Initial capacity have to be 16.");
            Assert.That(_names.Size, Is.EqualTo(expectedSize), "Initial size have to be 0.");
        }

        [Test]
        public void TestCtorWithInitialCapacity()
        {
            var initialCapacity = 20;
            _names = new SimpleSortedList<string>(initialCapacity);

            var expectedCapacity = initialCapacity;
            var expectedSize = 0;

            Assert.That(_names.Capacity, Is.EqualTo(expectedCapacity), "Capacity must be equal to the provided one.");
            Assert.That(_names.Size, Is.EqualTo(expectedSize), "Initial size have to be 0.");
        }

        [Test]
        public void TestCtorWithInitialComparer()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;

            _names = new SimpleSortedList<string>(comparer);

            var expectedCapacity = 16;
            var expectedSize = 0;

            Assert.That(_names.Capacity, Is.EqualTo(expectedCapacity), "Initial capacity have to be 16.");
            Assert.That(_names.Size, Is.EqualTo(expectedSize), "Initial size have to be 0.");
        }

        [Test]
        public void TestCtorWithAllParams()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            var initialCapacity = 30;
            _names = new SimpleSortedList<string>(comparer, initialCapacity);

            var expectedCapacity = initialCapacity;
            var expectedSize = 0;

            Assert.That(_names.Capacity, Is.EqualTo(expectedCapacity), "Capacity must be equal to the provided one.");
            Assert.That(_names.Size, Is.EqualTo(expectedSize), "Initial size have to be 0.");
        }

        [Test]
        public void TestAddIcreasesSize()
        {
            _names = CreateSimpleSortedList();
            _names.Add("Len");
            var expectedSize = 1;

            Assert.That(_names.Size, Is.EqualTo(expectedSize), "Size is not increased.");
        }

        [Test]
        public void TestAddNullThrowsException()
        {
            _names = CreateSimpleSortedList();

            Assert.That(() => _names.Add(null), Throws.ArgumentNullException, "\"ArgumentNullException\" have to be thrown.");
        }

        [Test]
        public void TestAddUnsortedDataIsHeldSorted()
        {
            _names = CreateSimpleSortedList();

            var collection = new List<string>
            {
               "Rosen",
               "Balcan",
               "Georgi"
            };

            foreach (var name in collection)
            {
                _names.Add(name);

                Assert.That(_names, Is.Ordered, "Elements are not sorted when added.");
            }
        }

        [Test]
        public void TestAddingMoreThanInitialCapacity()
        {
            _names = CreateSimpleSortedList();

            var addedElementsCount = 17; // <- default cappacity + 1;
            for (int i = 0; i < addedElementsCount; i++)
            {
                _names.Add("Name");
            }

            var expectedSize = addedElementsCount;
            Assert.That(_names.Size, Is.EqualTo(expectedSize), "Size is not increased.");
            Assert.That(_names.Capacity, Is.GreaterThan(expectedSize), "Capacity have to be greater then 17.");
        }

        [Test]
        public void TestAddingAllFromCollectionIncreasesSize()
        {
            _names = CreateSimpleSortedList();
            var collection = new List<string>
            {
                "Rosen",
                "Balcan",
                "Georgi"
            };

            _names.AddAll(collection);

            var expectedSize = collection.Count;
            Assert.That(_names.Size, Is.EqualTo(expectedSize), "Size have to be equal to the size of the added collection.");
        }

        [Test]
        public void TestAddingAllFromNullThrowsException()
        {
            _names = CreateSimpleSortedList();

            Assert.That(() => _names.AddAll(null), Throws.ArgumentNullException, "\"ArgumentNullException\" have to be thrown.");
        }

        [Test]
        public void TestAddAllKeepsSorted()
        {
            _names = CreateSimpleSortedList();
            var collection = new List<string>
            {
                "Rosen",
                "Len",
                "All",
                "Balcan",
                "Magi",
                "Jes",
                "Georgi",
                "Sam"
            };

            _names.AddAll(collection);

            Assert.That(_names, Is.Ordered, "Elements are not sorted.");
        }

        [Test]
        public void TestRemoveValidElementDecreasesSize()
        {
            _names = CreateSimpleSortedList();
            _names.Add("Name");
            _names.Remove("Name");

            var expectedSize = 0;
            Assert.That(_names.Size, Is.EqualTo(expectedSize), "Size is not decreased.");
        }

        [Test]
        public void TestRemoveValidElementRemovesSelectedOne()
        {
            _names = CreateSimpleSortedList();
            _names.Add("Ivan");
            _names.Add("Nasko");
            _names.Remove("Ivan");

            Assert.That(_names, Is.Not.AnyOf("Ivan"), "Have to remove element.");
        }

        [Test]
        public void TestRemovingNullThrowsException()
        {
            _names = CreateSimpleSortedList();

            Assert.That(() => _names.Remove(null), Throws.ArgumentNullException, "\"ArgumentNullException\" have to be thrown.");
        }

        [Test]
        public void TestJoinWithNull()
        {
            _names = CreateSimpleSortedList();
            _names.Add("Ivan");
            _names.Add("Nasko");

            Assert.That(() => _names.JoinWith(null), Throws.ArgumentNullException, "\"ArgumentNullException\" have to be thrown.");
        }

        [Test]
        public void TestJoinWorksFine()
        {
            _names = CreateSimpleSortedList();
            _names.Add("Ivan");
            _names.Add("Nasko");
            _names.Add("Bond");

            var expectedResult = "Bond, Ivan, Nasko";
            Assert.That(() => _names.JoinWith(", "), Is.EqualTo(expectedResult), "Elements are not joined correctly");
        }
    }
}
