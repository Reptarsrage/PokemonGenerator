namespace PokemonGenerator.Models.Gernerator
{
    internal class PokemonChoice :  IChoice
    {
        public int PokemonId { get; set; }
        public double Probability { get; set; }
    }
}
