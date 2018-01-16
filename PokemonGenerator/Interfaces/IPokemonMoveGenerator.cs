using PokemonGenerator.Models;

namespace PokemonGenerator.Interfaces
{
    internal interface IPokemonMoveGenerator
    {
        void AssignMovesToTeam(PokeList list, int level);
    }
}