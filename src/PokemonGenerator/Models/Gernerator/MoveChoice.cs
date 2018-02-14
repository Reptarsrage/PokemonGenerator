using PokemonGenerator.Models.DTO;

namespace PokemonGenerator.Models.Gernerator
{
    internal class MoveChoice : IChoice
    {
        public PokemonMoveSetResult Move { get; set; }
        
        public double Probability { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(MoveChoice))
            {
                return false;
            }

            return Equals((MoveChoice) obj);
        }

        protected bool Equals(MoveChoice other)
        {
            return Equals(Move, other.Move);
        }

        public override int GetHashCode()
        {
            return (Move != null ? Move.GetHashCode() : 0);
        }
    }
}