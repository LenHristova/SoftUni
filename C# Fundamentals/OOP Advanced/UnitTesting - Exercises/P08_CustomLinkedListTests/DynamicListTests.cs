using System;
using System.Linq;
using System.Reflection;

using CustomLinkedList;
using NUnit.Framework;

namespace P08_CustomLinkedListTests
{
    public class DynamicListTests
    {
        private DynamicList<string> _dynamicList;

        private FieldInfo GetFieldInfo(string fieldName)
        {
            var fieldInfo = _dynamicList
                .GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.Name == fieldName);

            Assert.That(fieldInfo, Is.Not.EqualTo(null), $"Field whit name {fieldName} not found.");

            return fieldInfo;
        }

        [Test]
        public void ValidCtor()
        {
            _dynamicList = new DynamicList<string>();
            var headFieldInfo = GetFieldInfo("head");
            var headValue = headFieldInfo.GetValue(_dynamicList);
            var tailFieldInfo = GetFieldInfo("tail");
            var tailValue = tailFieldInfo.GetValue(_dynamicList);
            var countFieldInfo = GetFieldInfo("count");
            var countValue = countFieldInfo.GetValue(_dynamicList);

            Assert.That(headValue, Is.EqualTo(null));
            Assert.That(tailValue, Is.EqualTo(null));
            Assert.That(countValue, Is.EqualTo(0));
        }

        [Test]
        [TestCase(-1, 2, 3)]
        [TestCase(-10, 10, 15)]
        [TestCase(-100, 100, 100)]
        public void GetIndexerShouldThrowExceptionWithInvalidIndex(int negativeIndex, int listCount, int greaterIndex)
        {
            _dynamicList = new DynamicList<string>();
            var countFieldInfo = GetFieldInfo("count");
            countFieldInfo.SetValue(_dynamicList, listCount);

            Assert.That(() => _dynamicList[negativeIndex],
                Throws.TypeOf(typeof(ArgumentOutOfRangeException)),
                "Should throw \"ArgumentOutOfRangeException\"");

            Assert.That(() => _dynamicList[greaterIndex],
                Throws.TypeOf(typeof(ArgumentOutOfRangeException)),
                "Should throw \"ArgumentOutOfRangeException\"");

        }

        [Test]
        [TestCase(-1, 2, 3)]
        [TestCase(-10, 10, 15)]
        [TestCase(-100, 100, 100)]
        public void SetIndexerShouldThrowExceptionWithInvalidIndex(int negativeIndex, int listCount, int greaterIndex)
        {
            _dynamicList = new DynamicList<string>();
            var countFieldInfo = GetFieldInfo("count");
            countFieldInfo.SetValue(_dynamicList, listCount);

            Assert.That(() => _dynamicList[negativeIndex] = "",
                Throws.TypeOf(typeof(ArgumentOutOfRangeException)),
                "Should throw \"ArgumentOutOfRangeException\"");

            Assert.That(() => _dynamicList[negativeIndex] = "",
                Throws.TypeOf(typeof(ArgumentOutOfRangeException)),
                "Should throw \"ArgumentOutOfRangeException\"");

        }

        [Test]
        public void AddShouldWorkCorrectly()
        {
            _dynamicList = new DynamicList<string>();
            var countFieldInfo = GetFieldInfo("count");
            countFieldInfo.SetValue(_dynamicList, 0);

            for (int i = 0; i < 10; i++)
            {
                _dynamicList.Add(i.ToString());

                Assert.That(_dynamicList[i], Is.EqualTo(i.ToString()));

                var expectedCount = i + 1;
                var actualCount = countFieldInfo.GetValue(_dynamicList);

                Assert.That(actualCount, Is.EqualTo(expectedCount));
            }
        }

