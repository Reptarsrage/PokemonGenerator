using Microsoft.Extensions.Options;
using PokemonGenerator.IO;
using PokemonGenerator.Models;

namespace PokemonGenerator.Controls
{
    public class OptionsWindowBase : WindowBase
    {
        protected IOptions<PersistentConfig> _config;
        protected PersistentConfig _workingConfig;
        protected readonly IPersistentConfigManager _configManager;
        protected readonly string _contentDirectory;


        public OptionsWindowBase() {  /* Designer only */ }

        public OptionsWindowBase(
            IOptions<PersistentConfig> options,
            IPersistentConfigManager persistentConfigManager)
        {
            _configManager = persistentConfigManager;
            _config = options;
            _workingConfig = new PersistentConfig();
        }

        public virtual void Save()
        {
            _configManager.Save();
        }
    }
}