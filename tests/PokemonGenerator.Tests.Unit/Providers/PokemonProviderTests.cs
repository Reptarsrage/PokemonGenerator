using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Moq;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Models.Gernerator;
using PokemonGenerator.Providers;
using PokemonGenerator.Repositories;
using PokemonGenerator.Utilities;
using Xunit;

namespace PokemonGenerator.Tests.Unit.Providers
{
    public class PokemonProviderTests
    {
        private readonly IOptions<PersistentConfig> _config;
        private readonly Mock<IProbabilityUtility> _mockProbabilityUtility;
        private readonly Mock<IPokemonRepository> _mockPokemonRepository;

        public PokemonProviderTests()
        {
            var config = new PersistentConfig
            {
                Configuration = new GeneratorConfig
                {
                    LegendaryPokemon = new List<int>(),
                    DisabledPokemon = new List<int>(),
                    SpecialPokemon = new List<int>(),
                    PairedMoves = new Dictionary<int, int[]>(),
                    DependantMoves = new Dictionary<int, int[]>(),
                    HMBank = new List<int>(),
                    TeamSize = 6,
                    MoveEffectFilters = new Dictionary<string, double>(),
                    PokemonLiklihood = new PokemonLiklihood(),
                    Mean = 0.5D,
                    Skew = 0.3D,
                    StandardDeviation = 0.1D,
                    SameTypeModifier = 1.5D,
                    DamageModifier = 200D,
                    DamageTypeModifier = Likeliness.Low,
                    AlreadyPickedMoveEffectsModifier = Likeliness.Medium_Low,
                    AlreadyPickedMoveModifier = Likeliness.Extremely_Low,
                    PairedModifier = 2D,
                    DamageTypeDelta = 15,
                    RandomMoveMinPower = 40,
                    RandomMoveMaxPower = 100,
                    AllowDuplicates = false,
                    RandomBagMinCount = 10
                }
            };
            _config = Options.Create(config);
            _mockProbabilityUtility = new Mock<IProbabilityUtility>(MockBehavior.Strict);
            _mockPokemonRepository = new Mock<IPokemonRepository>(MockBehavior.Strict);
        }

