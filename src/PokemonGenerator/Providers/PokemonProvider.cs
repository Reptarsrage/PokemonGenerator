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
    }

    /// <inheritdoc />
    public class PokemonProvider : IPokemonProvider
    {
        private readonly IProbabilityUtility _probabilityUtility;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IOptions<PersistentConfig> _pokemonGeneratorConfig;

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
            _pokemonGeneratorConfig = pokemonGeneratorConfig;
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
            if (_previousLevel != level || _randomBagOfPokemon.Count < 10)
            {
                _previousLevel = level;
                _randomBagOfPokemon = _pokemonRepository.GetRandomBagPokemon(level)
                    .Select(id => new PokemonChoice { PokemonId = id })
                    .ToList();
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

            // Lazy load
            if (_possiblePokemon == null)
            {
                _possiblePokemon = _pokemonRepository.GetPossiblePokemon(level)
                    .Select(id => new PokemonChoice { PokemonId = id })
                    .ToList();
            }

            // Make sure both players end up with different pokemons, re-use the same list if possible
            if (_previousLevel != level || _randomBagOfPokemon.Count < 10)
            {
                _previousLevel = level;
                _randomBagOfPokemon = _pokemonRepository.GetRandomBagPokemon(level)
                    .Select(id => new PokemonChoice { PokemonId = id })
                    .ToList();
            }

            // add initial probabilities
            foreach (var choice in _possiblePokemon)
            {
                if (_pokemonGeneratorConfig.Value.Configuration.DisabledPokemon.Contains(choice.PokemonId) ||
                    _pokemonGeneratorConfig.Value.Configuration.ForbiddenPokemon.Contains(choice.PokemonId))
                {
                    choice.Probability = _pokemonGeneratorConfig.Value.Configuration.PokemonLiklihood.Ignored;
                }
                else if (_pokemonGeneratorConfig.Value.Configuration.LegendaryPokemon.Contains(choice.PokemonId))
                {
                    choice.Probability = _pokemonGeneratorConfig.Value.Configuration.PokemonLiklihood.Legendary;
                }
                else if (_pokemonGeneratorConfig.Value.Configuration.SpecialPokemon.Contains(choice.PokemonId))
                {
                    choice.Probability = _pokemonGeneratorConfig.Value.Configuration.PokemonLiklihood.Special;
                }
                else
                {
                    choice.Probability = _pokemonGeneratorConfig.Value.Configuration.PokemonLiklihood.Standard;
                }
            }

            // choose
            var chosenId = _probabilityUtility.ChooseWithProbability(_possiblePokemon.Cast<IChoice>().ToList());
            if (chosenId == null)
            {
                throw new ArgumentException("Not enough Pokemon to choose from.");
            }

            var iChooseYou = new Pokemon
            {
                SpeciesId = (byte)_possiblePokemon[(int)chosenId].PokemonId,
                Unused = 0x0,
                OTName = "ROBOT",
                HeldItem = 0x0
            };

            _possiblePokemon.RemoveAt((int)chosenId);
            return iChooseYou;
        }
    }
}