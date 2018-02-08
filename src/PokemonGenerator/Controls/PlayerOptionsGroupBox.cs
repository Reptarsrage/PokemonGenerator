using Microsoft.Extensions.Options;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Providers;
using PokemonGenerator.Validators;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PokemonGenerator.Controls
{
    public partial class PlayerOptionsGroupBox : WindowBase
    {
        private struct WorkerProgress
        {
            public int Index { get; set; }
            public Bitmap Image { get; set; }
            public bool Svg { get; set; }
        }

        private IPokeGeneratorOptionsValidator _optionsValidator;
        private ISpriteProvider _spriteProvider;
        private SVGViewer[] TeamImages;

        public PlayerOptionsGroupBox() { /* For Designer only */ }

        public void Initialize(
            int player,
            IOptions<PersistentConfig> options,
            IPokeGeneratorOptionsValidator optionsValidator,
            ISpriteProvider spriteProvider)
        {
            Player = player;
            _spriteProvider = spriteProvider;
            _optionsValidator = optionsValidator;

            // Init
            InitializeComponent();
            GroupBox.Text = Text;
            SelectPlayerVersion.DataSource = new[] { "Gold", "Silver" };
            TeamImages = new[]
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
            BackgroundWorker.RunWorkerAsync();

            base.Shown(args);
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
            var TextPlayerNameGood = _optionsValidator.ValidateName(TextPlayerName.Text);
            ToggleErrorImageToPic(ImagePlayerName, TextPlayerNameGood ? ImageState.Good : ImageState.Bad);
            good &= TextPlayerNameGood;

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

            for (var i = 0; i < TeamImages.Length; i++)
            {
                Bitmap image = null;
                var svg = true;
                if (i < DataSource.Team.MemberIds.Count)
                {
                    var idx = DataSource.Team.MemberIds[i];
                    image = _spriteProvider.RenderSprite(idx - 1 /* Sprite is 0-based Pokemon are 1-based */, TeamImages[i].Size);
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
                TeamImages[args.Index].SvgImage = Properties.Resources.Question_16x;
            }
            else
            {
                TeamImages[args.Index].Image = args.Image;
            }
        }
    }
}