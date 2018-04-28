using System;

using NUnit.Framework;

using P07_Hack;

namespace P07_HackTests
{
    public class ArithmeticTests
    {
        private static readonly float[] FloatValues =
        {
            float.MinValue,
            -0.2F,
            -5.2F,
            -1,
            1,
            0.0F,
            0.2F,
            5.2F,
            float.MaxValue
        };

        private static readonly int[] IntValues =
        {
            int.MinValue + 1,
            -20,
            -1,
            0,
            20,
            1,
            int.MaxValue
        };

        private static readonly double[] DoubleValues =
        {
            double.MinValue,
            -20.2,
            -500.2,
            -2000,
            2000,
            0.0,
            0.2,
            5500.2,
            double.MaxValue
        };

        private static readonly long[] LongValues =
        {
            long.MinValue + 1,
            -20,
            -2156151,
            0,
            20,
            2156151,
            long.MaxValue
        };

        private static readonly decimal[] DecimalValues =
        {
            decimal.MinValue,
            -0.2M,
            -5.2216587315648865498965489849M,
            -52216587315648865498965489849M,
            -3,
            0,
            3.2M,
            5.2216587315648865498965489849M,
            52216587315648865498965489849M,
            3,
            decimal.MaxValue
        };

        [Test]
        [TestCaseSource(nameof(FloatValues))]
        public void ReturnAbsValueShouldWorkCorrectlyWithFloats(float value)
        {
            var arithmetic = new Arithmetic();

            var actualValue = arithmetic.ReturnAbsValue(value);
            var expectedValue = value < 0 ? value * -1 : value;

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCaseSource(nameof(IntValues))]
        public void ReturnAbsValueShouldWorkCorrectlyWithIntegers(int value)
        {
            var arithmetic = new Arithmetic();

            var actualValue = arithmetic.ReturnAbsValue(value);
            var expectedValue = value < 0 ? value * -1 : value;

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCase(int.MinValue)]
        public void ReturnAbsValueShouldThrowExceptionWithIntegerMinValue(int value)
        {
            var arithmetic = new Arithmetic();

            Assert.That(() => arithmetic.ReturnAbsValue(value),
                Throws.TypeOf(typeof(OverflowException)));
        }

        [Test]
        [TestCaseSource(nameof(DoubleValues))]
        public void ReturnAbsValueShouldWorkCorrectlyWithDoubles(double value)
        {
            var arithmetic = new Arithmetic();

            var actualValue = arithmetic.ReturnAbsValue(value);
            var expectedValue = value < 0 ? value * -1 : value;

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCaseSource(nameof(LongValues))]
        public void ReturnAbsValueShouldWorkCorrectlyWithLongs(long value)
        {
            var arithmetic = new Arithmetic();

            var actualValue = arithmetic.ReturnAbsValue(value);
            var expectedValue = value < 0 ? value * -1 : value;

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCase(long.MinValue)]
        public void ReturnAbsValueShouldThrowExceptionWithLongMinValue(long value)
        {
            var arithmetic = new Arithmetic();

            Assert.That(() => arithmetic.ReturnAbsValue(value),
                Throws.TypeOf(typeof(OverflowException)));
        }

        [Test]
        [TestCaseSource(nameof(DecimalValues))]
        public void ReturnAbsValueShouldWorkCorrectlyWithDecimals(decimal value)
        {
            var arithmetic = new Arithmetic();

            var actualValue = arithmetic.ReturnAbsValue(value);
            var expectedValue = value < 0 ? value * -1 : value;

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCaseSource(nameof(DoubleValues))]
        public void ReturnFlooredValueShouldWorkCorrectlyWithDoubles(double value)
        {
            var arithmetic = new Arithmetic();

            var actualValue = arithmetic.ReturnFlooredValue(value);
            var expectedValue = (long)value;

            if (value < 0 && expectedValue - value != 0)
            {
                expectedValue--;
            }

            if (value == double.MinValue || value == double.MaxValue)
            {
                Assert.That(actualValue, Is.EqualTo(value));
            }
            else
            {
                Assert.That(actualValue, Is.EqualTo(expectedValue));
            }
        }

        [Test]
        [TestCaseSource(nameof(DecimalValues))]
        public void ReturnFlooredValueShouldWorkCorrectlyWithDecimals(decimal value)
        {
            var arithmetic = new Arithmetic();

            var actualValue = arithmetic.ReturnFlooredValue(value);
            var expectedValueStr = value.ToString();
            var digitsToTake = expectedValueStr.Contains(".") == false
                ? expectedValueStr.Length
                : expectedValueStr.IndexOf('.');

            var m = expectedValueStr.Substring(0, digitsToTake);
            var expectedValue = decimal.Parse(m);
            if (value < 0 && expectedValue - value != 0)
            {
                expectedValue--;
            }

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }
    }
}
