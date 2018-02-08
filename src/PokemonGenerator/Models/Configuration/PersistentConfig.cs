namespace PokemonGenerator.Models.Configuration
{
    public class PersistentConfig
    {
        public PersistentConfig()
        {
            Configuration = new PokemonGeneratorConfig();
            Options = new ProgramOptions();
        }

        public PokemonGeneratorConfig Configuration { get; set; }

        public ProgramOptions Options { get; set; }
    }
}