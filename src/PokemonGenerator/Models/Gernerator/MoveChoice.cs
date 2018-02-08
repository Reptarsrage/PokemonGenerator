using PokemonGenerator.Models.Dto;

namespace PokemonGenerator.Models.Gernerator
{
    internal class MoveChoice : IChoice
    {
        public PokemonMoveSetResult Move { get; set; }
        public double Probability { get; set; }
    }
}