using NUnit.Framework;
using PokemonGenerator.Utilities;

namespace PokemonGenerator.Tests.Utility_Tests
{
    [TestFixture]
    public class PokemonStatUtilityTests
    {
        private IPokemonStatUtility pokemonStatUtility;

        [SetUp]
        public void SetUp()
        {
            pokemonStatUtility = new PokemonStatUtility();
        }

        [Test]
        [Category("Unit")]
        [TestCase(45, 15, 65535, 100, 294U)]
        [TestCase(45, 0, 0, 5, 19U)]
        [TestCase(45, 8, 33000, 50, 135U)]
        [TestCase(45, 15, 33000, 50, 142U)]
        [TestCase(45, 8, 65535, 50, 145U)]
        [TestCase(255, 8, 33000, 50, 345U)]
        [TestCase(35, 7, 22850, 81, 189U)]
        public void CalculateHitPointsTest(double baseHitPoints, double iv, double ev, double level, uint expected)
        {
            var hp = pokemonStatUtility.CalculateHitPoints(baseHitPoints, iv, ev, level);
            Assert.AreEqual(expected, hp);
        }

        [Test]
        [Category("Unit")]
        [TestCase(49, 15, 65535, 100, 197U)]
        [TestCase(49, 0, 0, 5, 9U)]
        [TestCase(49, 8, 33000, 50, 84U)]
        [TestCase(49, 15, 33000, 50, 91U)]
        [TestCase(49, 8, 65535, 50, 94U)]
        [TestCase(255, 8, 33000, 50, 290U)]
        [TestCase(90, 5, 24795, 81, 190U)]
        public void CalculateStatTest(double baseStat, double iv, double ev, double level, uint expected)
        {
            var stat = pokemonStatUtility.CalculateStat(baseStat, iv, ev, level);
            Assert.AreEqual(expected, stat);
        }

        [Test]
        [Category("Unit")]
        [TestCase("medium-slow", 100, 1059860U)]
        [TestCase("fast", 100, 800000U)]
        [TestCase("slow", 100, 1250000U)]
        [TestCase("medium", 100, 1000000U)]
        [TestCase("medium-fast", 100, 1000000U)]
        [TestCase("medium-slow", 5, 135U)]
        [TestCase("fast", 5, 100U)]
        [TestCase("slow", 5, 156U)]
        [TestCase("medium", 5, 125U)]
        [TestCase("medium-fast", 5, 125U)]
        public void CalculateExperiencePointsTest(string experienceGroup, int level, uint expected)
        {
            var xp = pokemonStatUtility.CalculateExperiencePoints(experienceGroup, level);
            Assert.AreEqual(expected, xp);
        }
    }
}