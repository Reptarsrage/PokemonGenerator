namespace PokemonGeneratorGUI.Models
{
    /// <summary>
    /// Args to pass to background worker
    /// </summary>
    public struct WorkerArgs
    {
        public int level { get; set; }
        public string entropy { get; set; }
        public string i1 { get; set; }
        public string i2 { get; set; }
        public string o1 { get; set; }
        public string o2 { get; set; }
        public string p64 { get; set; }
        public string pgExe { get; set; }
        public string game1 { get; set; }
        public string game2 { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
    }
}