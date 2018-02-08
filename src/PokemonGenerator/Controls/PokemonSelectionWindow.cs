using Microsoft.Extensions.Options;
using PokemonGenerator.DAL;
using PokemonGenerator.IO;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Models.Dto;
using System;
using System.ComponentModel;
using System.Linq;

namespace PokemonGenerator.Controls
{
    public partial class PokemonSelectionWindow : OptionsWindowBase
    {
        private readonly IPokemonDA _pokemonDA;

        private int _total;
        private int _selected;

        public PokemonSelectionWindow(
            IPokemonDA pokemonDA,
            IOptions<PersistentConfig> options,
            IPersistentConfigManager persistentConfigManager) : base(options, persistentConfigManager)
        {
            InitializeComponent();

            // Set Title
            Text = "Select Pokemon";

            _pokemonDA = pokemonDA;

            BackgroundWorker.RunWorkerAsync();
        }

        public override void Shown()
        {
            _workingConfig.Configuration.DisabledPokemon.Clear();
            _workingConfig.Configuration.DisabledPokemon.AddRange(_config.Value.Configuration.DisabledPokemon);
            foreach (var btn in LayoutPanelMain.Controls.OfType<SpriteButton>())
            {
                // Un-Bind events
                btn.ItemSelctedEvent += ItemSelcted;

                var id = btn.Index + 1;
                btn.Checked = _workingConfig.Configuration.DisabledPokemon.All(pid => pid != id);

                // Re-Bind events
                btn.ItemSelctedEvent += ItemSelcted;
            }
            UpdateCount();
        }

        public override void Save()
        {
            if (_selected < 6)
            {
                throw new InvalidOperationException("Please select at least 6 Pokemon.");
            }

            _config.Value.Configuration.DisabledPokemon.Clear();
            _config.Value.Configuration.DisabledPokemon.AddRange(_workingConfig.Configuration.DisabledPokemon.Distinct().OrderBy(d => d));

            base.Save();
        }

        private void UpdateCount()
        {
            // TODO LabelCount.Text = $"{_selected}/{_total} Selected";
        }

        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var pokemon = _pokemonDA.GetAllPokemon();

            var worker = sender as BackgroundWorker;

            foreach (var poke in pokemon)
            {
                worker.ReportProgress(1, poke);
            }
        }

        private void BackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var poke = e.UserState as PokemonEntry;
            var selected = _workingConfig.Configuration.DisabledPokemon.All(id => poke.Id != id);
            var legendary = _workingConfig.Configuration.LegendaryPokemon.Any(id => poke.Id == id);
            var special = _workingConfig.Configuration.SpecialPokemon.Any(id => poke.Id == id);
            var forbidden = _workingConfig.Configuration.ForbiddenPokemon.Any(id => poke.Id == id);

            // Create Item
            var item = new SpriteButton(poke.Id - 1 /* Convert to zero based from pokemon 1-based id */, selected)
            {
                Name = poke.Id.ToString(),
                Text = poke.Identifier.ToUpper(),
                Tint =
                    forbidden ? CustomColors.Forbidden :
                    legendary ? CustomColors.Legendary :
                    special ? CustomColors.Special :
                    CustomColors.Standard,
                Enabled = !forbidden
            };

            // Add Item
            LayoutPanelMain.Controls.Add(item);

            // Maintain info
            _total++;
            if (selected)
            {
                _selected++;
            }

            // Bind events
            item.ItemSelctedEvent += ItemSelcted;
        }

        private void ItemSelcted(object sender, ItemSelectedEventArgs args)
        {
            var button = sender as SpriteButton;
            var idx = button.Index + 1 /* Convert back from zero based to pokemon 1-based id */;

            _selected += args.Selected ? 1 : -1;

            if (args.Selected)
            {
                _workingConfig.Configuration.DisabledPokemon.Remove(idx);
            }
            else
            {
                _workingConfig.Configuration.DisabledPokemon.Add(idx);
            }

            UpdateCount();
        }

        private void BackgroundWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateCount();
        }

        private void SelectAll(object sender, EventArgs e)
        {
            foreach (var btn in LayoutPanelMain.Controls.OfType<SpriteButton>())
            {
                btn.Checked = true;
            }
        }

        private void SelectNone(object sender, EventArgs e)
        {
            foreach (var btn in LayoutPanelMain.Controls.OfType<SpriteButton>())
            {
                btn.Checked = false;
            }
        }
    }
}