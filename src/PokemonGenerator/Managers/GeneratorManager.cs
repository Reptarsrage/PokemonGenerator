﻿using Microsoft.Extensions.Options;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Models.Serialization;
using PokemonGenerator.Providers;
using PokemonGenerator.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PokemonGenerator.Managers
{
    /// <summary>
    /// Generates a team of pokemon for both players
    /// </summary>
    public interface IGeneratorManager
    {
        /// <summary>
        /// Generates a team of pokemon for both players
        /// </summary>
        void Generate();
    }

    /// <inheritdoc />
    public class GeneratorManager : IGeneratorManager
    {
        private readonly IPokemonMoveProvider _pokemonMoveProvider;
        private readonly IPokemonProvider _pokemonProvider;
        private readonly ISaveFileProvider _saveFileProvider;
        private readonly IPokeGeneratorOptionsValidator _optionsValidator;
        private readonly IPokemonStatProvider _pokemonStatProvider;
        private readonly IOptions<PersistentConfig> _config;

        public GeneratorManager(
            IPokemonMoveProvider pokemonMoveProvider,
            IPokemonProvider pokemonProvider,
            ISaveFileProvider saveFileProvider,
            IPokeGeneratorOptionsValidator optionsValidator,
            IPokemonStatProvider pokemonStatProvider, IOptions<PersistentConfig> config)
        {
            _pokemonMoveProvider = pokemonMoveProvider;
            _pokemonProvider = pokemonProvider;
            _saveFileProvider = saveFileProvider;
            _optionsValidator = optionsValidator;
            _pokemonStatProvider = pokemonStatProvider;
            _config = config;
        }

        /// <inheritdoc />
        public void Generate()
        {
            if (!_optionsValidator.Validate(_config.Value.Options))
                throw new ArgumentException("configOptions");

            var sav = _saveFileProvider.Load(_config.Value.Options.PlayerOne.InputSaveLocation);

            // Generate Player One and Team
            sav.PlayerName = _config.Value.Options.PlayerOne.Name;
            CopyAndGen(_config.Value.Options.PlayerOne.OutputSaveLocation, _config.Value.Options.PlayerOne.InputSaveLocation,
                sav, _config.Value.Options.Level, _config.Value.Options.PlayerOne.Team.MemberIds);

            // Generate Player Two and Team
            sav.PlayerName = _config.Value.Options.PlayerTwo.Name;
            CopyAndGen(_config.Value.Options.PlayerTwo.OutputSaveLocation, _config.Value.Options.PlayerTwo.InputSaveLocation,
                sav, _config.Value.Options.Level, _config.Value.Options.PlayerTwo.Team.MemberIds);
        }

        /// <summary>
        /// Rakes a save file, copies it (or replaces it), generates a team of six pokemon, and saves the team to the output file.
        /// </summary>
        /// <param name="outSaveFile">Full path to the ouput file.</param>
        /// <param name="inSaveFile">Full path to the input file.</param>
        /// <param name="save">The <see cref="SaveFileModel" to use when saving./></param>
        /// <param name="level">The level to generate pokemon at. Must be 5-100.</param>
        /// <param name="chosenMembers">Team members chosen explicitly by the user</param>
        private void CopyAndGen(string outSaveFile, string inSaveFile, SaveFileModel save, int level, List<int> chosenMembers)
        {
            if (string.IsNullOrWhiteSpace(outSaveFile) || string.IsNullOrWhiteSpace(inSaveFile))
            {
                throw new ArgumentException(nameof(outSaveFile));
            }

            if (!Directory.Exists(Path.GetDirectoryName(outSaveFile)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outSaveFile));
            }

            // If Allowing duplicates, reload lists for selectining randoms
            if (_config.Value.Configuration.AllowDuplicates)
            {
                _pokemonProvider.ReloadRandomBag(level);
            }

            // Choose Pokemon Team
            var list = new List<Pokemon>();
            for (var i = 0; i < _config.Value.Configuration.TeamSize; i++)
            {
                list.Add(i < chosenMembers.Count
                    ? _pokemonProvider.GeneratePokemon(chosenMembers[i], level)
                    : _pokemonProvider.GenerateRandomPokemon(level));
            }

            var pokeList = new PokeList(list.Count)
            {
                Species = list.Select(p => p.SpeciesId).ToArray(),
                Pokemon = list.ToArray()
            };

            // Choose base stats
            _pokemonStatProvider.GetTeamBaseStats(pokeList, level);

            // Choose IV's and EV's
            _pokemonStatProvider.AssignIVsAndEVsToTeam(pokeList, level);

            // Calculate final stats using formulae
            _pokemonStatProvider.CalculateStatsForTeam(pokeList, level);

            // Assign moves
            for (var i = 0; i < pokeList.Pokemon.Length; i++)
            {
                pokeList.Pokemon[i] = _pokemonMoveProvider.AssignMoves(pokeList.Pokemon[i], level);
            }

            // Save to file
            save.TeamPokemonList = pokeList;
            _saveFileProvider.Save(outSaveFile, inSaveFile, save);
        }
    }
}