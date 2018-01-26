using PokemonGenerator.Enumerations;
using PokemonGenerator.Generators;
using PokemonGenerator.Models;
using PokemonGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGenerator
{
    public interface IPokemonTeamGenerator
    {
        PokeList GenerateRandomPokemonTeam(int level);
    }

    /// <summary>
    /// The real  MVP of this application. Contains all the logic for generating a team of pokemon.
    /// </summary>
    internal class PokemonTeamGenerator : IPokemonTeamGenerator
    {
        private readonly IPokemonStatUtility _pokemonStatUtility;
        private readonly IProbabilityUtility _probabilityUtility;
        private readonly IPokemonMoveGenerator _pokemonMoveGenerator;
        private readonly PokemonGeneratorConfig _pokemonGeneratorConfig;
        private readonly Random _random;
        private IList<PokemonChoice> possiblePokemon;
        private int previousLevel;

        public PokemonTeamGenerator(IPokemonStatUtility pokemonStatUtility,
            IProbabilityUtility probabilityUtility, IPokemonMoveGenerator pokemonMoveGenerator,
            PokemonGeneratorConfig pokemonGeneratorConfig, Random random)
        {
            _pokemonStatUtility = pokemonStatUtility;
            _probabilityUtility = probabilityUtility;
            _pokemonMoveGenerator = pokemonMoveGenerator;
            _pokemonGeneratorConfig = pokemonGeneratorConfig;
            _random = random;
        }

        /// <summary>
        /// Smartly generates a team of six random (ish) pokemon.
        /// 
        /// </summary>
        /// <param name="level">The level of generated pokemon. Must be between 5 and 100 inclusive.</param>
        /// <returns><see cref="PokeList"/> containing the generated team.</returns>
        public PokeList GenerateRandomPokemonTeam(int level)
        {
            if (level < 5 || level > 100)
            {
                throw new ArgumentOutOfRangeException($"level ({level}) must be between 5 and 100 inclusive.");
            }

            // Choose Pokemon Team
            var pokeList = ChooseTeam(level);

            // Choose base stats
            _pokemonStatUtility.GetTeamBaseStats(pokeList, level);

            // Choose IV's and EV's
            _pokemonStatUtility.AssignIVsAndEVsToTeam(pokeList, level);

            // Calculate final stats using formulae
            _pokemonStatUtility.CalculateStatsForTeam(pokeList, level);

            // Assign moves
            _pokemonMoveGenerator.AssignMovesToTeam(pokeList, level);

            // Done!
            return pokeList;
        }

        /// <summary>
        /// Chooses six rendom (ish) empty <see cref="Pokemon"/> with only their species (ID)..
        /// Note:
        /// <para />
        /// Will only choose pokemon at a certain level, eliminating pokemon that would have eveolved before the level, or will be eveolved into after the level. <para />
        /// Will never choose pokemon contained in <see cref="IGNOREPOKEMON"/>.<para />
        /// Will very rarely choose pokemon contained in <see cref="LEGENDARIES"/>.<para />
        /// Has a low chance of choose pokemon contained in <see cref="SPECIALPOKEMON"/>.<para />
        /// </summary>
        /// <param name="level">The level of generated pokemon. Must be between 5 and 100 inclusive.</param>
        /// <returns><see cref="PokeList"/> containing the generated team.</returns>
        private PokeList ChooseTeam(int level)
        {
            var ret = new PokeList(_pokemonGeneratorConfig.TeamSize);

            // Make sure both players end up with different pokemons, re-use the same list if possible
            if (previousLevel != level || possiblePokemon == null || possiblePokemon.Count < 20)
            {
                previousLevel = level;
                possiblePokemon = _pokemonStatUtility.GetPossiblePokemon(level).Select(id => new PokemonChoice { PokemonId = id }).ToList();
            }

            // add initial probabilities
            foreach (var choice in possiblePokemon)
            {
                if (_pokemonGeneratorConfig.IgnoredPokemon.Contains(choice.PokemonId))
                {
                    choice.Probability = _pokemonGeneratorConfig.PokemonLiklihood.Ignored;
                }
                else if (_pokemonGeneratorConfig.LegendaryPokemon.Contains(choice.PokemonId))
                {
                    choice.Probability =  _pokemonGeneratorConfig.PokemonLiklihood.Legendary;
                }
                else if (_pokemonGeneratorConfig.SpecialPokemon.Contains(choice.PokemonId))
                {
                    choice.Probability = _pokemonGeneratorConfig.PokemonLiklihood.Special;
                }
                else
                {
                    choice.Probability = _pokemonGeneratorConfig.PokemonLiklihood.Standard;
                }
            }

            // choose team
            for (int i = 0; i < _pokemonGeneratorConfig.TeamSize; i++)
            {
                var ichooseYou = new Pokemon();
                var chosen_one = _probabilityUtility.ChooseWithProbability(possiblePokemon.Cast<IChoice>().ToList());
                if (chosen_one == null)
                {
                    throw new ArgumentException("Not enough Pokemon to choose from.");
                }

                ichooseYou.SpeciesId = (byte)possiblePokemon[(int)chosen_one].PokemonId;
                ret.Species[i] = (byte)possiblePokemon[(int)chosen_one].PokemonId;
                possiblePokemon.RemoveAt((int)chosen_one);
                ichooseYou.Unused = 0x0;
                ichooseYou.OTName = "ROBOT";
                ichooseYou.HeldItem = 0x0;
                ret.Pokemon[i] = ichooseYou;
            }

            return ret;
        }
    }
}