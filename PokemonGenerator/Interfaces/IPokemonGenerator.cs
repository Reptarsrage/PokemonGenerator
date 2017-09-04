using PokemonGenerator.Enumerations;
using PokemonGenerator.Models;

namespace PokemonGenerator
{
    public interface IPokemonGenerator
    {
        PokemonGeneratorConfig Config { get; set; }

        PokeList GenerateRandomPokemon(int level, Entropy entropy);
    }
}