using Microsoft.Extensions.Options;
using PokemonGenerator.Controls;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Repositories;

namespace PokemonGenerator.Windows.Options
{
    public partial class RandomOptionsWindow : OptionsWindowBase
    {
        public RandomOptionsWindow(
            IOptions<PersistentConfig> options,
            IConfigRepository configRepository) : base(options, configRepository)
        {
            InitializeComponent();

            // Set Title
            Text = "Randomness Options";

            // Data Bind
            OptionsWindowBindingSource.DataSource = _workingConfig.Configuration;
        }

        public override void Closed(WindowEventArgs args)
        {
            base.Closed(null);
        }

        public override void Shown(WindowEventArgs args)
        {
            FieldNumericMean.Value = (decimal)_config.Value.Configuration.Mean;
            FieldNumericSkew.Value = (decimal)_config.Value.Configuration.Skew;
            FieldNumericStandardDeviation.Value = (decimal)_config.Value.Configuration.StandardDeviation;
            FieldNumericDamageModifier.Value = (decimal)_config.Value.Configuration.DamageModifier;
            FieldNumericDamageTypeDelta.Value = (decimal)_config.Value.Configuration.DamageTypeDelta;
            FieldNumericRandomMoveMinPower.Value = (decimal)_config.Value.Configuration.RandomMoveMinPower;
            FieldNumericRandomMoveMaxPower.Value = (decimal)_config.Value.Configuration.RandomMoveMaxPower;
            FieldNumericSameTypeModifier.Value = (decimal)_config.Value.Configuration.SameTypeModifier;
            FieldNumericDamageTypeModifier.Value = (decimal)_config.Value.Configuration.DamageTypeModifier;
            FieldNumericAlreadyPickedMoveModifier.Value = (decimal)_config.Value.Configuration.AlreadyPickedMoveModifier;
            FieldNumericAlreadyPickedMoveEffectsModifier.Value = (decimal)_config.Value.Configuration.AlreadyPickedMoveEffectsModifier;
            CheckBoxAllowDuplicates.Checked = _config.Value.Configuration.AllowDuplicates;
        }

        public override void Save()
        {
            _config.Value.Configuration.Mean = _workingConfig.Configuration.Mean;
            _config.Value.Configuration.Skew = _workingConfig.Configuration.Skew;
            _config.Value.Configuration.StandardDeviation = _workingConfig.Configuration.StandardDeviation;
            _config.Value.Configuration.DamageModifier = _workingConfig.Configuration.DamageModifier;
            _config.Value.Configuration.DamageTypeDelta = _workingConfig.Configuration.DamageTypeDelta;
            _config.Value.Configuration.RandomMoveMinPower = _workingConfig.Configuration.RandomMoveMinPower;
            _config.Value.Configuration.RandomMoveMaxPower = _workingConfig.Configuration.RandomMoveMaxPower;
            _config.Value.Configuration.SameTypeModifier = _workingConfig.Configuration.SameTypeModifier;
            _config.Value.Configuration.DamageTypeModifier = _workingConfig.Configuration.DamageTypeModifier;
            _config.Value.Configuration.AlreadyPickedMoveModifier = _workingConfig.Configuration.AlreadyPickedMoveModifier;
            _config.Value.Configuration.AlreadyPickedMoveEffectsModifier = _workingConfig.Configuration.AlreadyPickedMoveEffectsModifier;
            _config.Value.Configuration.AllowDuplicates = _workingConfig.Configuration.AllowDuplicates;

            base.Save();
        }
    }
}