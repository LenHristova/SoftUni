using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using BashSoft.Contracts;
using BashSoft.DataStructures;

using NUnit.Framework;

namespace BashSoftTesting
{
    public class OrderedDataStructureTester
    {
        private const int DEFAULT_CAPACITY = 16;

        private static Comparer<string> DefaultComparer() => Comparer<string>.Create((x, y) => x.CompareTo(y));

        private ISimpleOrderedBag<string> _orderedData;

        private SimpleSortedList<string> CreateSimpleSortedList() => new SimpleSortedList<string>();

        //Sorce for most of the tests
        static object[] Data =
        {
            new string[]
            {
                "Rosen",
                "Balcan",
                "Georgi",
                "All",
                "Zoro",
                "Mo"
            }
        };

        private void SetUp(params string[] innerArrayValue)
        {
            _orderedData = CreateSimpleSortedList();

            var size = innerArrayValue.Length;
            var capacity = DEFAULT_CAPACITY;
            while (size >= capacity)
            {
                capacity *= 2;
            }

            Array.Sort(innerArrayValue);
            innerArrayValue = innerArrayValue.Concat(new string[capacity - size]).ToArray();

            GetFieldInfo(typeof(string[]), "_innerCollection").SetValue(_orderedData, innerArrayValue);
            GetFieldInfo(typeof(int), "<Size>k__BackingField").SetValue(_orderedData, size);
            GetFieldInfo(typeof(IComparer<string>), "_comparer").SetValue(_orderedData, DefaultComparer());
        }

        private FieldInfo GetFieldInfo(Type type, string name)
        {
            var fieldInfo = _orderedData.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(f => f.FieldType == type && f.Name == name);

            Assert.That(fieldInfo, Is.Not.EqualTo(null), $"Private field of type \"{type}\" with name: \"{name}\" not found.");

            return fieldInfo;
        }

        [Test]
        public void EmptyCtorShouldWorkCorrectly()
        {
            _orderedData = CreateSimpleSortedList();

            var expectedCapacity = DEFAULT_CAPACITY;
            var expectedSize = 0;

            Assert.That(_orderedData.Capacity, Is.EqualTo(expectedCapacity), $"Initial capacity should be {DEFAULT_CAPACITY}.");
            Assert.That(_orderedData.Size, Is.EqualTo(expectedSize), "Initial size should be 0.");
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(100)]
        public void CtorWithPositiveInitialCapacityShouldWorkCorrectly(int initialCapacity)
        {
            _orderedData = new SimpleSortedList<string>(initialCapacity);

            var expectedCapacity = initialCapacity;
            var expectedSize = 0;

            Assert.That(_orderedData.Capacity, Is.EqualTo(expectedCapacity), "Capacity should be equal to the provided one.");
            Assert.That(_orderedData.Size, Is.EqualTo(expectedSize), "Initial size should be 0.");
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-15)]
        [TestCase(-100)]
        public void CtorWithNegativeInitialCapacityShouldThrowException(int initialCapacity)
        {
            Assert.That(() => new SimpleSortedList<string>(initialCapacity), Throws.ArgumentException, "\"ArgumentException\" should be thrown.");
        }

        [Test]
        public void CtorWithInitialComparerShouldWorkCorrectly()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;

            _orderedData = new SimpleSortedList<string>(comparer);

            var expectedCapacity = DEFAULT_CAPACITY;
            var expectedSize = 0;

