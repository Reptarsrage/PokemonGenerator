using PokemonGenerator.Controls;
using PokemonGenerator.IO;
using PokemonGenerator.Models;
using PokemonGenerator.Utilities;
using System;
using System.Configuration;
using System.IO;

namespace PokemonGenerator.Forms
{
    public partial class OptionsWindow : WindowBase
    {
        private PersistentConfig _config;
        private readonly IPersistentConfigManager _configManager;
        private readonly string _contentDirectory;

        public OptionsWindow(
            IPersistentConfigManager persistentConfigManager,
            IDirectoryUtility directoryUtility)
        {
            InitializeComponent();
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

            // Data Bind
            OptionsWindowBindingSource.DataSource = _config.Configuration;
        }

        public override void Shown()
        {
            _config = _configManager.Load();
            OptionsWindowBindingSource.DataSource = _config.Configuration;
        }

        public override void Closed()
        {
            // Nothing we need to do here
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            OnWindowClosedEvent(this, new WindowEventArgs(GetType()));
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            _configManager.Save(_config);
            OnWindowClosedEvent(this, new WindowEventArgs(GetType()));
        }
    }
}