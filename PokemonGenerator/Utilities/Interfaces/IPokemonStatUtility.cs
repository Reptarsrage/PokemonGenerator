using PokemonGenerator.Enumerations;
using PokemonGenerator.Models;
using System.Collections.Generic;

namespace PokemonGenerator.Utilities
{
    interface IPokemonStatUtility
    {
        void GetTeamBaseStats(PokeList list, int level);
        void AssignIVsAndEVsToTeam(PokeList list);
        IEnumerable<int> GetPossiblePokemon(int level, Entropy entopy);
        void CalculateStatsForTeam(PokeList list, int level);
    }
}