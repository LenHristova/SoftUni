using System;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

using P09_DateTime;

namespace P09_DateTimeTests
{
    public class MyDateTimeTests
    {
        private MyDateTimeNow _daysAddableNow;

        private FieldInfo GetFieldInfo(string fieldName)
        {
            var fieldInfo = _daysAddableNow
                .GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.Name == fieldName);

            Assert.That(fieldInfo, Is.Not.EqualTo(null), $"Field whit name {fieldName} not found.");

            return fieldInfo;
        }

        [TestCase(2000, 1, 15, 1)]
        [TestCase(200, 5, 17, 3)]
        [TestCase(2020, 12, 15, 5)]
        public void AddDaysSHouldWorkCorrectlyWhenAddingDaysInTheMiddleOfTheMonth(int year, int month, int day, double daysToAdd)
        {
            _daysAddableNow = new MyDateTimeNow();
            var dateTimeNowField = GetFieldInfo("_now");

            dateTimeNowField.SetValue(_daysAddableNow, new DateTime(year, month, day));

            var actualDateTime = _daysAddableNow.AddDays(daysToAdd);
            var expectedDateTime = new DateTime(year, month, day + (int)daysToAdd);
            Assert.That(actualDateTime, Is.EqualTo(expectedDateTime));
        }

        [Test]
        public void AddDaysSHouldWorkCorrectlyWhenAddingDaysInNextMonth()
        {
            _daysAddableNow = new MyDateTimeNow();
            var dateTimeNowField = GetFieldInfo("_now");

            dateTimeNowField.SetValue(_daysAddableNow, new DateTime(2000, 1, 31));

            var actualDateTime = _daysAddableNow.AddDays(1);
            var expectedDateTime = new DateTime(2000, 2, 1);
            Assert.That(actualDateTime, Is.EqualTo(expectedDateTime));
        }

        [TestCase(2000, 1, 15, -1)]
        [TestCase(200, 5, 17, -3)]
        [TestCase(2020, 12, 15, -5)]
        public void AddDaysSHouldWorkCorrectlyWhenAddingNegativeDaysCountInTheMiddleOfTheMonth(int year, int month, int day, double daysToAdd)
        {
            _daysAddableNow = new MyDateTimeNow();
            var dateTimeNowField = GetFieldInfo("_now");

            dateTimeNowField.SetValue(_daysAddableNow, new DateTime(year, month, day));

            var actualDateTime = _daysAddableNow.AddDays(daysToAdd);
            var expectedDateTime = new DateTime(year, month, day + (int)daysToAdd);
            Assert.That(actualDateTime, Is.EqualTo(expectedDateTime));
        }

        [Test]
        public void AddDaysSHouldWorkCorrectlyWhenAddingNegativeDaysCountInPreviousMonth()
        {
            _daysAddableNow = new MyDateTimeNow();
            var dateTimeNowField = GetFieldInfo("_now");

            dateTimeNowField.SetValue(_daysAddableNow, new DateTime(2000, 5, 1));

            var actualDateTime = _daysAddableNow.AddDays(-1);
            var expectedDateTime = new DateTime(2000, 4, 30);
            Assert.That(actualDateTime, Is.EqualTo(expectedDateTime));
        }

        [Test]
        public void AddDaysSHouldWorkCorrectlyWhenAddingDaysInLeapYear()
        {
            _daysAddableNow = new MyDateTimeNow();
            var dateTimeNowField = GetFieldInfo("_now");

            dateTimeNowField.SetValue(_daysAddableNow, new DateTime(2008, 2, 28));

            var actualDateTime = _daysAddableNow.AddDays(1);
            var expectedDateTime = new DateTime(2008, 2, 29);
            Assert.That(actualDateTime, Is.EqualTo(expectedDateTime));
        }

        [Test]
        public void AddDaysSHouldWorkCorrectlyWhenAddingDaysInNonLeapYear()
        {
            _daysAddableNow = new MyDateTimeNow();
            var dateTimeNowField = GetFieldInfo("_now");

            dateTimeNowField.SetValue(_daysAddableNow, new DateTime(1900, 2, 28));

            var actualDateTime = _daysAddableNow.AddDays(1);
            var expectedDateTime = new DateTime(1900, 3, 1);
            Assert.That(actualDateTime, Is.EqualTo(expectedDateTime));
        }

        [Test]
        public void AddDaysSHouldWorkCorrectlyWhenAddingDaysInDateTimeMinValue()
        {
            _daysAddableNow = new MyDateTimeNow();
            var dateTimeNowField = GetFieldInfo("_now");
            dateTimeNowField.SetValue(_daysAddableNow, DateTime.MinValue);

            var actualDateTime = _daysAddableNow.AddDays(1);
            var expectedDateTime = new DateTime(1, 1, 2);

            Assert.That(actualDateTime, Is.EqualTo(expectedDateTime));
        }

        [Test]
        public void AddDaysSHouldThrowExceptionWhenAddingDaysInDateTimeMaxValue()
        {
            _daysAddableNow = new MyDateTimeNow();
            var dateTimeNowField = GetFieldInfo("_now");
            dateTimeNowField.SetValue(_daysAddableNow, DateTime.MaxValue);
            Assert.That(() => _daysAddableNow.AddDays(1), Throws.TypeOf(typeof(ArgumentOutOfRangeException)));
        }

        [Test]
        public void AddDaysSHouldWorkCorrectlyWhenSubstractDaysInDateTimeMaxValue()
        {
            _daysAddableNow = new MyDateTimeNow();
            var dateTimeNowField = GetFieldInfo("_now");
            dateTimeNowField.SetValue(_daysAddableNow, DateTime.MaxValue);

            var actualDateTime = _daysAddableNow.AddDays(-1);
            var expectedDateTime = new DateTime(9999, 12, 30);

            Assert.That(actualDateTime.Date, Is.EqualTo(expectedDateTime.Date));
        }

        [Test]
        public void AddDaysSHouldThrowExceptionWhenSubstractDaysInDateTimeMinValue()
        {
            _daysAddableNow = new MyDateTimeNow();
            var dateTimeNowField = GetFieldInfo("_now");
            dateTimeNowField.SetValue(_daysAddableNow, DateTime.MinValue);
            Assert.That(() => _daysAddableNow.AddDays(-1), Throws.TypeOf(typeof(ArgumentOutOfRangeException)));
        }

    }
}
