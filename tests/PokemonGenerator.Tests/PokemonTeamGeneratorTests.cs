﻿using Moq;
using PokemonGenerator.Generators;
using PokemonGenerator.Models;
using PokemonGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokemonGenerator.Tests
{
    public class PokemonTeamGeneratorTests
    {
        private PokemonTeamGenerator _pokemonGeneratorWorker;
        private Mock<IPokemonStatUtility> pokemonStatUtilityMock;
        private Mock<IProbabilityUtility> probabilityUtilityMock;
        private Mock<IPokemonMoveGenerator> pokemonMoveGeneratorMock;
        private PokemonGeneratorConfig config;
        private Random random;

        public PokemonTeamGeneratorTests()
        {
            random = new Random("The cake is a lie".GetHashCode());
            config = new PokemonGeneratorConfig
            {
                LegendaryPokemon = new List<int>(),
                DisabledPokemon = new List<int>(),
                SpecialPokemon = new List<int>(),
                PairedMoves = new Dictionary<int, int[]>() { },
                DependantMoves = new Dictionary<int, int[]>() { },
                HMBank = new List<int>() { },
                TeamSize = 6,
                MoveEffectFilters = new Dictionary<string, double>() { },
                PokemonLiklihood = new PokemonLiklihood(),
                Mean = 0.5D,
                StandardDeviation = 0.1D,
                SameTypeModifier = 1D,
                DamageModifier = 1D,
                PairedModifier = 1D,
                DamageTypeDelta = 15,
                RandomMoveMinPower = 40,
                RandomMoveMaxPower = 100
            };
            pokemonStatUtilityMock = new Mock<IPokemonStatUtility>(MockBehavior.Strict);
            probabilityUtilityMock = new Mock<IProbabilityUtility>(MockBehavior.Strict);
            pokemonMoveGeneratorMock = new Mock<IPokemonMoveGenerator>(MockBehavior.Strict);
        }

        [Fact]
        public void GenerateRandomPokemonTeamTest()
        {
            // SetUP
            var level = 100;

            // Mock
            pokemonStatUtilityMock.Setup(m => m.GetPossiblePokemon(level)).Returns(Enumerable.Range(0, 100));
            pokemonMoveGeneratorMock.Setup(m => m.AssignMovesToTeam(It.IsAny<PokeList>(), level));
            probabilityUtilityMock.Setup(m => m.ChooseWithProbability(It.IsNotNull<IList<IChoice>>()))
                .Returns<IList<IChoice>>(l => l
                  .Select((choice, index) => new { choice, index })
                  .OrderByDescending(item => item.choice.Probability)
                  .FirstOrDefault().index);
            pokemonStatUtilityMock.Setup(m => m.GetTeamBaseStats(It.IsAny<PokeList>(), level));
            pokemonStatUtilityMock.Setup(m => m.AssignIVsAndEVsToTeam(It.IsAny<PokeList>(), level));
            pokemonStatUtilityMock.Setup(m => m.CalculateStatsForTeam(It.IsAny<PokeList>(), level));

            // Run
            _pokemonGeneratorWorker = new PokemonTeamGenerator(pokemonStatUtilityMock.Object, probabilityUtilityMock.Object, pokemonMoveGeneratorMock.Object, config, random);
            var team = _pokemonGeneratorWorker.GenerateRandomPokemonTeam(level);

            // Assert
            Assert.NotNull(team);
            Assert.Equal(config.TeamSize, team.Pokemon.Count());

            // Verify
            pokemonStatUtilityMock.VerifyAll();
            probabilityUtilityMock.VerifyAll();
        }
    }
}