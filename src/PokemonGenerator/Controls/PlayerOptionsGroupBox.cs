using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Providers;
using PokemonGenerator.Validators;
using PokemonGenerator.Windows;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.Options;
using PokemonGenerator.Repositories;

namespace PokemonGenerator.Controls
{
    public partial class PlayerOptionsGroupBox : PageEnabledControl
    {
        private struct WorkerProgress
        {
            public int Index { get; set; }
            public Bitmap Image { get; set; }
            public bool Svg { get; set; }
        }

        private readonly IPokeGeneratorOptionsValidator _optionsValidator;
        private readonly IOptions<PersistentConfig> _options;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ISpriteProvider _spriteProvider;
        private readonly SVGViewer[] _teamImages;

        public PlayerOptionsGroupBox()
        {
            _spriteProvider = DependencyInjector.Get<ISpriteProvider>();
            _optionsValidator = DependencyInjector.Get<IPokeGeneratorOptionsValidator>();
            _pokemonRepository = DependencyInjector.Get<IPokemonRepository>();
            _options = DependencyInjector.Get<IOptions<PersistentConfig>>();

            // Init
            InitializeComponent();
            GroupBox.Text = Text;
            SelectPlayerVersion.DataSource = new[] { "Gold", "Silver" };
            _teamImages = new[]
            {
                PictureTeamFirst,
                PictureTeamSecond,
                PictureTeamThird,
                PictureTeamFourth,
                PictureTeamFifth,
                PictureTeamSixth
            };
        }

        public new bool Validate()
        {
            return ValidatePlayerSection();
        }

        public string PlayerName
        {
            get => TextPlayerName.Text;
            set => UpdateText(TextPlayerName, value);
        }

        public string InLocation
        {
            get => TextPlayerInLocation.Text;
            set => UpdateText(TextPlayerInLocation, value);
        }

        public string OutLocation
        {
            get => TextPlayerOutLocation.Text;
            set => UpdateText(TextPlayerOutLocation, value);
        }

        public PlayerOptions DataSource
        {
            get => BindingSource.DataSource as PlayerOptions;
            set => BindingSource.DataSource = value;
        }

        public int Player { get; set; }

        public void SetOutLocationImage(ImageState state) => ToggleErrorImageToPic(ImagePlayerOutLocation, state);

        public void SetInLocationImage(ImageState state) => ToggleErrorImageToPic(ImagePlayerInLocation, state);

        public void SetNameImage(ImageState state) => ToggleErrorImageToPic(ImagePlayerName, state);

        public override void Shown(WindowEventArgs args)
        {
            if (!BackgroundWorker.IsBusy)
                BackgroundWorker.RunWorkerAsync();

            base.Shown(args);
        }

        public void CorrectTeam()
        {
            if (!BackgroundWorker.IsBusy)
                BackgroundWorker.RunWorkerAsync();
        }

        private void UpdateText(TextBox textBox, string val)
        {
            textBox.Text = val;
            textBox.DataBindings[0].WriteValue();
        }

        private bool CheckIfFileExistsAndAssignImage(TextBox textbox, PictureBox pic, string expectedExtension)
        {
            var good = _optionsValidator.ValidateFileOption(textbox.Text, expectedExtension);
            ToggleErrorImageToPic(pic, good ? ImageState.FileGood : ImageState.FileBad);
            return good;
        }

        private bool CheckIfPathIsValidAndAssignImage(TextBox textbox, PictureBox pic, string expectedExtension)
        {
            var good = _optionsValidator.ValidateFilePathOption(textbox.Text, expectedExtension);
            ToggleErrorImageToPic(pic, good ? ImageState.FileGood : ImageState.FileBad);
            return good;
        }

        private void ToggleErrorImageToPic(PictureBox pic, ImageState state)
        {
            switch (state)
            {
                case ImageState.Bad:
                    pic.BackgroundImage = Properties.Resources.StatusInvalid_16x;
                    break;
                case ImageState.Good:
                    pic.BackgroundImage = Properties.Resources.StatusOK_16x;
                    break;
                case ImageState.Warning:
                    pic.BackgroundImage = Properties.Resources.StatusWarning_16x;
                    break;
                case ImageState.FileBad:
                    pic.BackgroundImage = Properties.Resources.FileError_16x;
                    break;
                case ImageState.FileGood:
                    pic.BackgroundImage = Properties.Resources.FileOK_16x;
                    break;
                case ImageState.FileWarning:
                    pic.BackgroundImage = Properties.Resources.FileWarning_16x;
                    break;
                default:
                    pic.BackgroundImage = null;
                    break;
            }
            pic.Show();
        }

        private bool ValidatePlayerSection()
        {
            // Check Player One In/Out Locations
            var good = CheckIfFileExistsAndAssignImage(TextPlayerInLocation, ImagePlayerInLocation, ".sav");
            good &= CheckIfPathIsValidAndAssignImage(TextPlayerOutLocation, ImagePlayerOutLocation, ".sav");

            // Check Player One Name
            var textPlayerNameGood = _optionsValidator.ValidateName(TextPlayerName.Text);
            ToggleErrorImageToPic(ImagePlayerName, textPlayerNameGood ? ImageState.Good : ImageState.Bad);
            good &= textPlayerNameGood;

            return good;
        }

        private string ChooseFile(string filter = "Application|*.exe")
        {
            OpenFileDialog.Filter = filter;
            OpenFileDialog.FileName = "";
            return OpenFileDialog.ShowDialog().Equals(DialogResult.OK) ? OpenFileDialog.FileName : null;
        }

        private void ButtonPlayerInLocationClick(object sender, EventArgs e)
        {
            UpdateText(TextPlayerInLocation, ChooseFile("Save Files|*.sav") ?? TextPlayerInLocation.Text);
            ValidatePlayerSection();
        }

        private void ButtonPlayerOutLocationClick(object sender, EventArgs e)
        {
            UpdateText(TextPlayerOutLocation, ChooseFile("Save Files|*.sav") ?? TextPlayerOutLocation.Text);
            ValidatePlayerSection();
        }

        private void PlayerValidate(object sender, EventArgs e)
        {
            ValidatePlayerSection();
        }

        private void TeamClick(object sender, MouseEventArgs e)
        {
            OnWindowOpenedEvent(this, new WindowEventArgs(typeof(TeamSelectionWindow)));
        }

        private void TeamClick(object sender, EventArgs e)
        {
            OnWindowOpenedEvent(this, new WindowEventArgs(typeof(TeamSelectionWindow)) { Player = Player });
        }

        private void BackgroundWorkerDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var possiblePokemon = _pokemonRepository.GetAllPokemon();
            
            // Prune
            var toRemove = DataSource.Team.MemberIds
                .Where(id => possiblePokemon.All(poke => poke.Id != id || poke.MinimumLevel > _options.Value.Options.Level))
                .ToList();
            for (var i = 0; i < _teamImages.Length; i++)
            {
                if (i < DataSource.Team.MemberIds.Count && toRemove.Any(id => id == DataSource.Team.MemberIds[i]))
                {
                    DataSource.Team.MemberIds.RemoveAt(i);
                }

                Bitmap image = null;
                var svg = true;
                if (i < DataSource.Team.MemberIds.Count)
                {
                    var idx = DataSource.Team.MemberIds[i];
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

        private void BackgroundWorkerProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
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