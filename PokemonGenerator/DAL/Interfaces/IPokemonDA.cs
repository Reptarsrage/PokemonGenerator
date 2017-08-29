using System.Collections.Generic;
using PokemonGenerator.Models;
using PokemonGenerator.Enumerations;

namespace PokemonGenerator.DAL
{
    interface IPokemonDA
    {
        IEnumerable<PokemonMoveSetResult> GetMovesForPokemon(int id, int level);
        IEnumerable<int> GetPossiblePokemon(int level, Entropy entopy);
        IEnumerable<PokemonMoveSetResult> GetRandomMoves(int minPower, int maxPower);
        IEnumerable<BaseStats> GetTeamBaseStats(PokeList list);
        IEnumerable<string> GetWeaknesses(string v);
        IEnumerable<int> GetTMs();
    }
}