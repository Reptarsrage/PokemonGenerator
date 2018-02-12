using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.Extensions.Options;
using PokemonGenerator.Controls;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Models.DTO;
using PokemonGenerator.Providers;
using PokemonGenerator.Repositories;

namespace PokemonGenerator.Windows.Options
{
    public partial class PokemonSelectionWindow : OptionsWindowBase
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ISpriteProvider _spriteProvider;

        public PokemonSelectionWindow(
            IPokemonRepository pokemonRepository,
            IOptions<PersistentConfig> options,
            ISpriteProvider spriteProvider,
            IConfigRepository configRepository) : base(options, configRepository)
        {
            InitializeComponent();

            // Set Title
            Text = "Select Pokemon";

            _pokemonRepository = pokemonRepository;
            _spriteProvider = spriteProvider;
        }

        public override void Shown(WindowEventArgs args)
        {
            if (BackgroundWorker.IsBusy)
            {
                return;
            }

            // Clear old buttons
            foreach (var btn in LayoutPanelMain.Controls.OfType<SpriteButton>())
            {
                LayoutPanelMain.Controls.Remove(btn);
            }

            // Load new config
            _workingConfig.Configuration.DisabledPokemon.Clear();
            _workingConfig.Configuration.DisabledPokemon.AddRange(_config.Value.Configuration.DisabledPokemon);

            // Set New buttons
            BackgroundWorker.RunWorkerAsync();
        }

        public override void Save()
        {
            _config.Value.Configuration.DisabledPokemon.Clear();
            _config.Value.Configuration.DisabledPokemon.AddRange(_workingConfig.Configuration.DisabledPokemon.Distinct().OrderBy(d => d));

            base.Save();
        }

        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var pokemon = _pokemonRepository.GetAllPokemon();
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
            var legendary = _config.Value.Configuration.LegendaryPokemon.Any(id => poke.Id == id);
            var special = _config.Value.Configuration.SpecialPokemon.Any(id => poke.Id == id);
            var forbidden = _config.Value.Configuration.ForbiddenPokemon.Any(id => poke.Id == id);
            var levelLocked = poke.MinimumLevel > _config.Value.Options.Level;

            // Create Item
            var item = new SpriteButton(_spriteProvider, poke.Id - 1 /* Convert to zero based from pokemon 1-based id */, selected)
            {
                Name = poke.Id.ToString(),
                Text = poke.Identifier.ToUpper(),
                Tint =
                    forbidden ? CustomColors.Forbidden :
                    levelLocked ? CustomColors.Forbidden :
                    legendary ? CustomColors.Legendary :
                    special ? CustomColors.Special :
                    CustomColors.Standard,
                Enabled = !forbidden && !levelLocked
            };

            // Add Item
            LayoutPanelMain.Controls.Add(item);

            // Bind events
            item.ItemSelctedEvent += ItemSelcted;
        }

        private void ItemSelcted(object sender, ItemSelectedEventArgs args)
        {
            var button = sender as SpriteButton;
            var idx = button.Index + 1 /* Convert back from zero based to pokemon 1-based id */;

            if (args.Selected)
            {
                _workingConfig.Configuration.DisabledPokemon.Remove(idx);
            }
            else
            {
                _workingConfig.Configuration.DisabledPokemon.Add(idx);
            }
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