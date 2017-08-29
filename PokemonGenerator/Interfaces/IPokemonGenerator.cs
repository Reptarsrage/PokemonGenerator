using PokemonGenerator.Enumerations;
using PokemonGenerator.Models;

namespace PokemonGenerator
{
    internal interface IPokemonGenerator
    {
        PokeList GenerateRandomPokemon(int level, Entropy entropy);
    }
}