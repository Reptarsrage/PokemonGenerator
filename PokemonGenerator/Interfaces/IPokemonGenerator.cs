using PokemonGenerator.Enumerations;
using PokemonGenerator.Models;

namespace PokemonGenerator
{
    public interface IPokemonGeneratorWorker
    {
        PokemonGeneratorConfig Config { get; set; }

        PokeList GenerateRandomPokemon(int level, Entropy entropy);
    }
}