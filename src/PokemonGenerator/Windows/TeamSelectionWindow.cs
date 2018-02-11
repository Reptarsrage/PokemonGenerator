using Microsoft.Extensions.Options;
using PokemonGenerator.Controls;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Models.DTO;
using PokemonGenerator.Providers;
using PokemonGenerator.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PokemonGenerator.Windows
{
    public partial class TeamSelectionWindow : PageEnabledControl
    {
        private struct WorkerProgress
        {
            public int Index { get; set; }
            public Bitmap Image { get; set; }
            public bool Svg { get; set; }
        }

        private readonly IOptions<PersistentConfig> _config;
        private readonly ISpriteProvider _spriteProvider;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IConfigRepository _configRepository;
        private readonly Team _workingConfig;
        private readonly SVGViewer[] _teamImages;
        private readonly Stack<SpriteButton> _selected;
       
        private int _player;
        private bool _ignoreFiredSelectionEventsFlag;

        public TeamSelectionWindow(
            IPokemonRepository pokemonRepository,
            IOptions<PersistentConfig> options,
            ISpriteProvider spriteProvider,
            IConfigRepository configRepository)
        {
            InitializeComponent();

            // Set Title
            Text = "Select Team";

            _configRepository = configRepository;
            _config = options;
            _spriteProvider = spriteProvider;
            _pokemonRepository = pokemonRepository;
            _workingConfig = new Team();
            _player = 1;
            _selected = new Stack<SpriteButton>();
            _ignoreFiredSelectionEventsFlag = false;

            _teamImages = new[]
            {
                PictureTeamFirst,
                PictureTeamSecond,
                PictureTeamThird,
                PictureTeamFourth,
                PictureTeamFifth,
                PictureTeamSixth
            };

            BackgroundWorker.RunWorkerAsync();
        }

        public override void Shown(WindowEventArgs args)
        {
            if (args == null || args.Player < 0 || args.Player > 2)
            {
                throw new ArgumentException(nameof(WindowEventArgs.Player));
            }

            // Load newest config
            _player = args.Player;
            var teamConfig = _player == 1 ? _config.Value.Options.PlayerOne.Team : _config.Value.Options.PlayerTwo.Team;
            _workingConfig.MemberIds.Clear();
            _workingConfig.MemberIds.AddRange(teamConfig.MemberIds);

            // Load Team Images
            BackgroundWorkerTeam.RunWorkerAsync();

            // Un-Bind events
            _ignoreFiredSelectionEventsFlag = true;

            // Update Buttons
            foreach (var btn in LayoutPanelMain.Controls.OfType<SpriteButton>())
            {
                var id = btn.Index + 1;
                btn.Checked = _workingConfig.MemberIds.Any(pid => pid == id);
                if (btn.Checked)
                {
                    _selected.Push(btn);
                }
            }

            // Re-Bind events
            _ignoreFiredSelectionEventsFlag = false;

            UpdateCount();
        }

        private void Save()
        {
            if (_workingConfig.MemberIds.Count > 6)
            {
                throw new InvalidOperationException("Please select at most 6 Pokemon.");
            }

            if (_player == 1)
            {
                _config.Value.Options.PlayerOne.Team.MemberIds.Clear();
                _config.Value.Options.PlayerOne.Team.MemberIds.AddRange(_workingConfig.MemberIds.OrderBy(i => i));
            }
            else
            {
                _config.Value.Options.PlayerTwo.Team.MemberIds.Clear();
                _config.Value.Options.PlayerTwo.Team.MemberIds.AddRange(_workingConfig.MemberIds.OrderBy(i => i));
            }

            _configRepository.Save();
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
            var selectedFlag = _workingConfig?.MemberIds.Any(id => poke.Id == id) ?? false;
            var legendaryFlag = _config.Value.Configuration.LegendaryPokemon.Any(id => poke.Id == id);
            var specialFlag = _config.Value.Configuration.SpecialPokemon.Any(id => poke.Id == id);
            var forbiddenFlag = _config.Value.Configuration.ForbiddenPokemon.Any(id => poke.Id == id);
            var levelLockedFlag = poke.MinimumLevel > _config.Value.Options.Level;

            // Create Item
            var item = new SpriteButton(_spriteProvider, poke.Id - 1 /* Convert to zero based from pokemon 1-based id */, selectedFlag)
            {
                Name = poke.Id.ToString(),
                Text = poke.Identifier.ToUpper(),
                Tint =
                    forbiddenFlag ? CustomColors.Forbidden :
                    levelLockedFlag ? CustomColors.Forbidden :
                    legendaryFlag ? CustomColors.Legendary :
                    specialFlag ? CustomColors.Special :
                    CustomColors.Standard,
                Enabled = !forbiddenFlag && !levelLockedFlag,
            };

            // Add Item
            LayoutPanelMain.Controls.Add(item);

            // Bind events
            item.ItemSelctedEvent += ItemSelcted;
        }

        private void ItemSelcted(object sender, ItemSelectedEventArgs args)
        {
            if (_ignoreFiredSelectionEventsFlag)
                return;

            var button = sender as SpriteButton;
            var idx = button.Index + 1; // Convert back from zero based to pokemon 1-based id

            if (args.Selected && _workingConfig.MemberIds.All(id => id != idx))
            {
                if (_workingConfig.MemberIds.Count == 6)
                {
                    _ignoreFiredSelectionEventsFlag = true;
                    var popped = _selected.Pop();  
                    popped.Checked = false;
                    _workingConfig.MemberIds.Remove(popped.Index + 1); // Convert back from zero based to pokemon 1-based id
                    _ignoreFiredSelectionEventsFlag = false;
                }

                _workingConfig.MemberIds.Add(idx);
                _selected.Push(button);
            }
            else if (!args.Selected && _workingConfig.MemberIds.Any(id => id == idx))
            {
                _workingConfig.MemberIds.Remove(idx);
            }

            if (!BackgroundWorkerTeam.IsBusy)
                BackgroundWorkerTeam.RunWorkerAsync();

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

        private void BackgroundWorkerTeamDoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            for (var i = 0; i < _teamImages.Length; i++)
            {
                Bitmap image = null;
                var svg = true;
                if (i < _workingConfig.MemberIds.Count)
                {
                    var idx = _workingConfig.MemberIds[i];
                    image = _spriteProvider.RenderSprite(idx - 1 /* Sprite is 0-based Pokemon are 1-based */, _teamImages[i].Size);
                    svg = false;
                }

                worker.ReportProgress(1, new WorkerProgress
                {
                    Index = i,
                    Image = image,
                    Svg = svg
                });
            }
        }

        private void BackgroundWorkerTeamProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var args = (WorkerProgress)e.UserState;
            if (args.Svg)
            {
                _teamImages[args.Index].SvgImage = nameof(Properties.Resources.Question_16x);
            }
            else
            {
                _teamImages[args.Index].Image = args.Image;
            }
        }
    }
}