        [Test]
        [TestCase(-1, 2, 3)]
        [TestCase(-10, 10, 15)]
        [TestCase(-100, 100, 100)]
        public void RemoveAtShouldThrowExceptionWithInvalidIndex(int negativeIndex, int listCount, int greaterIndex)
        {
            _dynamicList = new DynamicList<string>();
            var countFieldInfo = GetFieldInfo("count");
            countFieldInfo.SetValue(_dynamicList, listCount);

            Assert.That(() => _dynamicList.RemoveAt(negativeIndex),
                Throws.TypeOf(typeof(ArgumentOutOfRangeException)),
                "Should throw \"ArgumentOutOfRangeException\"");

            Assert.That(() => _dynamicList.RemoveAt(greaterIndex),
                Throws.TypeOf(typeof(ArgumentOutOfRangeException)),
                "Should throw \"ArgumentOutOfRangeException\"");
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(10, 15)]
        [TestCase(14, 15)]
        public void RemoveAtShouldWorkCorrectlyWithValidIndex(int index, int listCount)
        {
            _dynamicList = new DynamicList<string>();

            for (int i = 0; i < listCount; i++)
            {
                _dynamicList.Add(i.ToString());
            }

            var countFieldInfo = GetFieldInfo("count");
            countFieldInfo.SetValue(_dynamicList, listCount);

            var actualRemovedElement = _dynamicList.RemoveAt(index);
            var expectedRemovedElement = index.ToString();

            Assert.That(actualRemovedElement, Is.EqualTo(expectedRemovedElement));


            var expectedCountAfterRemoving = listCount - 1;
            var actualCountAfterRemoving = countFieldInfo.GetValue(_dynamicList);

            Assert.That(actualCountAfterRemoving, Is.EqualTo(expectedCountAfterRemoving));
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(10, 15)]
        [TestCase(14, 15)]
        public void RemoveShouldWorkCorrectlyWithExistingElement(int index, int listCount)
        {
            _dynamicList = new DynamicList<string>();

            for (int i = 0; i < listCount; i++)
            {
                _dynamicList.Add(i.ToString());
            }

            var countFieldInfo = GetFieldInfo("count");
            countFieldInfo.SetValue(_dynamicList, listCount);

            var actualRemovedElementIndex = _dynamicList.Remove(index.ToString());
            var expectedRemovedElementIndex = index;

            Assert.That(actualRemovedElementIndex, Is.EqualTo(expectedRemovedElementIndex));

            var expectedCountAfterRemoving = listCount - 1;
            var actualCountAfterRemoving = countFieldInfo.GetValue(_dynamicList);

            Assert.That(actualCountAfterRemoving, Is.EqualTo(expectedCountAfterRemoving));
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(15)]
        public void RemoveShouldWorkCorrectlyWithNonExistingElement(int listCount)
        {
            _dynamicList = new DynamicList<string>();

            for (int i = 0; i < listCount; i++)
            {
                _dynamicList.Add(i.ToString());
            }

            var countFieldInfo = GetFieldInfo("count");
            countFieldInfo.SetValue(_dynamicList, listCount);

            var actualReturn = _dynamicList.Remove("Not exist");
            var expectedReturn = -1;

            Assert.That(actualReturn, Is.EqualTo(expectedReturn));

            var expectedCount = listCount;
            var actualCount = countFieldInfo.GetValue(_dynamicList);

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(10, 15)]
        [TestCase(14, 15)]
        public void IndexOfShouldWorkCorrectlyWithExistingElement(int index, int listCount)
        {
            _dynamicList = new DynamicList<string>();

            for (int i = 0; i < listCount; i++)
            {
                _dynamicList.Add(i.ToString());
            }

            var countFieldInfo = GetFieldInfo("count");
            countFieldInfo.SetValue(_dynamicList, listCount);

            var actualRemovedElementIndex = _dynamicList.IndexOf(index.ToString());
            var expectedRemovedElementIndex = index;

            Assert.That(actualRemovedElementIndex, Is.EqualTo(expectedRemovedElementIndex));

            var expectedCount = listCount;
            var actualCount = countFieldInfo.GetValue(_dynamicList);

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(15)]
        public void IndexOfShouldWorkCorrectlyWithNonExistingElement(int listCount)
        {
            _dynamicList = new DynamicList<string>();

            for (int i = 0; i < listCount; i++)
            {
                _dynamicList.Add(i.ToString());
            }

            var countFieldInfo = GetFieldInfo("count");
            countFieldInfo.SetValue(_dynamicList, listCount);

            var actualReturn = _dynamicList.IndexOf("Not exist");
            var expectedReturn = -1;

            Assert.That(actualReturn, Is.EqualTo(expectedReturn));

            var expectedCount = listCount;
            var actualCount = countFieldInfo.GetValue(_dynamicList);

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(10, 15)]
        [TestCase(14, 15)]
        public void ContainsShouldWorkCorrectlyWithExistingElement(int index, int listCount)
        {
            _dynamicList = new DynamicList<string>();

            for (int i = 0; i < listCount; i++)
            {
                _dynamicList.Add(i.ToString());
            }

            var countFieldInfo = GetFieldInfo("count");
            countFieldInfo.SetValue(_dynamicList, listCount);

            Assert.That(() => _dynamicList.Contains(index.ToString()), Is.EqualTo(true));

            var expectedCount = listCount;
            var actualCount = countFieldInfo.GetValue(_dynamicList);

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(15)]
        public void ContainsShouldWorkCorrectlyWithNonExistingElement(int listCount)
        {
            _dynamicList = new DynamicList<string>();

            for (int i = 0; i < listCount; i++)
            {
                _dynamicList.Add(i.ToString());
            }

            var countFieldInfo = GetFieldInfo("count");
            countFieldInfo.SetValue(_dynamicList, listCount);

            Assert.That(() => _dynamicList.Contains("Not exist"), Is.EqualTo(false));

            var expectedCount = listCount;
            var actualCount = countFieldInfo.GetValue(_dynamicList);

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
    }
}
