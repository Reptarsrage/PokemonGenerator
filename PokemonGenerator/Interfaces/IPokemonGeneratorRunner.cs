using PokemonGenerator.Models;

namespace PokemonGenerator
{
    public interface IPokemonGeneratorRunner
    {
        void Run(PersistentConfig configOptions);
    }
}