using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

using P03_Iterator;

namespace P03_IteratorTests
{
    public class ListIteratorTests
    {
        private ListIterator _listIterator;
        private FieldInfo _elementsFieldInfo;
        private FieldInfo _internalIndexFieldInfo;

        private static ListIterator CreateListIterator(params string[] elements)
        {
            return new ListIterator(elements);
        }

        private FieldInfo GetFieldInfo(Type fieldType)
        {
            var fieldInfo = _listIterator
                .GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.FieldType == fieldType);

            Assert.That(fieldInfo, Is.Not.EqualTo(null), $"Field of type {fieldType} not found.");

            return fieldInfo;
        }

        [Test]
        [TestCase(new object[] { })]
        [TestCase(new object[] { "1" })]
        [TestCase(new object[] { "1", "2", "3" })]
        [TestCase(new object[] { "3", "1", "2" })]
        [TestCase(new object[] { "Pesho", "Gosho", "Shosho" })]
        public void CtorShouldWorkCorrectly(params string[] elements)
        {
            _listIterator = CreateListIterator(elements);
            _elementsFieldInfo = GetFieldInfo(typeof(List<string>));

            var actualElements = _elementsFieldInfo.GetValue(_listIterator);
            var expectedElements = elements;
            Assert.That(actualElements, Is.EquivalentTo(expectedElements));

            _internalIndexFieldInfo = GetFieldInfo(typeof(int));
            var actualInternalIndex = _internalIndexFieldInfo.GetValue(_listIterator);
            var expectedInternalIndex = 0;
            Assert.That(actualInternalIndex, Is.EqualTo(expectedInternalIndex));
        }

        [Test]
        public void CtorShouldThrowException()
        {
            Assert.That(() => CreateListIterator(null), Throws.ArgumentException, "Should throw \"ArgumentException\"");
        }

        [Test]
        public void MoveShouldWorkCorrectly()
        {
            var elementsCount = 10;
            var elements = new string[elementsCount]
                .Select(el => "Pesho")
                .ToList();

            _listIterator = CreateListIterator();
            _elementsFieldInfo = GetFieldInfo(typeof(List<string>));
            _elementsFieldInfo.SetValue(_listIterator, elements);

            _internalIndexFieldInfo = GetFieldInfo(typeof(int));
            var settedIndex = 5;
            _internalIndexFieldInfo.SetValue(_listIterator, settedIndex);
            var actualInternalIndex = settedIndex;
            var expectedInternalIndex = settedIndex;

            for (int index = 0; index < elementsCount; index++)
            {
                if (HasNextElement(actualInternalIndex, elementsCount))
                {
                    Assert.That(() => _listIterator.Move(), Is.EqualTo(true));

                    actualInternalIndex = (int)_internalIndexFieldInfo.GetValue(_listIterator);
                    expectedInternalIndex++;
                    Assert.That(actualInternalIndex, Is.EqualTo(expectedInternalIndex));
                }
                else
                {
                    Assert.That(() => _listIterator.Move(), Is.EqualTo(false));

                    actualInternalIndex = (int)_internalIndexFieldInfo.GetValue(_listIterator);
                    Assert.That(actualInternalIndex, Is.EqualTo(expectedInternalIndex));
                }
            }
        }

        private bool HasNextElement(int index, int collectionCount) => index < collectionCount;

        [Test]
        public void HasNextShouldWorkCorrectly()
        {
            var elementsCount = 10;
            var elements = new string[elementsCount]
                .Select(el => "Pesho")
                .ToList();

            _listIterator = CreateListIterator();
            _elementsFieldInfo = GetFieldInfo(typeof(List<string>));
            _elementsFieldInfo.SetValue(_listIterator, elements);

            _internalIndexFieldInfo = GetFieldInfo(typeof(int));
            var settedIndex = 3;
            _internalIndexFieldInfo.SetValue(_listIterator, settedIndex);

            for (int index = 0; index < elementsCount; index++)
            {

                if (HasNextElement(settedIndex, elementsCount))
                {
                    Assert.That(() => _listIterator.HasNext(), Is.EqualTo(true));

                    settedIndex++;
                    _internalIndexFieldInfo.SetValue(_listIterator, settedIndex);
                }
                else
                {
                    Assert.That(() => _listIterator.HasNext(), Is.EqualTo(false));
                }
            }
        }

        [Test]
        [TestCase(new object[] { "1", "2", "3" })]
        [TestCase(new object[] { "3", "1" })]
        [TestCase(new object[] { "Pesho", "Gosho", "Shosho" })]
        public void PrintShouldWorkCorrectly(params string[] elements)
        {
            _listIterator = CreateListIterator();
            _elementsFieldInfo = GetFieldInfo(typeof(List<string>));
            _elementsFieldInfo.SetValue(_listIterator, elements.ToList());

            _internalIndexFieldInfo = GetFieldInfo(typeof(int));
            var settedIndex = elements.Length / 2;
            _internalIndexFieldInfo.SetValue(_listIterator, settedIndex);

            var actualElement = _listIterator.Print();
            var expectedElement = elements[settedIndex];
        
             Assert.That(actualElement, Is.EqualTo(expectedElement));
        }

        [Test]
        public void PrintShouldThrowExceptionWhenHasNoElements()
        {
            _listIterator = CreateListIterator();
            //_elementsFieldInfo = GetFieldInfo(typeof(List<string>));
            //_elementsFieldInfo.SetValue(_listIterator, new List<string>());


            Assert.That(() => _listIterator.Print(), Throws.InvalidOperationException);
        }
    }
}
