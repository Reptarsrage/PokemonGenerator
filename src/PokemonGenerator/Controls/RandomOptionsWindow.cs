using PokemonGenerator.Controls;
using PokemonGenerator.IO;
using PokemonGenerator.Utilities;

namespace PokemonGenerator.Forms
{
    public partial class RandomOptionsWindow : OptionsWindowBase
    {
        public RandomOptionsWindow(
            IPersistentConfigManager persistentConfigManager,
            IDirectoryUtility directoryUtility) : base(persistentConfigManager, directoryUtility)
        {
            InitializeComponent();

            // Set Title
            Text = "Randomness Options";

            // Data Bind
            OptionsWindowBindingSource.DataSource = _config.Configuration;
        }

        public override void Shown()
        {
            _config = _configManager.Load();
            OptionsWindowBindingSource.DataSource = _config.Configuration;
        }
    }
}