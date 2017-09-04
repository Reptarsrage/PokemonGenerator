using PokemonGenerator.Models;

namespace PokemonGenerator
{
    public interface IPokemonGeneratorRunner
    {
        void Run(string contentDirectory, string outputDirectory, PokeGeneratorArguments options);
    }
}