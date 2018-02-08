using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.Options;
using PokemonGenerator.Controls;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Models.DTO;
using PokemonGenerator.Providers;
using PokemonGenerator.Repositories;

namespace PokemonGenerator.Windows
{
    public partial class TeamSelectionWindow : WindowBase
    {
        private readonly IOptions<PersistentConfig> _config;
        private readonly ISpriteProvider _spriteProvider;
        private readonly Team _workingConfig;
        private readonly IPokemonRepository _pokemonRepository;
        protected readonly IConfigRepository ConfigRepository;

        private int player;

        public TeamSelectionWindow(
            IPokemonRepository pokemonRepository,
            IOptions<PersistentConfig> options,
            ISpriteProvider spriteProvider,
            IConfigRepository configRepository)
        {
            InitializeComponent();

            // Set Title
            Text = "Select Team";

            ConfigRepository = configRepository;
            _config = options;
            _spriteProvider = spriteProvider;
            _pokemonRepository = pokemonRepository;
            _workingConfig = new Team();
            player = 1;

            BackgroundWorker.RunWorkerAsync();
        }

        public override void Shown(WindowEventArgs args)
        {
            if (args == null || args.Player < 0 || args.Player > 2)
            {
                throw new ArgumentException(nameof(WindowEventArgs.Player));
            }

            player = args.Player;
            var teamConfig = player == 1 ? _config.Value.Options.PlayerOne.Team : _config.Value.Options.PlayerTwo.Team;
            _workingConfig.MemberIds.Clear();
            _workingConfig.MemberIds.AddRange(teamConfig.MemberIds);
            foreach (var btn in LayoutPanelMain.Controls.OfType<SpriteButton>())
            {
                // Un-Bind events
                btn.ItemSelctedEvent += ItemSelcted;

                var id = btn.Index + 1;
                btn.Checked = _workingConfig.MemberIds.Any(pid => pid == id);

                // Re-Bind events
                btn.ItemSelctedEvent += ItemSelcted;
            }
            UpdateCount();
        }

        private void Save()
        {
            if (_workingConfig.MemberIds.Count > 6)
            {
                throw new InvalidOperationException("Please select at most 6 Pokemon.");
            }

            if (player == 1)
            {
                _config.Value.Options.PlayerOne.Team.MemberIds.Clear();
                _config.Value.Options.PlayerOne.Team.MemberIds.AddRange(_workingConfig.MemberIds.OrderBy(i => i));
            }
            else
            {
                _config.Value.Options.PlayerTwo.Team.MemberIds.Clear();
                _config.Value.Options.PlayerTwo.Team.MemberIds.AddRange(_workingConfig.MemberIds.OrderBy(i => i));
            }

            ConfigRepository.Save();
        }

        private void UpdateCount()
        {
            // TODO LabelCount.Text = $"{_selected}/{_total} Selected";
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
            var selected = _workingConfig?.MemberIds.Any(id => poke.Id == id) ?? false;
            var legendary = _config.Value.Configuration.LegendaryPokemon.Any(id => poke.Id == id);
            var special = _config.Value.Configuration.SpecialPokemon.Any(id => poke.Id == id);
            var forbidden = _config.Value.Configuration.ForbiddenPokemon.Any(id => poke.Id == id);

            // Create Item
            var item = new SpriteButton(_spriteProvider, poke.Id - 1 /* Convert to zero based from pokemon 1-based id */, selected)
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

            // Bind events
            item.ItemSelctedEvent += ItemSelcted;
        }

        private void ItemSelcted(object sender, ItemSelectedEventArgs args)
        {
            var button = sender as SpriteButton;
            var idx = button.Index + 1 /* Convert back from zero based to pokemon 1-based id */;

            if (args.Selected)
            {
                if (_workingConfig.MemberIds.All(id => id != idx))
                {
                    _workingConfig.MemberIds.Add(idx);
                }
            }
            else
            {
                if (_workingConfig.MemberIds.Any(id => id == idx))
                {
                    _workingConfig.MemberIds.Remove(idx);
                }        
            }

            UpdateCount();
        }

        private void BackgroundWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateCount();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            OnWindowClosedEvent(this, new WindowEventArgs(GetType()));
        }

        private void ButtonOkClick(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (InvalidOperationException ex)
            {
                var response = MessageBox.Show($"{ex.Message}\nWould you like to close without saving?", "Unable to save", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (response == DialogResult.Cancel)
                {
                    return;
                }
            }

            OnWindowClosedEvent(this, new WindowEventArgs(GetType()));
        }
    }
}