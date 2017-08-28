using System.Collections.Generic;
using PokemonGenerator.DAL.Serialization;
using PokemonGenerator.Modals;

namespace PokemonGenerator.DAL
{
    interface IPokemonDA
    {
        List<uspGetPokemonMoveSetResult> GetMovesForPokemon(int id, int level);
        List<int> GetPossiblePokemon(int level, Entropy entopy);
        List<uspGetPokemonMoveSetResult> GetRandomMoves(int minPower, int maxPower);
        List<tbl_vwBaseStats> GetTeamBaseStats(PokeList list);
        List<string> GetWeaknesses(string v);
        List<int> GetTMs();
    }
}