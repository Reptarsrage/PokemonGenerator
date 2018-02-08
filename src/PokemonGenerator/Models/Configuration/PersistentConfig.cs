namespace PokemonGenerator.Models.Configuration
{
    public class PersistentConfig
    {
        public PersistentConfig()
        {
            Configuration = new GeneratorConfig();
            Options = new ProgramOptions();
        }

        public GeneratorConfig Configuration { get; set; }

        public ProgramOptions Options { get; set; }
    }
}