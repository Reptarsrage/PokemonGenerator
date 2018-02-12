using Microsoft.Extensions.Options;
using PokemonGenerator.Controls;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Repositories;
using PokemonGenerator.Utilities;

namespace PokemonGenerator.Windows.Options
{
    public class OptionsWindowBase : PageEnabledControl
    {
        protected IOptions<PersistentConfig> _config;
        protected PersistentConfig _workingConfig;
        protected readonly IConfigRepository ConfigRepository;
        protected readonly string _contentDirectory;


        public OptionsWindowBase() {  /* Designer only */ }

        public OptionsWindowBase(
            IOptions<PersistentConfig> options,
            IConfigRepository configRepository)
        {
            ConfigRepository = configRepository;
            _config = options;
            _workingConfig = new PersistentConfig();

            // Copy values from real config to working config
            _workingConfig = _config.Value.Clone();
        }

        public virtual void Save()
        {
            ConfigRepository.Save();
        }
    }
}