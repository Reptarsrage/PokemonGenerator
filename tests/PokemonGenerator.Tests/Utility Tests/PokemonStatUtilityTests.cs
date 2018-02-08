using Moq;
using PokemonGenerator.Models.DTO;
using PokemonGenerator.Models.Serialization;
using PokemonGenerator.Repositories;
using PokemonGenerator.Utilities;
using System.Linq;
using Xunit;

namespace PokemonGenerator.Tests.Unit.Utility_Tests
{
    public class PokemonStatUtilityTests
    {
        private PokemonStatUtility pokemonStatUtility;
        private Mock<IPokemonRepository> pokemonDAMock;
        private Mock<IProbabilityUtility> probabilityUtilityMock;

        public PokemonStatUtilityTests()
        {
            pokemonDAMock = new Mock<IPokemonRepository>(MockBehavior.Strict);
            probabilityUtilityMock = new Mock<IProbabilityUtility>(MockBehavior.Strict);
            pokemonStatUtility = new PokemonStatUtility(pokemonDAMock.Object, probabilityUtilityMock.Object);
        }

        [Theory]
        [InlineData(45, 15, 65535, 100, 294U)]
        [InlineData(45, 0, 0, 5, 19U)]
        [InlineData(45, 8, 33000, 50, 135U)]
        [InlineData(45, 15, 33000, 50, 142U)]
        [InlineData(45, 8, 65535, 50, 145U)]
        [InlineData(255, 8, 33000, 50, 345U)]
        [InlineData(35, 7, 22850, 81, 189U)]
        public void CalculateHitPointsTest(double baseHitPoints, double iv, double ev, double level, uint expected)
        {
            var hp = pokemonStatUtility.CalculateHitPoints(baseHitPoints, iv, ev, level);
            Assert.Equal(expected, hp);
        }

        [Theory]
        [InlineData(49, 15, 65535, 100, 197U)]
        [InlineData(49, 0, 0, 5, 9U)]
        [InlineData(49, 8, 33000, 50, 84U)]
        [InlineData(49, 15, 33000, 50, 91U)]
        [InlineData(49, 8, 65535, 50, 94U)]
        [InlineData(255, 8, 33000, 50, 290U)]
        [InlineData(90, 5, 24795, 81, 190U)]
        public void CalculateStatTest(double baseStat, double iv, double ev, double level, uint expected)
        {
            var stat = pokemonStatUtility.CalculateStat(baseStat, iv, ev, level);
            Assert.Equal(expected, stat);
        }

        [Theory]
        [InlineData("medium-slow", 100, 1059860U)]
        [InlineData("fast", 100, 800000U)]
        [InlineData("slow", 100, 1250000U)]
        [InlineData("medium", 100, 1000000U)]
        [InlineData("medium-fast", 100, 1000000U)]
        [InlineData("medium-slow", 5, 135U)]
        [InlineData("fast", 5, 100U)]
        [InlineData("slow", 5, 156U)]
        [InlineData("medium", 5, 125U)]
        [InlineData("medium-fast", 5, 125U)]
        public void CalculateExperiencePointsTest(string experienceGroup, int level, uint expected)
        {
            var xp = pokemonStatUtility.CalculateExperiencePoints(experienceGroup, level);
            Assert.Equal(expected, xp);
        }

        [Fact]
        public void AssignIVsAndEVsToTeamTest()
        {
            // Setup
            int level = 50;

            // Mock
            probabilityUtilityMock.Setup(m => m.GaussianRandomSkewed(0, 65535, level / 100D)).Returns(1);
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
            pokemonStatUtility.AssignIVsAndEVsToTeam(team, level);

            // Assert
            Assert.NotNull(team);
            foreach (var poke in team.Pokemon)
            {
                Assert.True(poke.DefenseEV >= 0, "DefenseEV >= 0");
                Assert.True(poke.DefenseEV <= 65535, "DefenseEV <= 65535");
                Assert.True(poke.AttackEV >= 0, "AttackEV >= 0");
                Assert.True(poke.AttackEV <= 65535, "AttackEV <= 65535");
                Assert.True(poke.HitPointsEV >= 0, "HitPointsEV >= 0");
                Assert.True(poke.HitPointsEV <= 65535, "HitPointsEV <= 65535");
                Assert.True(poke.SpecialEV >= 0, "SpecialEV >= 0");
                Assert.True(poke.SpecialEV <= 65535, "SpecialEV <= 65535");
                Assert.True(poke.SpeedEV >= 0, "SpeedEV >= 0");
                Assert.True(poke.SpeedEV <= 65535, "SpeedEV <= 65535");
            }
            foreach (var poke in team.Pokemon)
            {
                Assert.True(poke.DefenseIV >= 0, "DefenseIV >= 0");
                Assert.True(poke.DefenseIV <= 15, "DefenseIV <= 15");
                Assert.True(poke.AttackIV >= 0, "AttackIV >= 0");
                Assert.True(poke.AttackIV <= 15, "AttackIV <= 15");
                Assert.True(poke.SpecialIV >= 0, "SpecialIV >= 0");
                Assert.True(poke.SpecialIV <= 15, "SpecialIV <= 15");
                Assert.True(poke.SpeedIV >= 0, "SpeedIV >= 0");
                Assert.True(poke.SpeedIV <= 15, "SpeedIV <= 15");
            }

            // Verify
            pokemonDAMock.VerifyAll();
            probabilityUtilityMock.VerifyAll();
        }

        [Fact]
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
                Assert.Equal(poke.SpeciesId + 2, poke.Defense);
                Assert.Equal(poke.SpeciesId + 1, poke.Attack);
                Assert.Equal(poke.SpeciesId + 4, poke.SpDefense);
                Assert.Equal(poke.SpeciesId + 3, poke.SpAttack);
                Assert.Equal(poke.SpeciesId + 5, poke.Speed);
            }

            // Verify
            pokemonDAMock.VerifyAll();
            probabilityUtilityMock.VerifyAll();
        }
    }
}