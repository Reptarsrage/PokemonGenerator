using PokemonGenerator.Models;

namespace PokemonGenerator.Utilities
{
    interface IProbabilityUtility
    {
        int? ChooseWithProbability(System.Collections.Generic.IList<IChoice> choices);
        int GaussianRandom(int low, int high);
    }
}