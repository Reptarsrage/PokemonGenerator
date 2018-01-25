using PokemonGenerator.IO;
using PokemonGenerator.Models;
using PokemonGenerator.Utilities;
using System.Configuration;
using System.IO;

namespace PokemonGenerator.Controls
{
    public class OptionsWindowBase : WindowBase
    {
        protected PersistentConfig _config;
        protected readonly IPersistentConfigManager _configManager;
        protected readonly string _contentDirectory;

        public OptionsWindowBase() {  /* Designer only */ }

        public OptionsWindowBase(
            IPersistentConfigManager persistentConfigManager,
            IDirectoryUtility directoryUtility)
        {
            _configManager = persistentConfigManager;
            _contentDirectory = directoryUtility.ContentDirectory();

            // Load Persistent Config
            var configFileName = ConfigurationManager.AppSettings["configFileName"];
            if (!Path.IsPathRooted(configFileName))
            {
                configFileName = Path.Combine(_contentDirectory, configFileName);
            }
            _configManager.ConfigFilePath = configFileName;
            _config = _configManager.Load();
        }

        public virtual void Save()
        {
            _configManager.Save(_config);
        }
    }
}