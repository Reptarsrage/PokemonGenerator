namespace PokemonGenerator.Models
{
    public class PersistentConfig
    {
        public PersistentConfig(PokemonGeneratorConfig pokemonGeneratorConfig, PokeGeneratorOptions pokeGeneratorOptions)
        {
            Configuration = pokemonGeneratorConfig;
            Options = pokeGeneratorOptions;
        }

        public PokemonGeneratorConfig Configuration { get; set; }

        public PokeGeneratorOptions Options { get; set; }
    }
}
