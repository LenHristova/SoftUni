using NUnit.Framework;

namespace SkeletonTests
{
    public class DummyTests
    {
        private static Dummy CreateDummy(int health, int experience)
        {
            return new Dummy(health, experience);
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void DummyMustLosesHealthWhenBeAttacked(int attackedPoints)
        {
            const int health = 10;
            const int experience = 5;
            var dummy = CreateDummy(health, experience);

            dummy.TakeAttack(attackedPoints);

            var expectedHealthPoints = health - attackedPoints;
            Assert.That(
                dummy.Health, 
                Is.EqualTo(expectedHealthPoints),
                "When be attacked, Dummy must lose health points, equal to attacked points.");
        }

        [Test]
        public void DeadDummyMustThrowExceptionWhenBeAttacked()
        {
            const int health = 0;
            const int experience = 5;
            var dummy = CreateDummy(health, experience);

            var attackedPoints = 1;

            Assert.That(
                () => dummy.TakeAttack(attackedPoints),
                Throws.InvalidOperationException.With.Message.EqualTo("Dummy is dead."),
                "Dead Dummy must throw \"InvalidOperationException\" with message: \"Dummy is dead.\"");
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void DeadDummyCanGiveXp(int experience)
        {
            const int health = 0;
            var dummy = CreateDummy(health, experience);

            var expectedGivenExperiencePoints = experience;
            Assert.That(
                () =>
                dummy.GiveExperience(), 
                Is.EqualTo(expectedGivenExperiencePoints),
                "Dead Dummy must can give experience points, equal to his experience points.");
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void AliveDummyCanNotGiveXp(int experience)
        {
            const int health = 10;
            var dummy = CreateDummy(health, experience);

            Assert.That(() =>
                dummy.GiveExperience(),
                Throws.InvalidOperationException.With.Message.EqualTo("Target is not dead."),
                "Alive Dummy can not give XP - must be thrown \"InvalidOperationException\" with message: \"Target is not dead.\"");
        }
    }
}
