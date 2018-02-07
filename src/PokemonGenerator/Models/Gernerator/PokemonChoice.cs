namespace PokemonGenerator.Models
{
    internal class PokemonChoice :  IChoice
    {
        public int PokemonId { get; set; }
        public double Probability { get; set; }
    }
}
