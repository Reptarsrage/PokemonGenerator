using PokemonGenerator.Modals;

namespace PokemonGenerator
{
    internal interface IPokemonGenerator
    {
        PokeList GenerateRandomPokemon(int level, Entropy entropy);
    }
}