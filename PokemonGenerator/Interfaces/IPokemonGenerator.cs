using PokemonGenerator.Enumerations;
using PokemonGenerator.Models;

namespace PokemonGenerator
{
    public interface IPokemonGenerator
    {
        PokeList GenerateRandomPokemon(int level, Entropy entropy);
    }
}