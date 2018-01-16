using PokemonGenerator.Enumerations;
using PokemonGenerator.Models;

namespace PokemonGenerator
{
    public interface IPokemonTeamGenerator
    {
        PokeList GenerateRandomPokemonTeam(int level, Entropy entropy);
    }
}