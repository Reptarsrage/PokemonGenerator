namespace PokemonGenerator.Models
{
    public class PersistentConfig
    {
        public PersistentConfig()
        {
            Configuration = new PokemonGeneratorConfig();
            Options = new Options();
        }

        public PokemonGeneratorConfig Configuration { get; set; }

        public Options Options { get; set; }
    }
}