            Assert.That(_orderedData.Capacity, Is.EqualTo(expectedCapacity), $"Initial capacity should be {DEFAULT_CAPACITY}.");
            Assert.That(_orderedData.Size, Is.EqualTo(expectedSize), "Initial size should be 0.");
        }

        [Test]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(100)]
        public void CtorWithAllValidParamsShouldWorkCorrectly(int initialCapacity)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            _orderedData = new SimpleSortedList<string>(comparer, initialCapacity);

            var expectedCapacity = initialCapacity;
            var expectedSize = 0;

            Assert.That(_orderedData.Capacity, Is.EqualTo(expectedCapacity), "Capacity must be equal to the provided one.");
            Assert.That(_orderedData.Size, Is.EqualTo(expectedSize), "Initial size have to be 0.");
        }

        [Test]
        [TestCase(1)]
        [TestCase(7)]
        [TestCase(10)]
        public void AddShouldIcreaseSize(int numberOfAddedElements)
        {
            SetUp();

            for (int i = 0; i < numberOfAddedElements; i++)
            {
                _orderedData.Add("Pesho");
            }

            var expectedSize = numberOfAddedElements;

            Assert.That(_orderedData.Size, Is.EqualTo(expectedSize), "Size is not properly increased.");
        }

        [Test]
        public void AddNullShouldThrowException()
        {
            SetUp();

            Assert.That(() => _orderedData.Add(null), Throws.ArgumentNullException, "\"ArgumentNullException\" have to be thrown.");
        }

        [Test]
        [TestCaseSource(nameof(Data))]
        public void AddUnsortedDataShouldHeldSorted(string[] data)
        {
            SetUp();

            foreach (var name in data)
            {
                _orderedData.Add(name);

                Assert.That(_orderedData, Is.Ordered, "Elements are not sorted when added.");
            }
        }

        [Test]
        public void AddMoreThanInitialCapacityShouldWorkCorrectly()
        {
            SetUp();

            var addedElementsCount = DEFAULT_CAPACITY + 1;
            for (int i = 0; i < addedElementsCount; i++)
            {
                _orderedData.Add("Name");
            }

            var expectedSize = addedElementsCount;
            Assert.That(_orderedData.Size, Is.EqualTo(expectedSize), "Size is not properly increased.");
            Assert.That(_orderedData.Capacity, Is.GreaterThan(expectedSize), $"Capacity should be greater then {DEFAULT_CAPACITY} + 1.");
        }

        [Test]
        [TestCaseSource(nameof(Data))]
        public void AddAllFromCollectionShouldIncreasesSize(string[] data)
        {
            SetUp();

            _orderedData.AddAll(data);

            var expectedSize = data.Length;
            Assert.That(_orderedData.Size, Is.EqualTo(expectedSize), "Size is not properly increased.");
        }

        [Test]
        public void AddAllFromNullShouldThrowException()
        {
            SetUp();

            Assert.That(() => _orderedData.AddAll(null), Throws.ArgumentNullException, "\"ArgumentNullException\" should be thrown.");
        }

        [Test]
        [TestCaseSource(nameof(Data))]
        public void AddAllShouldKeepSorted(string[] data)
        {
            SetUp();

            _orderedData.AddAll(data);

            Assert.That(_orderedData, Is.Ordered, "Elements are not sorted.");
        }

        [Test]
        [TestCaseSource(nameof(Data))]
        public void RemoveValidElementShouldDecreaseSize(string[] data)
        {
            SetUp(data);
            var elementToRemove = data[data.Length / 2];
            _orderedData.Remove(elementToRemove);

            var expectedSize = data.Length - 1;
            Assert.That(_orderedData.Size, Is.EqualTo(expectedSize), "Size is not decreased.");
        }

        [Test]
        [TestCaseSource(nameof(Data))]
        public void RemoveValidElementShouldRemoveSelectedOne(string[] data)
        {
            SetUp(data);

            var elementToRemove = data[data.Length / 2];
            _orderedData.Remove(elementToRemove);

            Assert.That(_orderedData, Is.Not.AnyOf(elementToRemove), "Element not removed.");
        }

        [Test]
        [TestCaseSource(nameof(Data))]
        public void RemoveNullShouldThrowException(string[] data)
        {
            SetUp(data);

            Assert.That(() => _orderedData.Remove(null), Throws.ArgumentNullException, "\"ArgumentNullException\" should be thrown.");
        }

        [Test]
        [TestCase(", ")]
        [TestCase(" ")]
        [TestCase("\n\r")]
        public void JoinWithShouldWorkCorrectly(string joiner)
        {
            var innerArrayValue = new string[]
            {
                "All",
                "Mo",
                "Zoro",
            };

            SetUp(innerArrayValue);

            var expectedResult = string.Join(joiner, innerArrayValue);
            Assert.That(() => _orderedData.JoinWith(joiner), Is.EqualTo(expectedResult), "Elements are not joined correctly");
        }

        [Test]
        [TestCaseSource(nameof(Data))]
        public void JoinWithNullShouldThrowException(string[] data)
        {
            SetUp(data);

            Assert.That(() => _orderedData.JoinWith(null), Throws.ArgumentNullException, "\"ArgumentNullException\" should be thrown.");
        }
    }
}
