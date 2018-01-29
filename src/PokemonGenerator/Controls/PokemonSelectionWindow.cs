using Microsoft.Extensions.Options;
using PokemonGenerator.Controls;
using PokemonGenerator.DAL;
using PokemonGenerator.IO;
using PokemonGenerator.Models;
using System;
using System.ComponentModel;
using System.Linq;

namespace PokemonGenerator.Forms
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
            _workingConfig.Configuration.IgnoredPokemon.Clear();
            _workingConfig.Configuration.IgnoredPokemon.AddRange(_config.Value.Configuration.IgnoredPokemon);
            foreach (var btn in LayoutPanelMain.Controls.OfType<SpriteButton>())
            {
                // Un-Bind events
                btn.ItemSelctedEvent += ItemSelcted;

                var id = btn.Index + 1;
                btn.Checked = _workingConfig.Configuration.IgnoredPokemon.All(pid => pid != id);

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

            _config.Value.Configuration.IgnoredPokemon.Clear();
            _config.Value.Configuration.IgnoredPokemon.AddRange(_workingConfig.Configuration.IgnoredPokemon.Distinct().OrderBy(d => d));

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
            var selected = _workingConfig.Configuration.IgnoredPokemon.All(id => poke.Id != id);

            // Create Item
            var item = new SpriteButton(poke.Id - 1 /* Convert to zero based from pokemon 1-based id */, selected)
            {
                Name = poke.Id.ToString(),
                Text = poke.Identifier.ToUpper()
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
                _workingConfig.Configuration.IgnoredPokemon.Remove(idx);
            }
            else
            {
                _workingConfig.Configuration.IgnoredPokemon.Add(idx);
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