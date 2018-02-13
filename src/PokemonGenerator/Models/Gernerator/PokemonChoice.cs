namespace PokemonGenerator.Models.Gernerator
{
    internal class PokemonChoice :  IChoice
    {
        public int PokemonId { get; set; }
        public double Probability { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && obj.GetType() == typeof(PokemonChoice) && Equals((PokemonChoice) obj);
        }

        protected bool Equals(PokemonChoice other)
        {
            return PokemonId == other.PokemonId;
        }

        public override int GetHashCode()
        {
            return PokemonId;
        }
    }
}