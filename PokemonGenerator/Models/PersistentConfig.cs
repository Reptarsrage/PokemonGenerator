namespace PokemonGenerator.Models
{
    public class PersistentConfig
    {
        public PersistentConfig()
        {
            Configuration = new PokemonGeneratorConfig();
            Options = new PokeGeneratorOptions();
        }

        public PokemonGeneratorConfig Configuration { get; set; }

        public PokeGeneratorOptions Options { get; set; }
    }
}
