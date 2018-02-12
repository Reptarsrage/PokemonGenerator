using Microsoft.Extensions.Options;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Models.Gernerator;
using PokemonGenerator.Models.Serialization;
using PokemonGenerator.Repositories;
using PokemonGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGenerator.Providers
{
    /// <summary>
    /// Generates a pokemon without stats or moves.
    /// For moves see <see cref="IPokemonMoveProvider"/> and for stats see <see cref="PokemonStatProvider"/>
    /// </summary>
    public interface IPokemonProvider
    {
        /// <summary>
        /// Chooses a random (ish) <see cref="Pokemon"/> with only the species (ID).
        /// 
        /// Will only choose pokemon at a certain level, eliminating pokemon that would have eveolved before the level, or will be eveolved into after the level. 
        /// Will never choose pokemon contained in <see cref="GeneratorConfig.ForbiddenPokemon"/>.
        /// Will very rarely choose pokemon contained in <see cref="GeneratorConfig.LegendaryPokemon"/>.
        /// Has a low chance of choose pokemon contained in <see cref="GeneratorConfig.SpecialPokemon"/>.
        /// </summary>
        /// <param name="level">The level of generated pokemon. Must be between 5 and 100 inclusive.</param>
        Pokemon GenerateRandomPokemon(int level);

        /// <summary>
        /// Generates a pokemon given the Id and Level
        /// </summary>
        /// <param name="speciesId">Sepcies Id must be between 1-256</param>
        /// <param name="level">The level of generated pokemon. Must be between 5 and 100 inclusive.</param>
        /// <returns></returns>
        Pokemon GeneratePokemon(int speciesId, int level);

        /// <summary>
        /// Refresheds the bag of random pokemon. 
        /// Otherwise the bag will only contain things that haven't been selected yet.
        /// </summary>
        void ReloadRandomBag(int level);
    }

    /// <inheritdoc />
    public class PokemonProvider : IPokemonProvider
    {
        private readonly IProbabilityUtility _probabilityUtility;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IOptions<PersistentConfig> _config;

        private List<PokemonChoice> _possiblePokemon;
        private List<PokemonChoice> _randomBagOfPokemon;
        private int _previousLevel;

        public PokemonProvider(
            IProbabilityUtility probabilityUtility,
            IPokemonRepository pokemonRepository,
            IOptions<PersistentConfig> pokemonGeneratorConfig)
        {
            _probabilityUtility = probabilityUtility;
            _pokemonRepository = pokemonRepository;
            _config = pokemonGeneratorConfig;
        }

        public void ReloadRandomBag(int level)
        {
            if (level < 5 || level > 100)
            {
                throw new ArgumentOutOfRangeException($"level ({level}) must be between 5 and 100 inclusive.");
            }

            _randomBagOfPokemon = _pokemonRepository.GetRandomBagPokemon(level)
                .Select(id => new PokemonChoice { PokemonId = id })
                .ToList();
        }

        /// <inheritdoc />
        public Pokemon GeneratePokemon(int speciesId, int level)
        {
            if (level < 5 || level > 100)
            {
                throw new ArgumentOutOfRangeException($"level ({level}) must be between 5 and 100 inclusive.");
            }

            if (speciesId < 1 || speciesId > 256)
            {
                throw new ArgumentOutOfRangeException($"level ({speciesId}) must be between 1 and 256 inclusive.");
            }

            // Lazy load
            if (_possiblePokemon == null)
            {
                _possiblePokemon = _pokemonRepository.GetPossiblePokemon(level)
                    .Select(id => new PokemonChoice { PokemonId = id })
                    .ToList();
            }

            // Make sure both players end up with different pokemons, re-use the same list if possible
            // if the allow duplicates flag is set, we will always reload the list
            if (_previousLevel != level || _randomBagOfPokemon.Count < 10)
            {
                _previousLevel = level;
                ReloadRandomBag(level);
            }

            var iChooseYou = new Pokemon
            {
                SpeciesId = (byte)speciesId,
                Unused = 0x0,
                OTName = "ROBOT",
                HeldItem = 0x0
            };

            // I think we want to disallow players to randomly get pokemon they've already chosen
            // even if this means the other player can't randomly get it either
            var poke = _possiblePokemon.First(p => p.PokemonId == speciesId);
            if (_randomBagOfPokemon.Contains(poke))
            {
                _randomBagOfPokemon.Remove(poke);
            }

            return iChooseYou;
        }

        /// <inheritdoc />
        public Pokemon GenerateRandomPokemon(int level)
        {
            if (level < 5 || level > 100)
            {
                throw new ArgumentOutOfRangeException($"level ({level}) must be between 5 and 100 inclusive.");
            }

            // Make sure both players end up with different pokemons, re-use the same list if possible
            if (_previousLevel != level || _randomBagOfPokemon.Count < 10)
            {
                _previousLevel = level;
                ReloadRandomBag(level);
            }

            // add initial probabilities
            foreach (var choice in _randomBagOfPokemon)
            {
                if (_config.Value.Configuration.DisabledPokemon.Contains(choice.PokemonId) ||
                    _config.Value.Configuration.ForbiddenPokemon.Contains(choice.PokemonId))
                {
                    choice.Probability = _config.Value.Configuration.PokemonLiklihood.Ignored;
                }
                else if (_config.Value.Configuration.LegendaryPokemon.Contains(choice.PokemonId))
                {
                    choice.Probability = _config.Value.Configuration.PokemonLiklihood.Legendary;
                }
                else if (_config.Value.Configuration.SpecialPokemon.Contains(choice.PokemonId))
                {
                    choice.Probability = _config.Value.Configuration.PokemonLiklihood.Special;
                }
                else
                {
                    choice.Probability = _config.Value.Configuration.PokemonLiklihood.Standard;
                }
            }

            // choose
            var chosenId = _probabilityUtility.ChooseWithProbability(_randomBagOfPokemon.Cast<IChoice>().ToList());
            if (chosenId == null)
            {
                throw new ArgumentException("Not enough Pokemon to choose from.");
            }

            var iChooseYou = new Pokemon
            {
                SpeciesId = (byte)_randomBagOfPokemon[(int)chosenId].PokemonId,
                Unused = 0x0,
                OTName = "ROBOT",
                HeldItem = 0x0
            };

            _randomBagOfPokemon.RemoveAt((int) chosenId);

            return iChooseYou;
        }
    }
}