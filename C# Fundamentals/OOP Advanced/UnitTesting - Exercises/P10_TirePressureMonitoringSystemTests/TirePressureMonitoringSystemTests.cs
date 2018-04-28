using System;

using Moq;

using NUnit.Framework;

using P10_TirePressureMonitoringSystem;

namespace P10_TirePressureMonitoringSystemTests
{
    public class TirePressureMonitoringSystemTests
    {
        private const double LowPressureThreshold = 17;
        private const double HighPressureThreshold = 21;

        [Test]
        [TestCase(int.MinValue)]
        [TestCase(-2000)]
        [TestCase(-2)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(5)]
        [TestCase(15)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        [TestCase(21)]
        [TestCase(22)]
        [TestCase(220)]
        [TestCase(int.MaxValue)]
        public void AlarmShouldWorkCorrectly(int psiPressureValue)
        {
            var sensor = new Mock<ISensor>();
            sensor.Setup(s => s.PopNextPressurePsiValue()).Returns(psiPressureValue);

            var alarm = new Alarm(sensor.Object);
            alarm.Check();

            if (psiPressureValue < LowPressureThreshold || HighPressureThreshold < psiPressureValue)
            {
                Assert.That(alarm.AlarmOn, Is.EqualTo(true));
            }
            else
            {
                Assert.That(alarm.AlarmOn, Is.EqualTo(false));
            }
        }
    }
}
