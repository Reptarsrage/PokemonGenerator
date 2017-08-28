namespace PokemonGenerator.Modals
{
    /// <summary>
    /// A modal used to store and serialize/deserialize information.
    /// </summary>
    internal class PokeGeneratorArguments
    {
        public string InputSavOne { get; set; }
        public string InputSavTwo { get; set; }
        public string OutputSav1 { get; set; }
        public string OutputSav2 { get; set; }
        public int Level { get; set; }
        public string EntropyVal { get; set; }
        public int Entropy { get; set; }

        public string GameOne { get; set; }
        public string GameTwo { get; set; }
        public string NameOne { get; internal set; }
        public string NameTwo { get; internal set; }
    }
}