using Microsoft.Extensions.Options;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Repositories;

namespace PokemonGenerator.Windows.Options
{
    public class OptionsWindowBase : WindowBase
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
        }

        public virtual void Save()
        {
            ConfigRepository.Save();
        }
    }
}