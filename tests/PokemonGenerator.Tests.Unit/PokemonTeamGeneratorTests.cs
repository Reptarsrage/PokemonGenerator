using Microsoft.Extensions.Options;
using Moq;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Models.Gernerator;
using PokemonGenerator.Providers;
using PokemonGenerator.Repositories;
using PokemonGenerator.Utilities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokemonGenerator.Tests.Unit
{
    public class PokemonTeamGeneratorTests
    {
        private IOptions<PersistentConfig> _config;
        private Mock<IProbabilityUtility> _mockProbabilityUtility;
        private Mock<IPokemonRepository> _mockPokemonRepository;

        public PokemonTeamGeneratorTests()
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
                    StandardDeviation = 0.1D,
                    SameTypeModifier = 1D,
                    DamageModifier = 1D,
                    PairedModifier = 1D,
                    DamageTypeDelta = 15,
                    RandomMoveMinPower = 40,
                    RandomMoveMaxPower = 100
                }
            };
            _config = Options.Create(config);
            _mockProbabilityUtility = new Mock<IProbabilityUtility>(MockBehavior.Strict);
            _mockPokemonRepository = new Mock<IPokemonRepository>(MockBehavior.Strict);
        }

        [Fact]
        public void GenerateRandomPokemonTeamTest()
        {
            // SetUP
            var level = 100;

            // Mock
            _mockPokemonRepository.Setup(m => m.GetRandomBagPokemon(level)).Returns(Enumerable.Range(1, 100));
            _mockPokemonRepository.Setup(m => m.GetPossiblePokemon(level)).Returns(Enumerable.Range(1, 100));
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
            Assert.True(poke.SpeciesId > 0);

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }
    }
}