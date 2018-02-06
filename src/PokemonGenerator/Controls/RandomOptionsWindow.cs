﻿using Microsoft.Extensions.Options;
using PokemonGenerator.Controls;
using PokemonGenerator.IO;
using PokemonGenerator.Models;

namespace PokemonGenerator.Forms
{
    public partial class RandomOptionsWindow : OptionsWindowBase
    {
        public RandomOptionsWindow(
            IOptions<PersistentConfig> options,
            IPersistentConfigManager persistentConfigManager) : base(options, persistentConfigManager)
        {
            InitializeComponent();

            // Set Title
            Text = "Randomness Options";

            // Data Bind
            OptionsWindowBindingSource.DataSource = _workingConfig.Configuration;
        }

        public override void Closed()
        {
            base.Closed();
        }

        public override void Shown()
        {
            FieldNumericMean.Value = (decimal)_config.Value.Configuration.Mean;
            FieldNumericSkew.Value = (decimal)_config.Value.Configuration.Skew;
            FieldNumericStandardDeviation.Value = (decimal)_config.Value.Configuration.StandardDeviation;
            FieldNumericDamageModifier.Value = (decimal)_config.Value.Configuration.DamageModifier;
            FieldNumericDamageTypeDelta.Value = (decimal)_config.Value.Configuration.DamageTypeDelta;
            FieldNumericRandomMoveMinPower.Value = (decimal)_config.Value.Configuration.RandomMoveMinPower;
            FieldNumericRandomMoveMaxPower.Value = (decimal)_config.Value.Configuration.RandomMoveMaxPower;
            FieldNumericSameTypeModifier.Value = (decimal)_config.Value.Configuration.SameTypeModifier;
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

            base.Save();
        }
    }
}