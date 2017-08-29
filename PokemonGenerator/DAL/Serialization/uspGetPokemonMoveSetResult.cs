namespace PokemonGenerator.DAL.Serialization
{
    internal class uspGetPokemonMoveSetResult
    {
        public int level { get; set; }
        public int moveId { get; set; }
        public string moveName { get; set; }
        public string Type { get; set; }
        public System.Int16? power { get; set; }
        public System.Int16? pp { get; set; }
        public string damageType { get; set; }
        public string learnType { get; set; }
        public string effect { get; set; }

        public override string ToString()
        {
            return $"{moveName} {{{power},{pp}}} ({Type})";
        }
    }
}