        [Fact]
        public void GeneratePokemonTest()
        {
            // SetUP
            var level = 100;
            var low = 1;
            var high = 10;

            // Mock
            _mockPokemonRepository.Setup(m => m.GetPossiblePokemon(It.IsAny<int>())).Returns(Enumerable.Range(low, high));
            _mockPokemonRepository.Setup(m => m.GetRandomBagPokemon(It.IsAny<int>())).Returns(Enumerable.Range(low, high));

            // Generate
            var provider = new PokemonProvider(_mockProbabilityUtility.Object, _mockPokemonRepository.Object, _config);
            var poke = provider.GeneratePokemon(1, level);

            // Assert
            Assert.NotNull(poke);
            Assert.InRange(poke.SpeciesId, low, high);

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Fact]
        public void GeneratePokemonNotPossibleTest()
        {
            // SetUP
            var level = 100;
            var low = 1;
            var high = 10;

            // Mock
            _mockPokemonRepository.Setup(m => m.GetPossiblePokemon(It.IsAny<int>())).Returns(Enumerable.Range(low, high));
            _mockPokemonRepository.Setup(m => m.GetRandomBagPokemon(It.IsAny<int>())).Returns(Enumerable.Range(low, high));

            // Generate
            var provider = new PokemonProvider(_mockProbabilityUtility.Object, _mockPokemonRepository.Object, _config);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => provider.GeneratePokemon(high + 1, level));

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Fact]
        public void GeneratePokemonReducesRandomBagTest()
        {
            // SetUP
            var level = 100;
            var low = 1;
            var high = 10;
            _config.Value.Configuration.RandomBagMinCount = 0;

            // Mock
            _mockPokemonRepository.Setup(m => m.GetPossiblePokemon(It.IsAny<int>())).Returns(Enumerable.Range(low, high));
            _mockPokemonRepository.Setup(m => m.GetRandomBagPokemon(It.IsAny<int>())).Returns(Enumerable.Range(low, high));
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsNotNull<IList<IChoice>>()))
                .Returns<IList<IChoice>>(l => l
                    .Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Generate
            var provider = new PokemonProvider(_mockProbabilityUtility.Object, _mockPokemonRepository.Object, _config);
            var chosen = new List<int>();
            for (var i = low; i < high / 2; i++)
            {
                var poke = provider.GeneratePokemon(i, level);
                Assert.NotNull(poke);
                Assert.InRange(poke.SpeciesId, low, high);
                chosen.Add(poke.SpeciesId);
            }

            // Generate Random
            for (var i =  high / 2; i < high; i++)
            {
                var poke = provider.GenerateRandomPokemon(level);
                Assert.NotNull(poke);
                Assert.InRange(poke.SpeciesId, low, high);
                Assert.DoesNotContain(poke.SpeciesId, chosen);
            }

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
            _mockPokemonRepository.Verify(m => m.GetRandomBagPokemon(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void GeneratePokemonReducesRandomBagRefillsTest()
        {
            // SetUP
            var level = 100;
            var low = 1;
            var high = 10;
            _config.Value.Configuration.RandomBagMinCount = 0;

            // Mock
            _mockPokemonRepository.Setup(m => m.GetPossiblePokemon(It.IsAny<int>())).Returns(Enumerable.Range(low, high));
            _mockPokemonRepository.Setup(m => m.GetRandomBagPokemon(It.IsAny<int>())).Returns(Enumerable.Range(low, high));
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsNotNull<IList<IChoice>>()))
                .Returns<IList<IChoice>>(l => l
                    .Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Generate
            var provider = new PokemonProvider(_mockProbabilityUtility.Object, _mockPokemonRepository.Object, _config);
            var chosen = new List<int>();
            for (var i = low; i < high / 2; i++)
            {
                var poke = provider.GeneratePokemon(i, level);
                Assert.NotNull(poke);
                Assert.InRange(poke.SpeciesId, low, high);
                chosen.Add(poke.SpeciesId);
            }

            // Generate Random
            for (var i =  high / 2; i < high * 2; i++)
            {
                var poke = provider.GenerateRandomPokemon(level);
                Assert.NotNull(poke);
                Assert.InRange(poke.SpeciesId, low, high);
            }

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
            _mockPokemonRepository.Verify(m => m.GetRandomBagPokemon(It.IsAny<int>()), Times.Exactly(2));
        }

        [Theory]
        [InlineData(252)]
        [InlineData(0)]
        [InlineData(-1)]
        public void GeneratePokemonBadSpeciesTest(int speciesId)
        {
            // Assert
            var provider = new PokemonProvider(_mockProbabilityUtility.Object, _mockPokemonRepository.Object, _config);
            Assert.Throws<ArgumentOutOfRangeException>(() => provider.GeneratePokemon(speciesId, 50));

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Theory]
        [InlineData(101)]
        [InlineData(0)]
        [InlineData(-1)]
        public void GeneratePokemonBadLevelTest(int level)
        {
            // Assert
            var provider = new PokemonProvider(_mockProbabilityUtility.Object, _mockPokemonRepository.Object, _config);
            Assert.Throws<ArgumentOutOfRangeException>(() => provider.GeneratePokemon(1, level));

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Theory]
        [InlineData(101)]
        [InlineData(0)]
        [InlineData(-1)]
        public void GenerateRandomPokemonBadLevelTest(int level)
        {
            // Assert
            var provider = new PokemonProvider(_mockProbabilityUtility.Object, _mockPokemonRepository.Object, _config);
            Assert.Throws<ArgumentOutOfRangeException>(() => provider.GenerateRandomPokemon(level));

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }


        [Fact]
        public void GenerateRandomPokemonTest()
        {
            // SetUP
            var level = 100;
            var low = 1;
            var high = 10;

            // Mock
            _mockPokemonRepository.Setup(m => m.GetRandomBagPokemon(level)).Returns(Enumerable.Range(low, high));
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsNotNull<IList<IChoice>>()))
                .Returns<IList<IChoice>>(l => l
                  .Select((choice, index) => new { choice, index })
                  .OrderByDescending(item => item.choice.Probability)
                  .First().index);

            // Generate
            var provider = new PokemonProvider(_mockProbabilityUtility.Object, _mockPokemonRepository.Object, _config);
            var poke = provider.GenerateRandomPokemon(level);

            // Assert
            Assert.NotNull(poke);
            Assert.InRange(poke.SpeciesId, low, high);

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }


        [Fact]
        public void GenerateRandomPokemonMultipleReducesBagTest()
        {
            // SetUP
            var level = 100;
            var low = 1;
            var high = 10;
            _config.Value.Configuration.RandomBagMinCount = 0;

            // Mock
            _mockPokemonRepository.Setup(m => m.GetRandomBagPokemon(level)).Returns(Enumerable.Range(low, high));
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsNotNull<IList<IChoice>>()))
                .Returns<IList<IChoice>>(l => l
                    .Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Generate
            var chosen = new List<int>();
            var provider = new PokemonProvider(_mockProbabilityUtility.Object, _mockPokemonRepository.Object, _config);
            for (var i = low; i < high; i++)
            {
                var poke = provider.GenerateRandomPokemon(level);
                Assert.NotNull(poke);
                Assert.InRange(poke.SpeciesId, low, high);
                Assert.DoesNotContain(poke.SpeciesId, chosen);
                chosen.Add(poke.SpeciesId);
            }

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
            _mockPokemonRepository.Verify(m => m.GetRandomBagPokemon(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void GenerateRandomPokemonMultipleReducesBagRefillsTest()
        {
            // SetUP
            var level = 100;
            var low = 1;
            var high = 10;
            _config.Value.Configuration.RandomBagMinCount = 0;

            // Mock
            _mockPokemonRepository.Setup(m => m.GetRandomBagPokemon(level)).Returns(Enumerable.Range(low, high));
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsNotNull<IList<IChoice>>()))
                .Returns<IList<IChoice>>(l => l
                    .Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Generate
            var provider = new PokemonProvider(_mockProbabilityUtility.Object, _mockPokemonRepository.Object, _config);
            for (var i = low; i < high * 2; i++)
            {
                var poke = provider.GenerateRandomPokemon(level);
                Assert.NotNull(poke);
                Assert.InRange(poke.SpeciesId, low, high);
            }

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
            _mockPokemonRepository.Verify(m => m.GetRandomBagPokemon(It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public void GenerateRandomPokemonMultipleAllowDuplicatesTest()
        {
            // SetUP
            var level = 100;
            var low = 1;
            var high = 10;
            _config.Value.Configuration.RandomBagMinCount = 0;

            // Mock
            _mockPokemonRepository.Setup(m => m.GetRandomBagPokemon(level)).Returns(Enumerable.Range(low, high));
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsNotNull<IList<IChoice>>()))
                .Returns<IList<IChoice>>(l => l
                    .Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Generate
            var provider = new PokemonProvider(_mockProbabilityUtility.Object, _mockPokemonRepository.Object, _config);
            for (var i = low; i < high * 2; i++)
            {
                provider.ReloadRandomBag(level);
                var poke = provider.GenerateRandomPokemon(level);
                Assert.NotNull(poke);
                Assert.InRange(poke.SpeciesId, low, high);
            }

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }
    }
}