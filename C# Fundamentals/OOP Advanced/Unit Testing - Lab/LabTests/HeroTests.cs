using System;
using System.Collections.Generic;
using System.Text;

using Moq;

using NUnit.Framework;

namespace SkeletonTests
{
    public class HeroTests
    {
        private static Hero CreateHero()
        {
            var heroName = "Len";
            var weapon = new Mock<IWeapon>();
            return new Hero(heroName, weapon.Object);
        }

        [Test]
        public void HeroMustGainXpIfTargetIsDead()
        {
            var hero = CreateHero();

            var target = new Mock<ITarget>();
            target.Setup(t => t.IsDead()).Returns(true);
            const int targetExperience = 5;
            target.Setup(t => t.GiveExperience()).Returns(targetExperience);

            int expectedExperience = 0;
            for (int i = 0; i < 5; i++)
            {
                hero.Attack(target.Object);
                expectedExperience += targetExperience;
            }

            
            Assert.That(
                hero.Experience,
                Is.EqualTo(expectedExperience),
                "If target is dead, Hero must add to his XP target XP.");
        }

    }
}
