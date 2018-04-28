using System;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

using P01_Database;

namespace P01_DatabaseTests
{
    public class DatabaseTests
    {
        private Database<int> _database;

        private Database<int> CreateDatabase(params int[] values)
        {
            return new Database<int>(values);
        }

        private FieldInfo GetFieldInfo(Type fieldType)
        {
            var fieldInfo = _database.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(f => f.FieldType == fieldType);

            Assert.That(fieldInfo, Is.Not.EqualTo(null), $"Field of type {fieldType} not found.");

            return fieldInfo;
        }

        [TestCase(new[] { 1, 2, 3 })]
        [TestCase(new[] { 3, 2, 1 })]
        [TestCase(new[] { -3, 2, 1 })]
        [TestCase(new[] { int.MaxValue })]
        [TestCase(new[] { int.MinValue })]
        [TestCase(new int[] { })]
        [TestCase(null)]
        public void TestValidCtor(int[] values)
        {
            _database = CreateDatabase(values);

            var storingArrayFieldInfo = GetFieldInfo(typeof(int[]));
            var storingArrayValue = storingArrayFieldInfo.GetValue(_database);

            var maxCapacity = 16;
            var valuesLength = values?.Length ?? 0;
            int buffer = maxCapacity - valuesLength;
            var expectedStoringArrayValue = values?.Concat(new int[buffer]) ?? new int[maxCapacity];

            Assert.That(storingArrayValue, Is.EquivalentTo(expectedStoringArrayValue), "Stored data is not equivalent to expected data.");

            var currentIndexFieldInfo = GetFieldInfo(typeof(int));
            var currentIndexValue = currentIndexFieldInfo.GetValue(_database);
            var expectedCurrentIndexValue = valuesLength;

            Assert.That(currentIndexValue, Is.EqualTo(expectedCurrentIndexValue), "Current index value is wrong.");
        }

        [Test]
        public void TestCtorWithMoreThen16NumbersShouldThrowException()
        {
            var values = new int[17];

            Assert.That(() => CreateDatabase(values), Throws.InvalidOperationException, "Should throw \"InvalidOperationException\"");
        }

        [TestCase(new[] { 1, 2, 3 })]
        [TestCase(new[] { 3, 2, 1 })]
        [TestCase(new[] { -3, 2, 1 })]
        [TestCase(new[] { int.MaxValue })]
        [TestCase(new[] { int.MinValue })]
        public void AddShouldWorkCorrectly(int[] values)
        {
            _database = CreateDatabase();
            foreach (var value in values)
            {
                _database.Add(value);
            }

            var storingArrayFieldInfo = GetFieldInfo(typeof(int[]));
            var storingArrayValue = storingArrayFieldInfo.GetValue(_database);

            var maxCapacity = 16;
            int buffer = maxCapacity - values.Length;
            var expectedStoringArrayValue = values.Concat(new int[buffer]);

            Assert.That(storingArrayValue, Is.EquivalentTo(expectedStoringArrayValue), "Stored data is not equivalent to expected data.");

            var currentIndexFieldInfo = GetFieldInfo(typeof(int));
            var currentIndexValue = currentIndexFieldInfo.GetValue(_database);
            var expectedCurrentIndexValue = values.Length;

            Assert.That(currentIndexValue, Is.EqualTo(expectedCurrentIndexValue), "Current index value is wrong.");
        }

        [Test]
        public void AddMoreThen16NumbersShouldThrowException()
        {
            _database = CreateDatabase();
            var currentIndexFieldInfo = GetFieldInfo(typeof(int));
            var maxCapacity = 16;
            currentIndexFieldInfo.SetValue(_database, maxCapacity);

            Assert.That(() => _database.Add(1), Throws.InvalidOperationException, "Should throw \"InvalidOperationException\"");
        }

        [TestCase(new[] { 1, 2, 3 })]
        [TestCase(new[] { 3, 2, 1 })]
        [TestCase(new[] { -3, 2, 1 })]
        [TestCase(new[] { int.MaxValue })]
        [TestCase(new[] { int.MinValue })]
        public void RemoveShouldWorkCorrectly(int[] values)
        {
            _database = CreateDatabase();

            var maxCapacity = 16;
            int buffer = maxCapacity - values.Length;
            var dbValues = values.Concat(new int[buffer]).ToArray();

            var storingArrayFieldInfo = GetFieldInfo(typeof(int[]));
            storingArrayFieldInfo.SetValue(_database, dbValues);

            var currentIndexFieldInfo = GetFieldInfo(typeof(int));
            currentIndexFieldInfo.SetValue(_database, values.Length);

            dbValues[values.Length - 1] = 0;
            var expectedStoringArrayValue = dbValues;
            var expectedCurrentIndexValue = values.Length - 1;

            _database.Remove();

            var storingArrayValue = (int[])storingArrayFieldInfo.GetValue(_database);
            var currentIndexValue = currentIndexFieldInfo.GetValue(_database);
            Assert.That(storingArrayValue, Is.EquivalentTo(expectedStoringArrayValue), "Stored data is not equivalent to expected data.");
            Assert.That(currentIndexValue, Is.EqualTo(expectedCurrentIndexValue), "Current index value is wrong.");
        }

        [Test]
        public void RemoveFromEmptyDatabaseShouldThrowException()
        {
            _database = CreateDatabase();

            var currentIndexFieldInfo = GetFieldInfo(typeof(int));
            currentIndexFieldInfo.SetValue(_database, 0);

            Assert.That(() => _database.Remove(), Throws.InvalidOperationException, "Should throw \"InvalidOperationException\"");
        }

        [TestCase(new[] { 1, 2, 3 })]
        [TestCase(new[] { 3, 2, 1 })]
        [TestCase(new[] { -3, 2, 1 })]
        [TestCase(new[] { int.MaxValue })]
        [TestCase(new[] { int.MinValue })]
        public void FetchShouldReturnArray(int[] values)
        {
            _database = CreateDatabase();

            var maxCapacity = 16;
            int buffer = maxCapacity - values.Length;
            var dbValues = values.Concat(new int[buffer]).ToArray();

            var storingArrayFieldInfo = GetFieldInfo(typeof(int[]));
            storingArrayFieldInfo.SetValue(_database, dbValues);

            var currentIndexFieldInfo = GetFieldInfo(typeof(int));
            currentIndexFieldInfo.SetValue(_database, values.Length);

            var expectedArray = values;
            Assert.That(() => _database.Fetch(), Is.EquivalentTo(expectedArray), "Fetched data is not equivalent to expected data.");

            var actualReturnType = _database.Fetch().GetType();
            var expectedReturnType = typeof(int[]);
            Assert.That(actualReturnType, Is.EqualTo(expectedReturnType), "Return type does not match the expected type.");
        }
    }
}
