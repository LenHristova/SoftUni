using Moq;

using NUnit.Framework;

namespace SkeletonTests
{
    public class AxeTests
    {
        private static Axe CreateAxe(int attackPoints, int durabilityPoints)
        {
            return new Axe(attackPoints, durabilityPoints);
        }

        [Test]
        public void AxeMustLooseDurabilityAfterAttack()
        {
            const int attackPoints = 10;
            const int durabilityPoints = 5;
            var axe = CreateAxe(attackPoints, durabilityPoints);

            var target = new Mock<ITarget>();

            axe.Attack(target.Object);

            const int expectedDurabilityPoints = 4;
            Assert.That(
                axe.DurabilityPoints, 
                Is.EqualTo(expectedDurabilityPoints), 
                "Axe Durability doesn't change after attack.");
        }

        [Test]
        public void AxeMustBeBrokenWithZeroDurabilityPoints()
        {
            const int attackPoints = 10;
            const int durabilityPoints = 0;
            var axe = CreateAxe(attackPoints, durabilityPoints);

            var target = new Mock<ITarget>();

            Assert.That(
                () => axe.Attack(target.Object),
                Throws.InvalidOperationException.With.Message.EqualTo("Axe is broken."),
                "Axe is broken with 0 Durability Points - must be thrown \"InvalidOperationException\" with message: \"Axe is broken.\"");
        }
    }
}
