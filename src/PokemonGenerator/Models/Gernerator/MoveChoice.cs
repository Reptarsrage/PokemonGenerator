namespace PokemonGenerator.Models
{
    internal class MoveChoice : IChoice
    {
        public PokemonMoveSetResult Move { get; set; }
        public double Probability { get; set; }
    }
}