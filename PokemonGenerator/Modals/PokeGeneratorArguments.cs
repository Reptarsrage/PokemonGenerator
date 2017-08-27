namespace PokemonGenerator.Modals
{
    /// <summary>
    /// A modal used to store and serialize/deserialize information.
    /// </summary>
    class PokeGeneratorArguments
    {
        public string InputSav1 { get; set; }
        public string InputSav2 { get; set; }
        public string OutputSav1 { get; set; }
        public string OutputSav2 { get; set; }
        public int Level { get; set; }
        public string EntropyVal { get; set; }
        public int Entropy { get; set; }

        public string Game1 { get; set; }
        public string Game2 { get; set; }
        public string Name1 { get; internal set; }
        public string Name2 { get; internal set; }
    }
}