using Microsoft.Extensions.Options;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Repositories;

namespace PokemonGenerator.Windows.Options
{
    public partial class PokemonLikelinessWindow : OptionsWindowBase
    {
        public PokemonLikelinessWindow(
            IOptions<PersistentConfig> options,
            IConfigRepository configRepository) : base(options, configRepository)
        {
            InitializeComponent();

            // Set Title
            Text = "Pokemon Liklihood";

            // Data Bind
            OptionsWindowBindingSource.DataSource = _workingConfig.Configuration;
        }

        public override void Closed(WindowEventArgs args)
        {
            base.Closed(null);
        }

        public override void Shown(WindowEventArgs args)
        {
            FieldNumericStandard.Value = (decimal)_config.Value.Configuration.PokemonLiklihood.Standard;
            FieldNumericLegendary.Value = (decimal)_config.Value.Configuration.PokemonLiklihood.Legendary;
            FieldNumericSpecial.Value = (decimal)_config.Value.Configuration.PokemonLiklihood.Special;
        }

        public override void Save()
        {
            _config.Value.Configuration.PokemonLiklihood.Standard = _workingConfig.Configuration.PokemonLiklihood.Standard;
            _config.Value.Configuration.PokemonLiklihood.Legendary = _workingConfig.Configuration.PokemonLiklihood.Legendary;
            _config.Value.Configuration.PokemonLiklihood.Special = _workingConfig.Configuration.PokemonLiklihood.Special;

            base.Save();
        }
    }
}