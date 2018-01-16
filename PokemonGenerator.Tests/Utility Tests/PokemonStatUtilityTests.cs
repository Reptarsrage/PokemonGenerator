using Moq;
using NUnit.Framework;
using PokemonGenerator.DAL;
using PokemonGenerator.Models;
using PokemonGenerator.Utilities;
using System.Linq;

namespace PokemonGenerator.Tests.Utility_Tests
{
    [TestFixture]
    public class PokemonStatUtilityTests
    {
        private PokemonStatUtility pokemonStatUtility;
        private Mock<IPokemonDA> pokemonDAMock;
        private Mock<IProbabilityUtility> probabilityUtilityMock;

        [SetUp]
        public void SetUp()
        {
            pokemonDAMock = new Mock<IPokemonDA>(MockBehavior.Strict);
            probabilityUtilityMock = new Mock<IProbabilityUtility>(MockBehavior.Strict);
            pokemonStatUtility = new PokemonStatUtility(pokemonDAMock.Object, probabilityUtilityMock.Object);
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

        [Test]
        public void AssignIVsAndEVsToTeamTest()
        {
            // Mock
            probabilityUtilityMock.Setup(m => m.GaussianRandom(0, 65535)).Returns(1);
            probabilityUtilityMock.Setup(m => m.GaussianRandom(0, 15)).Returns(1);
            var list = Enumerable.Range(1, 6).Select(i => new Pokemon
            {
                SpeciesId = (byte)i,
                OTName = i.ToString()
            });
            var team = new PokeList(list.Count())
            {
                Pokemon = list.ToArray(),
                Species = list.Select(p => p.SpeciesId).ToArray(),
                OTNames = list.Select(p => p.OTName).ToArray(),
            };

            // Run
            pokemonStatUtility = new PokemonStatUtility(pokemonDAMock.Object, probabilityUtilityMock.Object);
            pokemonStatUtility.AssignIVsAndEVsToTeam(team);

            // Assert
            Assert.NotNull(team);
            foreach (var poke in team.Pokemon)
            {
                Assert.GreaterOrEqual(poke.DefenseEV, 0, "DefenseEV");
                Assert.LessOrEqual(poke.DefenseEV, 65535, "DefenseEV");
                Assert.GreaterOrEqual(poke.AttackEV, 0, "AttackEV");
                Assert.LessOrEqual(poke.AttackEV, 65535, "AttackEV");
                Assert.GreaterOrEqual(poke.HitPointsEV, 0, "HitPointsEV");
                Assert.LessOrEqual(poke.HitPointsEV, 65535, "HitPointsEV");
                Assert.GreaterOrEqual(poke.SpecialEV, 0, "SpecialEV");
                Assert.LessOrEqual(poke.SpecialEV, 65535, "SpecialEV");
                Assert.GreaterOrEqual(poke.SpeedEV, 0, "SpeedEV");
                Assert.LessOrEqual(poke.SpeedEV, 65535, "SpeedEV");
            }
            foreach (var poke in team.Pokemon)
            {
                Assert.GreaterOrEqual(poke.DefenseIV, 0, "DefenseIV");
                Assert.LessOrEqual(poke.DefenseIV, 15, "DefenseIV");
                Assert.GreaterOrEqual(poke.AttackIV, 0, "AttackIV");
                Assert.LessOrEqual(poke.AttackIV, 15, "AttackIV");
                Assert.GreaterOrEqual(poke.SpecialIV, 0, "SpecialIV");
                Assert.LessOrEqual(poke.SpecialIV, 15, "SpecialIV");
                Assert.GreaterOrEqual(poke.SpeedIV, 0, "SpeedIV");
                Assert.LessOrEqual(poke.SpeedIV, 15, "SpeedIV");
            }

            // Verify
            pokemonDAMock.VerifyAll();
            probabilityUtilityMock.VerifyAll();
        }

        [Test]
        public void GetTeamBaseStatsTest()
        {
            // Mock
            var list = Enumerable.Range(1, 6).Select(i => new Pokemon
            {
                SpeciesId = (byte)i,
                OTName = i.ToString(),
            });
            var team = new PokeList(list.Count())
            {
                Pokemon = list.ToArray(),
                Species = list.Select(p => p.SpeciesId).ToArray(),
                OTNames = list.Select(p => p.OTName).ToArray(),
            };
            pokemonDAMock.Setup(m => m.GetTeamBaseStats(team)).Returns<PokeList>(pl => pl.Species.Select(i => new BaseStats
            {
                Id = i,
                Identifier = $"Test{i}",
                Types = "test",
                Hp = i,
                Attack = i + 1,
                Defense = i + 2,
                SpAttack = i + 3,
                SpDefense = i + 4,
                Speed = i + 5,
                GrowthRate = "test",
            }));

            // Run
            pokemonStatUtility = new PokemonStatUtility(pokemonDAMock.Object, probabilityUtilityMock.Object);
            pokemonStatUtility.GetTeamBaseStats(team, 100);

            // Assert
            Assert.NotNull(team);
            foreach (var poke in team.Pokemon)
            {
                Assert.AreEqual(poke.SpeciesId + 2, poke.Defense, "Defense");
                Assert.AreEqual(poke.SpeciesId + 1, poke.Attack, "Attack");
                Assert.AreEqual(poke.SpeciesId + 4, poke.SpDefense, "SpDefense");
                Assert.AreEqual(poke.SpeciesId + 3, poke.SpAttack, "SpAttack");
                Assert.AreEqual(poke.SpeciesId + 5, poke.Speed, "Speed");
            }

            // Verify
            pokemonDAMock.VerifyAll();
            probabilityUtilityMock.VerifyAll();
        }
    }
}