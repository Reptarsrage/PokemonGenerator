using Microsoft.Extensions.Options;
using Moq;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Models.DTO;
using PokemonGenerator.Models.Enumerations;
using PokemonGenerator.Models.Gernerator;
using PokemonGenerator.Models.Serialization;
using PokemonGenerator.Providers;
using PokemonGenerator.Repositories;
using PokemonGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokemonGenerator.Tests.Unit.Providers
{
    public class PokemonMoveProviderTests
    {
        private readonly IOptions<PersistentConfig> _config;
        private readonly Mock<IProbabilityUtility> _mockProbabilityUtility;
        private readonly Mock<IPokemonRepository> _mockPokemonRepository;
        private readonly Random _random;
        private readonly int _level;

        public PokemonMoveProviderTests()
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
            _level = 100;
            _random = new Random("The cake is a lie".GetHashCode());
            _config = Options.Create(config);
            _mockProbabilityUtility = new Mock<IProbabilityUtility>(MockBehavior.Strict);
            _mockPokemonRepository = new Mock<IPokemonRepository>(MockBehavior.Strict);
        }

        [Fact]
        public void AssignMovesWeighsSimilarEffects()
        {
             // Set up
            var poke = new Pokemon();
            var movesForPokemon = new List<PokemonMoveSetResult>
            {
                new PokemonMoveSetResult { MoveId = 1, Effect = "test1"}, // should always be picked first
                new PokemonMoveSetResult { MoveId = 2, Effect = "test1" },
                new PokemonMoveSetResult { MoveId = 3, Effect = "test1" },
                new PokemonMoveSetResult { MoveId = 4, Effect = "test2" },
                new PokemonMoveSetResult { MoveId = 5, Effect = "test3" },
                new PokemonMoveSetResult { MoveId = 6, Effect = "test4" }
            };

            // Mock
            _mockPokemonRepository.Setup(r => r.GetTMs()).Returns(new List<int>(0));
            _mockPokemonRepository.Setup(r => r.GetMovesForPokemon(poke.SpeciesId, _level)).Returns(movesForPokemon);
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsAny<IList<IChoice>>())).Returns((IList<IChoice> choices) =>
                choices.Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Run
            var target = new PokemonMoveProvider(_mockPokemonRepository.Object, _mockProbabilityUtility.Object, _config, _random);
            poke = target.AssignMoves(poke, _level);

            // Assert
            var expectedMoves = new List<int> { 1, 4, 5, 6 }; // too complicated to use logic here
            Assert.Contains(poke.MoveIndex1, expectedMoves);
            Assert.Contains(poke.MoveIndex2, expectedMoves);
            Assert.Contains(poke.MoveIndex3, expectedMoves);
            Assert.Contains(poke.MoveIndex4, expectedMoves);

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }


        [Fact]
        public void AssignMovesDependant()
        {
            // Set up
            var poke = new Pokemon();
            var movesForPokemon = new List<PokemonMoveSetResult>
            {
                new PokemonMoveSetResult { MoveId = 1 }, // should always be picked first
                new PokemonMoveSetResult { MoveId = 2 },
                new PokemonMoveSetResult { MoveId = 3 },
                new PokemonMoveSetResult { MoveId = 4 },
                new PokemonMoveSetResult { MoveId = 5 },
                new PokemonMoveSetResult { MoveId = 6 }
            };
            _config.Value.Configuration.DependantMoves = new Dictionary<int, int[]>
            {
                [1] = new [] { 2, 3 },
                [4] = new [] { 6 },
                [6] = new [] { 4 }
            };

            // Mock
            _mockPokemonRepository.Setup(r => r.GetTMs()).Returns(new List<int>(0));
            _mockPokemonRepository.Setup(r => r.GetMovesForPokemon(poke.SpeciesId, _level)).Returns(movesForPokemon);
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsAny<IList<IChoice>>())).Returns((IList<IChoice> choices) =>
                choices.Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Run
            var target = new PokemonMoveProvider(_mockPokemonRepository.Object, _mockProbabilityUtility.Object, _config, _random);
            poke = target.AssignMoves(poke, _level);

            // Assert
            var expectedMoves = new List<int> { 1, 2, 3, 5 }; // too complicated to use logic here
            Assert.Contains(poke.MoveIndex1, expectedMoves);
            Assert.Contains(poke.MoveIndex2, expectedMoves);
            Assert.Contains(poke.MoveIndex3, expectedMoves);
            Assert.Contains(poke.MoveIndex4, expectedMoves);

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Fact]
        public void AssignMovesPaired()
        {
            // Set up
            var poke = new Pokemon();
            var movesForPokemon = new List<PokemonMoveSetResult>
            {
                new PokemonMoveSetResult { MoveId = 1 }, // should always be picked first
                new PokemonMoveSetResult { MoveId = 2 },
                new PokemonMoveSetResult { MoveId = 3 },
                new PokemonMoveSetResult { MoveId = 4 },
                new PokemonMoveSetResult { MoveId = 5 },
                new PokemonMoveSetResult { MoveId = 6 }
            };
            _config.Value.Configuration.PairedMoves = new Dictionary<int, int[]>
            {
                [1] = new [] { 2, 3 },
                [2] = new [] { 5 }
            };

            // Mock
            _mockPokemonRepository.Setup(r => r.GetTMs()).Returns(new List<int>(0));
            _mockPokemonRepository.Setup(r => r.GetMovesForPokemon(poke.SpeciesId, _level)).Returns(movesForPokemon);
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsAny<IList<IChoice>>())).Returns((IList<IChoice> choices) =>
                choices.Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Run
            var target = new PokemonMoveProvider(_mockPokemonRepository.Object, _mockProbabilityUtility.Object, _config, _random);
            poke = target.AssignMoves(poke, _level);

            // Assert
            var pairedKeys = _config.Value.Configuration.PairedMoves.Keys.ToList();
            var expectedMoves = pairedKeys.Union(pairedKeys.SelectMany(key => _config.Value.Configuration.PairedMoves[key])).ToList();
            Assert.Contains(poke.MoveIndex1, expectedMoves);
            Assert.Contains(poke.MoveIndex2, expectedMoves);
            Assert.Contains(poke.MoveIndex3, expectedMoves);
            Assert.Contains(poke.MoveIndex4, expectedMoves);

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Fact]
        public void AssignMovesWeighsDamage()
        {
            // Set up
            var poke = new Pokemon();
            var movesForPokemon = new List<PokemonMoveSetResult>
            {
                new PokemonMoveSetResult { MoveId = 1, Power = 100 }, // this is epected
                new PokemonMoveSetResult { MoveId = 2, Power = 80 },
                new PokemonMoveSetResult { MoveId = 3, Power = 90 },  // this is epected
                new PokemonMoveSetResult { MoveId = 4, Power = 70 },
                new PokemonMoveSetResult { MoveId = 5, Power = 0 },   // this will be chosen bc of damage flag toggling
                new PokemonMoveSetResult { MoveId = 6, Power = 0 }    // this will be chosen bc of damage flag toggling
            };

            // Mock
            _mockPokemonRepository.Setup(r => r.GetTMs()).Returns(new List<int>(0));
            _mockPokemonRepository.Setup(r => r.GetMovesForPokemon(poke.SpeciesId, _level)).Returns(movesForPokemon);
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsAny<IList<IChoice>>())).Returns((IList<IChoice> choices) =>
                choices.Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Run
            var target = new PokemonMoveProvider(_mockPokemonRepository.Object, _mockProbabilityUtility.Object, _config, _random);
            poke = target.AssignMoves(poke, _level);

            // Assert
            var highestDamagingMoves = movesForPokemon.OrderByDescending(move => move.Power).Take(2).ToList();
            var nonDamagingMoves = movesForPokemon.OrderBy(move => move.Power).Take(2).ToList();
            var expectedMoves = highestDamagingMoves.Union(nonDamagingMoves).Select(move => move.MoveId).ToList();
            Assert.Contains(poke.MoveIndex1, expectedMoves);
            Assert.Contains(poke.MoveIndex2, expectedMoves);
            Assert.Contains(poke.MoveIndex3, expectedMoves);
            Assert.Contains(poke.MoveIndex4, expectedMoves);

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Fact]
        public void AssignMovesWeighsTypes()
        {
            // Set up
            var poke = new Pokemon
            {
                Types = new List<string> { "test2", "test5" }
            };
            var attackTypesToFavor = new List<string> { "test1" };
            var movesForPokemon = new List<PokemonMoveSetResult>
            {
                new PokemonMoveSetResult { MoveId = 1, Type = "test1" },
                new PokemonMoveSetResult { MoveId = 2, Type = "test2" },
                new PokemonMoveSetResult { MoveId = 3, Type = "test3" },
                new PokemonMoveSetResult { MoveId = 4, Type = "test4" },
                new PokemonMoveSetResult { MoveId = 5, Type = "test5" },
                new PokemonMoveSetResult { MoveId = 6, Type = "test1" },
            };

            // Mock
            _mockPokemonRepository.Setup(r => r.GetTMs()).Returns(new List<int>(0));
            _mockPokemonRepository.Setup(r => r.GetMovesForPokemon(poke.SpeciesId, _level)).Returns(movesForPokemon);
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Join(",", poke.Types))).Returns(new List<string>());
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(attackTypesToFavor);
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsAny<IList<IChoice>>())).Returns((IList<IChoice> choices) =>
                choices.Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Run
            var target = new PokemonMoveProvider(_mockPokemonRepository.Object, _mockProbabilityUtility.Object, _config, _random);
            poke = target.AssignMoves(poke, _level);

            // Assert
            var expectedTypes = poke.Types.Union(attackTypesToFavor).ToList();
            var expectedMoves = movesForPokemon.Join(expectedTypes, move => move.Type, type => type, (move, type) => move.MoveId).ToList();
            Assert.Contains(poke.MoveIndex1, expectedMoves);
            Assert.Contains(poke.MoveIndex2, expectedMoves);
            Assert.Contains(poke.MoveIndex3, expectedMoves);
            Assert.Contains(poke.MoveIndex4, expectedMoves);

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Fact]
        public void AssignMovesWeighsEffectFilters()
        {
            // Set up
            var poke = new Pokemon();
            var movesForPokemon = new List<PokemonMoveSetResult>
            {
                new PokemonMoveSetResult { MoveId = 1, Effect = "test1" },
                new PokemonMoveSetResult { MoveId = 2, Effect = "test2" },
                new PokemonMoveSetResult { MoveId = 3, Effect = "test3" },
                new PokemonMoveSetResult { MoveId = 4, Effect = "test4" },
                new PokemonMoveSetResult { MoveId = 5, Effect = "test5" },
                new PokemonMoveSetResult { MoveId = 6, Effect = "test1" },
            };
            _config.Value.Configuration.MoveEffectFilters = new Dictionary<string, double>
            {
                ["test1"] = 1,
                ["test2"] = 0.2,
                ["test3"] = .1,
                ["test4"] = 0,
                ["test5"] = 0.5,
            };

            // Mock
            _mockPokemonRepository.Setup(r => r.GetTMs()).Returns(new List<int>(0));
            _mockPokemonRepository.Setup(r => r.GetMovesForPokemon(poke.SpeciesId, _level)).Returns(movesForPokemon);
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsAny<IList<IChoice>>())).Returns((IList<IChoice> choices) =>
                choices.Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Run
            var target = new PokemonMoveProvider(_mockPokemonRepository.Object, _mockProbabilityUtility.Object, _config, _random);
            poke = target.AssignMoves(poke, _level);

            // Assert
            var expectedEffects = _config.Value.Configuration.MoveEffectFilters.OrderByDescending(pair => pair.Value).Take(4).Select(pair => pair.Key).ToList();
            var expectedMoves = movesForPokemon.Join(expectedEffects, move => move.Effect, effect => effect, (move, effect) => move.MoveId).ToList();
            Assert.Contains(poke.MoveIndex1, expectedMoves);
            Assert.Contains(poke.MoveIndex2, expectedMoves);
            Assert.Contains(poke.MoveIndex3, expectedMoves);
            Assert.Contains(poke.MoveIndex4, expectedMoves);

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Fact]
        public void AssignMovesDamageFlagForced()
        {
            // Set up
            var poke = new Pokemon();
            var movesForPokemon = new List<PokemonMoveSetResult>
            {
                new PokemonMoveSetResult { MoveId = 1, Power = 40 },
                new PokemonMoveSetResult { MoveId = 2, Power = 90 },
                new PokemonMoveSetResult { MoveId = 3, Power = 130 },
                new PokemonMoveSetResult { MoveId = 4, Power = 25 },
                new PokemonMoveSetResult { MoveId = 5, Power = 12 },
                new PokemonMoveSetResult { MoveId = 6, Power = 0  },
            };

            // Mock
            _mockPokemonRepository.Setup(r => r.GetTMs()).Returns(new List<int>(0));
            _mockPokemonRepository.Setup(r => r.GetMovesForPokemon(poke.SpeciesId, _level)).Returns(movesForPokemon);
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsAny<IList<IChoice>>())).Returns((IList<IChoice> choices) =>
                choices.Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Run
            var target = new PokemonMoveProvider(_mockPokemonRepository.Object, _mockProbabilityUtility.Object, _config, _random);
            poke = target.AssignMoves(poke, _level);

            // Assert
            var moves = new List<int> { poke.MoveIndex1, poke.MoveIndex2, poke.MoveIndex3, poke.MoveIndex4, };
            var powers = moves.Join(movesForPokemon, moveId => moveId, move => move.MoveId, (moveId, move) => move.Power).ToList();
            Assert.Equal(3, powers.Count(power => power > 0));
            Assert.Equal(1, powers.Count(power => power == 0));

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Fact]
        public void AssignMovesBalancesDamageFlag()
        {
            // Set up
            var poke = new Pokemon();
            var movesForPokemon = new List<PokemonMoveSetResult>
            {
                new PokemonMoveSetResult { MoveId = 1, Power = 0},
                new PokemonMoveSetResult { MoveId = 2, Power = 90 },
                new PokemonMoveSetResult { MoveId = 3, Power = 130},
                new PokemonMoveSetResult { MoveId = 4, Power = 25},
                new PokemonMoveSetResult { MoveId = 5, Power = 0 },
                new PokemonMoveSetResult { MoveId = 6, Power = 0  },
            };

            // Mock
            _mockPokemonRepository.Setup(r => r.GetTMs()).Returns(new List<int>(0));
            _mockPokemonRepository.Setup(r => r.GetMovesForPokemon(poke.SpeciesId, _level)).Returns(movesForPokemon);
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsAny<IList<IChoice>>())).Returns((IList<IChoice> choices) =>
                choices.Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Run
            var target = new PokemonMoveProvider(_mockPokemonRepository.Object, _mockProbabilityUtility.Object, _config, _random);
            poke = target.AssignMoves(poke, _level);

            // Assert
            var moves = new List<int> { poke.MoveIndex1, poke.MoveIndex2, poke.MoveIndex3, poke.MoveIndex4, };
            var powers = moves.Join(movesForPokemon, moveId => moveId, move => move.MoveId, (moveId, move) => move.Power).ToList();
            Assert.Equal(2, powers.Count(power => power > 0));
            Assert.Equal(2, powers.Count(power => power == 0));

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Fact]
        public void AssignMovesRemovesFromTmBank()
        {
            // Set up
            var poke = new Pokemon();
            var tmBank = new List<int> { 3, 2, 4 };
            var movesForPokemon = new List<PokemonMoveSetResult>
            {
                new PokemonMoveSetResult { MoveId = 1, LearnType = "machine"},
                new PokemonMoveSetResult { MoveId = 2, LearnType = "machine" },
                new PokemonMoveSetResult { MoveId = 3, LearnType = "machine"},
                new PokemonMoveSetResult { MoveId = 4, LearnType = "machine"},
                new PokemonMoveSetResult { MoveId = 5, LearnType = "machine" },
                new PokemonMoveSetResult { MoveId = 6 },
            };

            // Mock
            _mockPokemonRepository.Setup(r => r.GetTMs()).Returns(tmBank);
            _mockPokemonRepository.Setup(r => r.GetMovesForPokemon(poke.SpeciesId, _level)).Returns(movesForPokemon);
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsAny<IList<IChoice>>())).Returns((IList<IChoice> choices) =>
                choices.Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Run
            var target = new PokemonMoveProvider(_mockPokemonRepository.Object, _mockProbabilityUtility.Object, _config, _random);
            poke = target.AssignMoves(poke, _level);

            // Assert
            var expected = tmBank.Union(movesForPokemon.Where(move => move.LearnType == null).Select(move => move.MoveId)).ToList();
            Assert.Equal(4, expected.Count); // sanity
            Assert.Contains(poke.MoveIndex1, expected);
            Assert.Contains(poke.MoveIndex2, expected);
            Assert.Contains(poke.MoveIndex3, expected);
            Assert.Contains(poke.MoveIndex4, expected);

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Fact]
        public void AssignMovesInBank()
        {
            // Set up
            var poke = new Pokemon();
            var hmBank = new List<int> { 2, 4 };
            var tmBank = new List<int> { 3, 7 };
            var movesForPokemon = new List<PokemonMoveSetResult>
            {
                new PokemonMoveSetResult { MoveId = 1, LearnType = "machine"},
                new PokemonMoveSetResult { MoveId = 2, LearnType = "machine" },
                new PokemonMoveSetResult { MoveId = 3, LearnType = "machine"},
                new PokemonMoveSetResult { MoveId = 4, LearnType = "machine"},
                new PokemonMoveSetResult { MoveId = 5, LearnType = "machine" },
                new PokemonMoveSetResult { MoveId = 6, LearnType = "machine" },
                new PokemonMoveSetResult { MoveId = 7, LearnType = "machine" },
                new PokemonMoveSetResult { MoveId = 8, LearnType = "machine" },
            };
            _config.Value.Configuration.HMBank = hmBank;

            // Mock
            _mockPokemonRepository.Setup(r => r.GetTMs()).Returns(tmBank);
            _mockPokemonRepository.Setup(r => r.GetMovesForPokemon(poke.SpeciesId, _level)).Returns(movesForPokemon);
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsAny<IList<IChoice>>())).Returns((IList<IChoice> choices) =>
                choices.Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Run
            var target = new PokemonMoveProvider(_mockPokemonRepository.Object, _mockProbabilityUtility.Object, _config, _random);
            poke = target.AssignMoves(poke, _level);

            // Assert
            var expected = tmBank.Union(hmBank).ToList();
            Assert.Equal(4, expected.Count); // sanity
            Assert.Contains(poke.MoveIndex1, expected);
            Assert.Contains(poke.MoveIndex2, expected);
            Assert.Contains(poke.MoveIndex3, expected);
            Assert.Contains(poke.MoveIndex4, expected);

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Theory]
        [InlineData(20, 35, 15)]
        [InlineData(35, 20, 15)]
        [InlineData(25, 20, 15)]
        public void AssignMovesDamageType(ushort spAttack, ushort attack, int delta)
        {
            // Set up
            _config.Value.Configuration.DamageTypeDelta = delta;

            var poke = new Pokemon
            {
                SpAttack = spAttack,
                Attack = attack
            };
            var movesForPokemon = new List<PokemonMoveSetResult>
            {
                new PokemonMoveSetResult { MoveId = 1, DamageType = "physical"},
                new PokemonMoveSetResult { MoveId = 2, DamageType = "physical" },
                new PokemonMoveSetResult { MoveId = 3, DamageType = "special"},
                new PokemonMoveSetResult { MoveId = 4, DamageType = "special"},
                new PokemonMoveSetResult { MoveId = 5, DamageType = "physical" },
                new PokemonMoveSetResult { MoveId = 6, DamageType = "physical" },
                new PokemonMoveSetResult { MoveId = 7, DamageType = "special" },
                new PokemonMoveSetResult { MoveId = 8, DamageType = "special" },
            };

            // Mock
            _mockPokemonRepository.Setup(r => r.GetTMs()).Returns(new List<int>());
            _mockPokemonRepository.Setup(r => r.GetMovesForPokemon(poke.SpeciesId, _level)).Returns(movesForPokemon);
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsAny<IList<IChoice>>())).Returns((IList<IChoice> choices) =>
                choices.Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Run
            var target = new PokemonMoveProvider(_mockPokemonRepository.Object, _mockProbabilityUtility.Object, _config, _random);
            poke = target.AssignMoves(poke, _level);

            // Assert
            var type = Math.Abs(spAttack - attack) < delta ? DamageType.Both : spAttack > attack ? DamageType.Special : DamageType.Physical;
            var expecteds = movesForPokemon
                .Where(move => type == DamageType.Both ||
                               (type == DamageType.Physical && move.DamageType.Equals("physical")) ||
                               (type == DamageType.Special && move.DamageType.Equals("special")))
                .Select(move => move.MoveId)
                .ToList();
            Assert.Contains(poke.MoveIndex1, expecteds);
            Assert.Contains(poke.MoveIndex2, expecteds);
            Assert.Contains(poke.MoveIndex3, expecteds);
            Assert.Contains(poke.MoveIndex4, expecteds);

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Fact]
        public void AssignMovesForSpecialPokemon()
        {
            // Set up
            var poke = new Pokemon();
            var movesForPokemon = new List<PokemonMoveSetResult>
            {
                new PokemonMoveSetResult { MoveId =  67 },
                new PokemonMoveSetResult { MoveId =  99 },
                new PokemonMoveSetResult { MoveId =  32 },
                new PokemonMoveSetResult { MoveId =  44 }
            };
            _config.Value.Configuration.SpecialPokemon.Add(poke.SpeciesId);

            // Mock
            _mockPokemonRepository.Setup(r => r.GetTMs()).Returns(new List<int>());
            _mockPokemonRepository.Setup(r => r.GetMovesForPokemon(poke.SpeciesId, _level)).Returns(movesForPokemon);
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());

            // Run
            var target = new PokemonMoveProvider(_mockPokemonRepository.Object, _mockProbabilityUtility.Object, _config, _random);
            poke = target.AssignMoves(poke, _level);

            // Assert
            var expected = movesForPokemon.Take(4).Select(t => t.MoveId).ToList();
            Assert.Contains(poke.MoveIndex1, expected);
            Assert.Contains(poke.MoveIndex2, expected);
            Assert.Contains(poke.MoveIndex3, expected);
            Assert.Contains(poke.MoveIndex4, expected);

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }

        [Fact]
        public void AssignMovesForSketch()
        {
            // Set up
            var poke = new Pokemon();
            var movesForPokemon = new List<PokemonMoveSetResult>
            {
                new PokemonMoveSetResult { MoveId = 166 }, // Sketch
            };
            var randoMoves = new List<PokemonMoveSetResult>
            {
                new PokemonMoveSetResult { MoveId =  67 },
                new PokemonMoveSetResult { MoveId =  99 },
                new PokemonMoveSetResult { MoveId =  32 },
                new PokemonMoveSetResult { MoveId =  44 }
            };

            // Mock
            _mockPokemonRepository.Setup(r => r.GetTMs()).Returns(new List<int>());
            _mockPokemonRepository.Setup(r => r.GetMovesForPokemon(poke.SpeciesId, _level)).Returns(movesForPokemon);
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockPokemonRepository.Setup(r => r.GetWeaknesses(string.Empty)).Returns(new List<string>());
            _mockPokemonRepository.Setup(r => r.GetRandomMoves(_config.Value.Configuration.RandomMoveMinPower, _config.Value.Configuration.RandomMoveMaxPower)).Returns(randoMoves);
            _mockProbabilityUtility.Setup(m => m.ChooseWithProbability(It.IsAny<IList<IChoice>>())).Returns((IList<IChoice> choices) =>
                choices.Select((choice, index) => new { choice, index })
                    .OrderByDescending(item => item.choice.Probability)
                    .First().index);

            // Run
            var target = new PokemonMoveProvider(_mockPokemonRepository.Object, _mockProbabilityUtility.Object, _config, _random);
            poke = target.AssignMoves(poke, _level);

            // Assert
            Assert.Contains(poke.MoveIndex1, randoMoves.Select(move => move.MoveId));
            Assert.Contains(poke.MoveIndex2, randoMoves.Select(move => move.MoveId));
            Assert.Contains(poke.MoveIndex3, randoMoves.Select(move => move.MoveId));
            Assert.Contains(poke.MoveIndex4, randoMoves.Select(move => move.MoveId));

            // Verify
            _mockPokemonRepository.VerifyAll();
            _mockProbabilityUtility.VerifyAll();
        }
    }
}