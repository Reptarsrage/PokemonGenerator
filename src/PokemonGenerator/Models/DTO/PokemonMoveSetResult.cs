namespace PokemonGenerator.Models
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
    }
}
