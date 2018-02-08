using System;

namespace PokemonGenerator.Models.DTO
{
    public class PokemonMoveSetResult
    {
        public int Level { get; set; }

        public int MoveId { get; set; }

        public string MoveName { get; set; }

        public string Type { get; set; }

        public System.Int16? Power { get; set; }

        public System.Int16? Pp { get; set; }

        public string DamageType { get; set; }

        public string LearnType { get; set; }

        public string Effect { get; set; }

        public override string ToString()
        {
            return $"{MoveName} {{{Power},{Pp}}} ({Type})";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(PokemonMoveSetResult))
            {
                return false;
            }

            return Equals((PokemonMoveSetResult) obj);
        }

        protected bool Equals(PokemonMoveSetResult other)
        {
            return MoveId == other.MoveId && string.Equals(LearnType, other.LearnType, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (MoveId * 397) ^ (LearnType != null ? LearnType.GetHashCode() : 0);
            }
        }
    }
}